using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Gun : MonoBehaviour
{
    /*
    [SerializeField]
    protected GameObject _bulletPrefab;
    [SerializeField]
    protected GameObject _bulletSpawnpoint;*/
    [SerializeField]
    protected Camera _player;
    [SerializeField]
    public Sprite _icon;

    protected Animator _animator;
    protected GameObject _gun;

    protected float damage;
    protected float reloadTime;
    protected float fireRate;
    protected float bspeed;
    protected float delay;

    public int currAmmo;
    public int magSize;
    

    protected bool isReloading;
    protected bool isShooting;
    protected bool isSwitching;
    protected bool isSwitchAnimPlaying;
    public bool isEquipped;
    
    protected string reloadAnim;
    protected string shootAnim;
    protected string equipAnim;
    protected string unequipAnim;
    protected string currSwitchAnim;

    protected void Start()
    {
        _gun = gameObject;
        _animator = _gun.GetComponent<Animator>();
    }

    protected void Update()
    {
        Equip();

        Reload();

        Shoot();
    }

    protected virtual void PlayAnimation(string animationName)
    {
        if (_animator != null)
        {
            _animator.Play(animationName);
        }
    }

    public void TriggerReload()
    {
        if (!isSwitching)
        {
            isReloading = true;
            delay = Time.time + reloadTime;
            _animator.SetTrigger("isReloading");
        }
    }

    protected void Reload()
    {
        if (isReloading)
        {
            if (delay <= Time.time)
            {
                currAmmo = magSize;
                isReloading = false;
            }
        }
    }

    protected void Shoot()
    {
        /*int touch = 0;
        if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                touch = 1;
            }
        }
        else
        {
            touch = Input.touchCount;
        }*/

        if (!isReloading && !isSwitching && Input.touchCount > 0)
        {
            if (!isShooting)
            {
                isShooting = true;
                delay = Time.time;
            }

            if(delay <= Time.time)
            {
                //CAN SHOOT
                if (currAmmo > 0)
                {
                    _animator.SetTrigger("isShooting");
                    if(Physics.Raycast(_player.transform.position, _player.transform.forward, out RaycastHit hit))
                    {
                        Enemy enemyhit = hit.transform.GetComponent<Enemy>();
                        if (enemyhit != null)
                        {
                            enemyhit.Damage(damage);
                        }
                    }
                    else
                    {
                        Debug.Log("Didn't hit");
                    }
                    //ShootBullet();
                    currAmmo--;
                    delay += 60/fireRate;
                }
                else
                {
                    //empty magazine sound
                }
            }
        }
        else
        {
            isShooting = false;
        }
    }

    /*protected void ShootBullet()
    {
        if (_bulletSpawnpoint != null)
        {
            GameObject bulletObject = Instantiate(_bulletPrefab, _bulletSpawnpoint.transform);
            bulletObject.transform.parent = null;
            bulletObject.GetComponent<Rigidbody>().velocity = bspeed * -_bulletSpawnpoint.transform.forward;
            Bullet bullet = bulletObject.AddComponent<Bullet>();
            bullet.damage = damage;
        }
    }*/

    public void TriggerEquip()
    {
        if (!isSwitching)
        {
            isSwitching = true;
        }
    }

    protected void Equip()
    {
        if (!isReloading && !isShooting && isSwitching)
        {
            if (!isSwitchAnimPlaying) 
            {
                _animator.SetTrigger("isSwitching");
                delay = Time.time + 1f;
                isSwitchAnimPlaying = true;
                
            }
            else
            {
                if (delay <= Time.time)
                {
                    isEquipped = !isEquipped;
                    //gameObject.SetActive(isEquipped);
                    isSwitching = false;
                    isSwitchAnimPlaying = false;
                }
            }
        }
    }
}