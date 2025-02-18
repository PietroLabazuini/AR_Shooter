using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyHealth healthBar;
    float health;
    float maxHealth = 100f;
    // Start is called before the first frame update
    void Start()
    {
        tag = "Enemy";
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0)
        {
            //MORT
            Destroy(gameObject);
        }
    }

    public void Damage(float damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar(health/maxHealth);
    }
}
