using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    public Pistol()
    {
        damage = 40f;
        reloadTime = 1.5f;
        fireTime = 0.4f;
        magSize = 7;
        currAmmo = 7;
        bspeed = 4;
        reloadAnim = "ReloadPistol";
        shootAnim = "ShootPistol";
    }
}
