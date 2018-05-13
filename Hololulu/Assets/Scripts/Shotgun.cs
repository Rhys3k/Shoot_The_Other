using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shotgun : Weapon
{
    public override void Shoot()
    {
        Vector3 pos1 = new Vector3(bulletSpawnPoints[0].position.x, bulletSpawnPoints[0].position.y, bulletSpawnPoints[0].position.z);
        Vector3 pos2 = new Vector3(bulletSpawnPoints[1].position.x, bulletSpawnPoints[1].position.y, bulletSpawnPoints[1].position.z);
        Vector3 pos3 = new Vector3(bulletSpawnPoints[2].position.x, bulletSpawnPoints[2].position.y, bulletSpawnPoints[2].position.z);

        GameObject bulletObj1 = Instantiate(bullet, pos1, Quaternion.identity);
        bulletObj1.GetComponent<BulletScript>().bulletDamage = dmg;
        GameObject bulletObj2 = Instantiate(bullet, pos2, Quaternion.identity);
        bulletObj2.GetComponent<BulletScript>().bulletDamage = dmg;
        GameObject bulletObj3 = Instantiate(bullet, pos3, Quaternion.identity);
        bulletObj3.GetComponent<BulletScript>().bulletDamage = dmg;

        bulletObj1.transform.rotation = gameObject.transform.parent.transform.rotation;
        bulletObj1.GetComponent<Rigidbody>().AddForce(bulletObj1.transform.forward * speed);

        bulletObj2.transform.rotation = gameObject.transform.parent.transform.rotation;
        bulletObj2.GetComponent<Rigidbody>().AddForce(bulletObj2.transform.forward * speed);

        bulletObj3.transform.rotation = gameObject.transform.parent.transform.rotation;
        bulletObj3.GetComponent<Rigidbody>().AddForce(bulletObj3.transform.forward * speed);

        NetworkServer.Spawn(bulletObj1);
        NetworkServer.Spawn(bulletObj2);
        NetworkServer.Spawn(bulletObj3);

        //ChangeAmmo(-1);
    }
}
