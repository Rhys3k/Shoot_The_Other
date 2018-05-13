using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Weapon : NetworkBehaviour
{
    [SerializeField] protected GameObject bullet;

    [SerializeField] protected string name;
    [SerializeField] protected int dmg;
    [SerializeField] protected int speed = 1000;
    [SerializeField] public int maxAmmo;
    [SerializeField] public int curAmmo;
    [SerializeField] protected int reloadTime;
    [SerializeField] protected float delay;
    [SerializeField] protected List<Transform> bulletSpawnPoints;
    [SerializeField] public Text ammoText;

    public float Delay { get { return delay; } }
    public int Dmg { get { return dmg; } }

    private float reloadDelay = 0f;
    private bool isReloading = false;

    public virtual void Shoot()
    {
        Vector3 pos = new Vector3(bulletSpawnPoints[0].position.x, bulletSpawnPoints[0].position.y, bulletSpawnPoints[0].position.z);

        GameObject bulletObj = Instantiate(bullet, pos, Quaternion.identity);
        bulletObj.GetComponent<BulletScript>().bulletDamage = dmg;

        bulletObj.transform.rotation = gameObject.transform.parent.transform.rotation;
        bulletObj.GetComponent<Rigidbody>().AddForce(bulletObj.transform.forward * speed);

        NetworkServer.Spawn(bulletObj);

        ChangeAmmo(-1);
    }

    protected virtual void ChangeAmmo(int ammoAmount)
    {
        curAmmo += ammoAmount;
        ammoText.text = curAmmo+" / "+maxAmmo;
       
    }

    public virtual void AmmoReloadCheck()
    {
        if (curAmmo > 0 && !isReloading)
        {
            Shoot();
        }
        else
            WeaponReload();
    }

    public virtual void WeaponReload()
    {
        if (isReloading != true )
        {
            reloadDelay = Time.time + reloadTime;
            isReloading = true;
            Debug.Log("Start reloading..." +(Time.time+ + reloadTime));
        }
    }

    protected virtual void Start()
    {
        ChangeAmmo(maxAmmo);
    }

    protected virtual void Update()
    {
        Debug.Log(isReloading + "   " + (Time.time >= reloadDelay));
        if(isReloading && Time.time >= reloadDelay)
        {
            int reloadAmmo = maxAmmo - curAmmo;
            ChangeAmmo(reloadAmmo);
            isReloading = false;
            Debug.Log("Reloading finished!");
        }
    }
}
