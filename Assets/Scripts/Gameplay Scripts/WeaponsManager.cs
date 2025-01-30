using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponsManager : MonoBehaviour
{
    [SerializeField]
    Gun[] guns;
    [SerializeField]
    AmmoDisplay _ammoDisplay;
    int currIndex = 0;

    public void Start()
    {
        if (guns != null) 
        {
            guns[currIndex].TriggerEquip();
            _ammoDisplay.magSize = guns[currIndex].magSize;
            for (int i = 1; i < guns.Length; i++)
            {
                guns[i].gameObject.SetActive(false);
            }
            _ammoDisplay.DisplayGunIcon(guns[1%guns.Length]);
        }
    }

    public void SwitchGun()
    {
        int nextIndex = (currIndex + 1) % guns.Length;
        if (guns[nextIndex] != null)
        {
            guns[currIndex].TriggerEquip();
            guns[nextIndex].gameObject.SetActive(true);
            guns[nextIndex].TriggerEquip();
            currIndex = nextIndex;
            _ammoDisplay.magSize = guns[currIndex].magSize;
            _ammoDisplay.DisplayGunIcon(guns[currIndex + 1 % guns.Length]);
        }
    }

    public Gun GetCurrentGun()
    {
        return (guns[currIndex]);
    }
}
