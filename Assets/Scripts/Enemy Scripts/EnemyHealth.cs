using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    Image _healthBar;
    [SerializeField]
    GameObject _axis;

    GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }
    void Update()
    {
        _axis.gameObject.transform.LookAt(player.transform.position);
    }

    public void UpdateHealthBar(float currHealth)
    {
        _healthBar.fillAmount = currHealth;
    }
}
