using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private WeaponHandler[] weapons;
    private int weaponIdx;

    // Start is called before the first frame update
    void Start()
    {
		weapons[0].gameObject.SetActive(true);
	}

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            enableWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            enableWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            enableWeapon(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            enableWeapon(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            enableWeapon(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            enableWeapon(5);
        }
    }

    void enableWeapon(int idx)
    {
        if (weaponIdx == idx) return;
        weapons[weaponIdx].gameObject.SetActive(false);
        weapons[idx].gameObject.SetActive(true);
        weaponIdx = idx;
    }

    public WeaponHandler GetCurrentWeapon()
    {
        return weapons[weaponIdx];
    }
}
