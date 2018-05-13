using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour {

    public float walkspeed;
    public bool lookAtPlayer;
    public int playerId;
    public int maxHp;
    [SyncVar (hook = "OnChangedHealth")] public int curHp;
    public Text playerHp;

    private float xAxis;
    private float zAxis;
    private Vector3 curserCheck;
    private Vector3 dir;
    private PlayerSpawn playerSpawn;
    Rigidbody rigid;

    private Weapon weapon;
 
    void Start ()
    {
        curHp = maxHp;
        playerHp.text = "HP: " + curHp + " / " + maxHp;
        rigid = GetComponent<Rigidbody>();
	}

    public override void OnStartLocalPlayer()
    {
        GetComponentInChildren<Canvas>().enabled = true;
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (lookAtPlayer == true)
        {
            CheckMouse();
        }
    }

    void FixedUpdate ()
    {
        //xAxis = Input.GetAxis("Horizontal") * Time.deltaTime * walkspeed;
        //zAxis = Input.GetAxis("Vertical") * Time.deltaTime * walkspeed;

        //transform.Translate(xAxis, 0, zAxis); 
        if (hasAuthority)
        {
            rigid.velocity = new Vector3(Mathf.Lerp(0, Input.GetAxis("Horizontal") * walkspeed, 0.8f), 0, Mathf.Lerp(0, Input.GetAxis("Vertical") * walkspeed, 0.8f));
        } 
    }

    void CheckMouse()
    {
        if (hasAuthority)
        {
            curserCheck = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dir = curserCheck;
            transform.LookAt(new Vector3(dir.x, transform.position.y, dir.z));
        }
    }

    public void GetDamage(int dmg)
    {
        if (!isServer)
        {
            return;
        }

        curHp = curHp - dmg;
        

        if (curHp <= 0)
        {
            IsDying();
        }
    }

    private void OnChangedHealth(int hp)
    {
        playerHp.text = "HP: " + hp + " / " + maxHp;
    }

    void IsDying()
    {
        Destroy(this.gameObject);
        //playerSpawn.RespawnPlayer();
    }
}
