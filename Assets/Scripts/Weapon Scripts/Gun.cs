using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Gun : MonoBehaviour
{
    [SerializeField]
    protected GameObject _bulletPrefab;
    [SerializeField]
    protected GameObject _bulletSpawnpoint;
    [SerializeField]
    protected Text _ammoDisplay;

    protected Animator _animator;
    protected GameObject _gun;

    protected float damage;
    protected float reloadTime;
    protected float fireTime;
    protected float bspeed;
    protected float delay;

    protected int currAmmo;
    protected int magSize;
    float alphaFactor = 1f;

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
        DisplayCurrentGunInfo();

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
            PlayAnimation(reloadAnim);
        }
    }

    protected void Reload()
    {
        if (isReloading)
        {
            if (delay < Time.time)
            {
                currAmmo = magSize;
                isReloading = false;
            }
        }
    }

    protected void Shoot()
    {
        if (!isReloading && !isSwitching && Input.touchCount > 0)
        {
            if (!isShooting)
            {
                isShooting = true;
                delay = Time.time;
            }

            if(delay <= Time.time)
            {
                if (currAmmo > 0)
                {
                    PlayAnimation(shootAnim);
                    ShootBullet();
                    currAmmo--;
                    delay += fireTime;
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

    protected void ShootBullet()
    {
        if (_bulletSpawnpoint != null)
        {
            GameObject bulletObject = Instantiate(_bulletPrefab, _bulletSpawnpoint.transform);
            bulletObject.transform.parent = null;
            bulletObject.GetComponent<Rigidbody>().velocity = bspeed * -_bulletSpawnpoint.transform.forward;
            Bullet bullet = bulletObject.AddComponent<Bullet>();
            bullet.damage = damage;
        }
    }

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
                string anim;
                if (isEquipped)
                {
                    anim = unequipAnim;
                }
                else
                {
                    anim = equipAnim;
                }
                PlayAnimation(anim);
                delay = Time.time + 1f;
                isSwitchAnimPlaying = true;
                
            }
            else
            {
                if (delay <= Time.time)
                {
                    isEquipped = !isEquipped;
                    gameObject.SetActive(isEquipped);
                    isSwitching = false;
                    isSwitchAnimPlaying = false;
                }
                else
                {
                    Debug.Log("Playing anim");
                }
            }
        }
    }

    protected void DisplayCurrentGunInfo()
    {
        _ammoDisplay.text = currAmmo.ToString();

        if (currAmmo < 0.3f * magSize)
        {
            if (currAmmo == 0)
            {
                if (_ammoDisplay.color.a <= 0) { alphaFactor = 1f; }
                else { if (_ammoDisplay.color.a >= 1) { alphaFactor = -1f; } }
                float newAlpha = _ammoDisplay.color.a + (0.02f * alphaFactor);
                _ammoDisplay.color = new Color(1, 0, 0, newAlpha);
            }
            else
            {
                _ammoDisplay.color = new Color(1, 0.7f, 0);
            }
        }
        else
        {
            _ammoDisplay.color = Color.white;
        }
    }

    /*
     * OLD VERSION
     * protected IEnumerator ShootCoroutine()
    {
        while (true)
        {
            if (isReloading)
            {
                PlayAnimation(reloadAnim);
                isReloading = false;
                yield return new WaitForSeconds(reloadTime);
                currAmmo = magSize;
            }
            else
            {
                while (Input.touchCount > 0)
                {
                    Debug.Log("Shoot = " + Time.time);
                    if (currAmmo > 0)
                    {
                        PlayAnimation(shootAnim);
                        ShootBullet();
                        currAmmo--;
                        yield return new WaitForSeconds(fireTime);
                    }
                    else
                    {
                        //reload sound
                        yield return null;
                    }
                }
            }
            yield return null;
        }
    }
    */

}