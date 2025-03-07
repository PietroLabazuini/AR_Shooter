using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Gun : MonoBehaviour
{
    #region VARIABLES
    [SerializeField]
    protected Camera _player;

    [SerializeField]
    public Sprite _icon;

    protected Animator _animator;

    protected GameObject _gun;

    protected float damage;
    protected float reloadTime;
    protected float switchTime;
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
    #endregion

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

    #region RELOAD
    public void TriggerReload()
    {
        if (isEquipped && !isSwitching)
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
    #endregion

    #region SHOOT
    protected void Shoot()
    {
        if (isEquipped && !isReloading && !isSwitching && Input.touchCount > 0)
        {
            if (!isShooting)
            {
                isShooting = true;
                delay = Time.time;
                _animator.SetBool("isShooting", true);
            }

            // Checking if the player can shoot (if it's the first bullet or if the previous one was fired)
            if(delay <= Time.time)
            {
                //Checking if the gun still has ammo
                if (currAmmo > 0)
                {
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
                    currAmmo--;
                    delay += 60/fireRate;
                }
                else
                {
                    //empty magazine sound
                    _animator.SetBool("isShooting", false);
                }
            }
        }
        else
        {
            isShooting = false;
            _animator.SetBool("isShooting", false);
        }

    }
    #endregion

    #region EQUIP
    public void TriggerEquip()
    {
        isSwitching = true;
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
                    isSwitching = false;
                    isSwitchAnimPlaying = false;
                }
            }
        }
    }
    #endregion

    #region UTILS
    protected virtual void PlayAnimation(string animationName)
    {
        if (_animator != null)
        {
            _animator.Play(animationName);
        }
    }
    #endregion
}