using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Gun
{
    public Rifle()
    {
        damage = 15f;
        reloadTime = 1.5f;
        fireTime = 0.10f;
        magSize = 25;
        currAmmo = 25; 
        bspeed = 5;
        reloadAnim = "ReloadRifle";
        shootAnim = "ShootRifle";
    }
}
