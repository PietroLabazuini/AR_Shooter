using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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

    protected int currAmmo;
    protected int magSize;

    protected bool reload;
    
    protected string reloadAnim;
    protected string shootAnim;

    protected void Start()
    {
        _gun = gameObject;
        _animator = _gun.GetComponent<Animator>();
        StartCoroutine(ShootCoroutine());
    }

    protected void Update()
    {
        _ammoDisplay.text = currAmmo.ToString();
    }

    protected virtual void PlayAnimation(string animationName)
    {
        if (_animator != null)
        {
            _animator.Play(animationName);
        }
    }

    public void Reload()
    {
        reload = true;
    }

    protected IEnumerator ShootCoroutine()
    {
        while (true)
        {
            yield return null;
            if (reload)
            {
                PlayAnimation(reloadAnim);
                reload = false;
                yield return new WaitForSeconds(reloadTime);
                currAmmo = magSize;
            }
            else
            {
                while (Input.touchCount > 0)
                {
                    if (currAmmo > 0)
                    {
                        PlayAnimation(shootAnim);
                        Shoot();
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
            AnimatorClipInfo[] m_CurrentClipInfo = _animator.GetCurrentAnimatorClipInfo(0);
            if (m_CurrentClipInfo.Length > 0)
            {
                Debug.Log(m_CurrentClipInfo[0].clip.length);
            }
        }
    }

    protected void Shoot()
    {
        if (_bulletSpawnpoint != null)
        {
            GameObject bulletObject = Instantiate(_bulletPrefab, _bulletSpawnpoint.transform);
            bulletObject.transform.parent = null;
            bulletObject.GetComponent<Rigidbody>().velocity = bspeed * -_bulletSpawnpoint.transform.forward;
            Bullet bullet = bulletObject.AddComponent<Bullet>();
            bullet.damage = damage;
           // Destroy(bulletObject, 5f);
        }
    }
}