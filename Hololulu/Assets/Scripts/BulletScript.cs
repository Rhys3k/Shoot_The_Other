using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
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
