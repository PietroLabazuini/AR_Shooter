using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField]
    protected WeaponsManager _weaponsManager;
    [SerializeField]
    protected Text _ammoDisplay;
    [SerializeField]
    protected Image _weaponIconDisplay;

    float alphaFactor = 1f;
    public int magSize;
    // Update is called once per frame
    void Update()
    {
        DisplayCurrentGunInfo();
    }

    protected void DisplayCurrentGunInfo()
    {
        int currAmmo = _weaponsManager.GetCurrentGun().currAmmo;

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

    public void DisplayGunIcon(Gun nextGun)
    {
        if (_ammoDisplay != null)
        {
            _weaponIconDisplay.sprite = nextGun._icon;
        }
    }
}
