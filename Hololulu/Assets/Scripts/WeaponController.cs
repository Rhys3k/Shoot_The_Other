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

    //public Text weaponName;

    [SerializeField] protected GameObject bullet;

    [SerializeField] protected string name;
    [SerializeField] protected int dmg;
    [SerializeField] protected int speed = 1000;
    //[SerializeField] public int maxAmmo;
    //[SyncVar (hook = "ChangeAmmo")]
    //[SerializeField] public int curAmmo;
    //[SerializeField] protected int reloadTime;
    [SerializeField] protected float delay;
    [SerializeField] protected List<Transform> bulletSpawnPoints;
    public float Delay { get { return delay; } }
    public int Dmg { get { return dmg; } }


 
    void Start ()
    {
       // ChangeWeapon(1);
	}
	
	void Update ()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= shootDelay)
        {
            shootDelay = Time.time + Delay;
            CmdShoot();
        }

        //if (Input.GetKeyDown(KeyCode.R))
        //{ 
        //    activeWeapon.WeaponReload();
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    ChangeWeapon(1);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    ChangeWeapon(2);
        //}
    }

    [Command]
    public virtual void CmdShoot()
    {
        Vector3 pos = new Vector3(bulletSpawnPoints[0].position.x, bulletSpawnPoints[0].position.y, bulletSpawnPoints[0].position.z);

        GameObject bulletObj = Instantiate(bullet, pos, Quaternion.identity);
        bulletObj.GetComponent<BulletScript>().bulletDamage = dmg;

        bulletObj.transform.rotation = gameObject.transform.rotation;
        bulletObj.GetComponent<Rigidbody>().AddForce(bulletObj.transform.forward * speed);

        //SpawnBulletOnNetwork(bulletObj);
        NetworkServer.Spawn(bulletObj);
        // ChangeAmmo(-1);
    }

    //private void ChangeWeapon(int index)
    //{
    //    weapons[currIndex-1].SetActive(false);

    //    weapons[index-1].SetActive(true);

    //    activeWeapon = weapons[index-1].GetComponent<Weapon>();
    //    currIndex = index;

    //    if(weaponName!=null && activeWeapon!=null)
    //    weaponName.text = activeWeapon.name;
    //    //activeWeapon.ammoText.text = activeWeapon.curAmmo + " / " + activeWeapon.maxAmmo;
    //}

    //private void SpawnBulletOnNetwork(GameObject bulletObj)
    //{
        //NetworkServer.Spawn(bulletObj);
    //}
}
