using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCollider : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public int towerDamage;
    public GameObject enemy;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void EnemyTakeDamage(int damage)
    {
        currentHealth-= damage;

        //animacka

        if (currentHealth <= 0)
        {
            enemy.GetComponent<enemyClass>().EnemyDie();
        }
    }
}
