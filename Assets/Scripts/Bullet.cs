using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;

    public GameObject bullet;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        bullet = gameObject;
        Destroy(gameObject, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
