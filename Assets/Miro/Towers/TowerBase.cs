using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerBase : MonoBehaviour
{
    public int health;
    public int damage;
    public int cost;
    public float attackSpeed;
    public float lastTimeGotDamage=float.NegativeInfinity;
    public string keyShortcut;
    public string currentState;
    public bool isDisabled = false;
    public bool isVulnerable = true;

    public GameObject projectile;
    public Transform position;
    public Animator towerAnimator;
    public BoxCollider2D rangeColider;
    public SpriteRenderer spriteRenderer;
    public Color damageColor = Color.red;
    public Color normalColor = Color.white;
    public enemyClass enemy;
    public GameManager gameManager;

    public virtual void Start()
    {
        rangeColider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        towerAnimator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        position = this.gameObject.transform;
        Idle();
    }

    public virtual void Attack() { }

    public virtual void Sell()
    {
        ChangeAnimationState("Death");
        gameManager.Total += cost / 100 * 25;
        Destroy(this.gameObject);
    }

    public virtual void Idle() { }

    public virtual void Death()
    {
        ChangeAnimationState("Death");
    }

    public virtual void Obliterate()
    {
        Destroy(this.gameObject);
    }

    public abstract void Shoot();

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Attack");
            Attack();
            enemy = collision.gameObject.GetComponent<enemyClass>();
            health -= enemy.attackDamage;
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Idle();
        }
    }

    public virtual void Update()
    {
        if (spriteRenderer)
        {
            spriteRenderer.color = Color.Lerp(damageColor, normalColor, Time.time-lastTimeGotDamage);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            lastTimeGotDamage = Time.time;
        }

        if (health <= 0)
        {
            Death();
        }
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        towerAnimator.Play(newState);

        currentState = newState;
    }
}
