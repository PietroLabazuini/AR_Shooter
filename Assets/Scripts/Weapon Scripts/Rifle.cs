using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Gun
{
    public Rifle()
    {
        damage = 15f;
        reloadTime = 1.5f;
        fireRate = 600f;
        magSize = 25;
        currAmmo = 25; 
        bspeed = 5;
        reloadAnim = "ReloadRifle";
        shootAnim = "ShootRifle";
        equipAnim = "EquipRifle";
        unequipAnim = "UnequipRifle";
    }
}
