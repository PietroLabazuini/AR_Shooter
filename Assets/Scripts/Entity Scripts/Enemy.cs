using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Image healthBar;
    float health;
    GameObject prefab;
    float maxHealthRectSize;
    float maxHealth = 100f;

    // Start is called before the first frame update
    void Start()
    {
        tag = "Enemy";
        health = maxHealth;
        maxHealthRectSize = healthBar.rectTransform.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (health > 0) 
        {
            //VIVANT

        }
        else
        {
            //MORT
            Destroy(gameObject);
        }
    }

    public void Damage(float damage)
    {
        health -= damage;
        UpdateHealthBar(health);
    }

    void UpdateHealthBar(float currHealth)
    {
        float normalizedHealth = Mathf.Clamp(currHealth / maxHealth, 0f, 1f);
        Vector2 newSize = healthBar.rectTransform.sizeDelta;
        newSize.x = maxHealthRectSize * normalizedHealth;
        Debug.Log(newSize.x);
        healthBar.rectTransform.sizeDelta = newSize;
    }
}
