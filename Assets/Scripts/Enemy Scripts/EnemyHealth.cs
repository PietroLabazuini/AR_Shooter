using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    Image healthBar;

    public void UpdateHealthBar(float currHealth)
    {
        healthBar.fillAmount = currHealth;
    }
}
