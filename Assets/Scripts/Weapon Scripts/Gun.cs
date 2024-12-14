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

    protected int currAmmo;
    protected int magSize;
    float alphaFactor = 1f;

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

        if(currAmmo < 0.3f * magSize)
        {
            Debug.Log("Low Ammo");
            if(currAmmo == 0)
            {
                //Debug.Log(_ammoDisplay.color.a);
                if (_ammoDisplay.color.a <= 0) { alphaFactor = 1f; }
                else { if (_ammoDisplay.color.a >= 1) { alphaFactor = -1f; } }
                float newAlpha = _ammoDisplay.color.a + (0.02f* alphaFactor);
                _ammoDisplay.color = new Color(1,0,0,newAlpha);
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

    protected virtual void PlayAnimation(string animationName)
    {
        if (_animator != null)
        {
            _animator.Play(animationName);
        }
    }

    private bool isReloading = false;

    public void Reload()
    {
        if (!isReloading) // Vérifie pour éviter d'appeler plusieurs fois le rechargement
        {
            reload = true;
        }
    }

    protected IEnumerator ShootCoroutine()
    {
        while (true)
        {
            // Priorité au rechargement
            if (reload)
            {
                isReloading = true; // Bloque les autres actions pendant le rechargement
                PlayAnimation(reloadAnim);
                yield return new WaitForSeconds(reloadTime);
                currAmmo = magSize;
                reload = false;
                isReloading = false; // Réautorise les actions
            }
            else if (!isReloading)
            {
                // Gestion des tirs
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
                        // Jouer un son ou notifier que les munitions sont vides
                        //PlayReloadSound();
                        yield return null;
                    }
                }
            }

            yield return null;
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