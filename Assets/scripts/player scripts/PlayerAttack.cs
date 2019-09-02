using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager wm;
    public float fireRate = 15;
    private float nextTimeToFire;
    public float damage = 20;

    private Animator ZoomCameraAnim;
    private bool zoom, isAiming;
    private Camera mainCam;
    private GameObject crossHair;
    [SerializeField]
    private GameObject arrowPrefab;
    [SerializeField]
    private GameObject spearPrefab;
    [SerializeField]
    private Transform arrowBowStartPos;

    private void Awake()
    {
        wm = GetComponent<WeaponManager>();
        // FIX: this works unlike the transform crazyness
        ZoomCameraAnim = GameObject.FindWithTag(Tags.ZOOM_CAMERA)
            .GetComponent<Animator>();
        //ZoomCameraAnim = transform.Find(Tags.LOOK_ROOT).transform.
        //    Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();
        crossHair = GameObject.FindWithTag(Tags.CROSSHAIR);
        mainCam = Camera.main; 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WeaponShoot();
        Zoom();
    }

    void WeaponShoot()
    {
        if(Input.GetMouseButton(0) && wm.GetCurrentWeapon().fireType == WeaponFireType.MULTIPLE)
        {
            if(Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                wm.GetCurrentWeapon().ShootAnimation();
                BulletFired();
            }
        }
        if (!Input.GetMouseButtonDown(0)) return;
        if (wm.GetCurrentWeapon().tag == Tags.AXE_TAG)
        {
            wm.GetCurrentWeapon().ShootAnimation();
        } else if (wm.GetCurrentWeapon().bulletType == WeaponBulletType.BULLET)
        {
            wm.GetCurrentWeapon().ShootAnimation();
            BulletFired();
        } else
        {
            // spear or arrow
            if(isAiming)
            {
                wm.GetCurrentWeapon().ShootAnimation();
                if(wm.GetCurrentWeapon().bulletType == WeaponBulletType.ARROW)
                {
                    ThrowArrowOrSpear(true);
                } else if (wm.GetCurrentWeapon().bulletType == WeaponBulletType.SPEAR)
                {
                    ThrowArrowOrSpear(false);
                }
            }
        }
    }

    void Zoom()
    {
        if (wm.GetCurrentWeapon().weaponAim == WeaponAim.AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                ZoomCameraAnim.Play(AnimationTags.ZOOM_IN_ANIM);
                crossHair.SetActive(false);
            }
            else if (Input.GetMouseButtonUp(1))
            {
                ZoomCameraAnim.Play(AnimationTags.ZOOM_OUT_ANIM);
                crossHair.SetActive(true);
            }
        }
        else if (wm.GetCurrentWeapon().weaponAim == WeaponAim.SELF_AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                wm.GetCurrentWeapon().Aim(true);
                isAiming = true;
            } else if (Input.GetMouseButtonUp(1))
            {
                wm.GetCurrentWeapon().Aim(false);
                isAiming = false;
            }
        }
    }

    void ThrowArrowOrSpear(bool isArrow)
    {
        GameObject projectile = isArrow ? Instantiate(arrowPrefab): Instantiate(spearPrefab);
        projectile.transform.position = arrowBowStartPos.position;
        projectile.GetComponent<ArrowBow>().Launch(mainCam);
    }

    void BulletFired()
    {
        RaycastHit hit;
        if(Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit))
        {
            print("ng: " + hit.transform.gameObject.name);
            if(hit.transform.tag == Tags.ENEMY_TAG)
			{
				hit.transform.GetComponent<Health>().ApplyDamage(damage);
			}
        }
    }
}
