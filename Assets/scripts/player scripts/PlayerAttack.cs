using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager wm;
    public float fireRate = 15;
    private float nextTimeToFire;
    public float damage = 20;

    private void Awake()
    {
        wm = GetComponent<WeaponManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WeaponShoot();
    }

    void WeaponShoot()
    {
        if(Input.GetMouseButton(0) && wm.GetCurrentWeapon().fireType == WeaponFireType.MULTIPLE)
        {
            if(Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                wm.GetCurrentWeapon().ShootAnimation();
            }
        }
        if (!Input.GetMouseButtonDown(0)) return;
        if (wm.GetCurrentWeapon().tag == Tags.AXE_TAG)
        {
            wm.GetCurrentWeapon().ShootAnimation();
        } else if (wm.GetCurrentWeapon().bulletType == WeaponBulletType.BULLET)
        {
            wm.GetCurrentWeapon().ShootAnimation();
            // BulletFired();
        } else
        {
            // spear or arrow
        }
    }
}
