using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponsManager : MonoBehaviour
{
    [SerializeField]
    AmmoDisplay _ammoDisplay;

    public Gun[] guns;

    int currIndex = 0;
    int gun_nbr;

    public void Start()
    {
        guns = new Gun[4];
        GameObject[] gunsObjects = GameObject.FindGameObjectsWithTag("Weapon");
        gun_nbr = gunsObjects.Length;

        for (int i = 0; i < gun_nbr; i++) 
        {
            Gun gun = gunsObjects[i].GetComponent<Gun>();
            if (gun != null)
            {
                guns[i] = gun;
            }
        }
        
        if (guns != null) 
        {
            guns[currIndex].TriggerEquip();
            _ammoDisplay.magSize = guns[currIndex].magSize;
            _ammoDisplay.DisplayGunIcon(guns[1%gun_nbr]);
        }
    }

    public void SwitchGun()
    {
        int nextIndex = (currIndex + 1) % gun_nbr;
        if (guns[nextIndex] != null)
        {
            guns[currIndex].TriggerEquip();
            guns[nextIndex].TriggerEquip();
            currIndex = nextIndex;
            _ammoDisplay.magSize = guns[currIndex].magSize;
            _ammoDisplay.DisplayGunIcon(guns[(currIndex + 1) % gun_nbr]);
        }
    }

    public Gun GetCurrentGun()
    {
        return (guns[currIndex]);
    }
}
