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

        TryShoot();
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
    protected void TryShoot()
    {
        if(isEquipped && !isSwitching && !isReloading)
        {
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (delay <= Time.time)
                        {
                            delay = Time.time;
                            Shoot();
                        }
                        break;
                    case TouchPhase.Stationary:
                        if (delay <= Time.time)
                        {
                            Shoot();
                        }
                        break;
                    case TouchPhase.Ended:
                        _animator.SetBool("isShooting", false);
                        break;
                    default:
                        break;

                }
            }
        }
    }

    private void Shoot()
    {
        //Checking if the gun still has ammo
        if (currAmmo > 0)
        {
            _animator.SetBool("isShooting", true);
            if (Physics.Raycast(_player.transform.position, _player.transform.forward, out RaycastHit hit))
            {
                if (hit.transform.TryGetComponent<Enemy>(out var enemyhit))
                {
                    enemyhit.Damage(damage);
                    Handheld.Vibrate();
                }
            }
            else
            {
                Debug.Log("Didn't hit");
            }
            currAmmo--;
            delay += 60 / fireRate;
            }
        else
        {
            //empty magazine sound
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