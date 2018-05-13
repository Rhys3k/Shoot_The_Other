using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WeaponController : NetworkBehaviour
{
    [SerializeField] private List<GameObject> weapons;
 
    private Weapon activeWeapon;

    private float shootDelay = 0f;
    private int currIndex = 1;

    public Text weaponName;

	void Start ()
    {
        ChangeWeapon(1);
	}
	
	void Update ()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= shootDelay)
        {
            shootDelay = Time.time + activeWeapon.Delay;

            CmdShoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        { 
            activeWeapon.WeaponReload();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeWeapon(2);
        }
    }

    private void ChangeWeapon(int index)
    {
        weapons[currIndex-1].SetActive(false);

        weapons[index-1].SetActive(true);

        activeWeapon = weapons[index-1].GetComponent<Weapon>();
        currIndex = index;

        if(weaponName!=null && activeWeapon!=null)
        weaponName.text = activeWeapon.name;
        //activeWeapon.ammoText.text = activeWeapon.curAmmo + " / " + activeWeapon.maxAmmo;
    }

    [Command]
    private void CmdShoot()
    {
        activeWeapon.AmmoReloadCheck();
    }
}
