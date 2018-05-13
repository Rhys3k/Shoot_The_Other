using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletScript : NetworkBehaviour
{
    [HideInInspector] public int bulletDamage;

    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Player")
        {
            col.collider.GetComponent<PlayerController>().GetDamage(bulletDamage);  
        }
        Destroy(this.gameObject);
    }
}
