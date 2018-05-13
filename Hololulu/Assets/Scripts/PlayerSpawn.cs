using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSpawn : NetworkBehaviour
{
    private NetworkStartPosition[] spawnPoints;

    void Start()
    {
        if (isLocalPlayer)
        {
        spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
    }
    
    [ClientRpc]
    public void RpcRespawn()
    {
        Vector3 spawnpoint = Vector3.zero;

        if(spawnPoints != null && spawnPoints.Length > 0)
        {
            spawnpoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
        }

        transform.position = spawnpoint;
    } 
    
}