using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomSpawn : Photon.MonoBehaviour
{
    public int numberIndice;
    public int numberDague;
    public List<GameObject> spawnPool;
    public List<GameObject> spawnPosition;
    public List<GameObject> spawnPositionC;
    public static List<GameObject> dagueSurMap;
    public float Radius = 10;
   
    public void spawnObject()
    {
        if (PlayerPrefs.GetInt("gamecreated",0) == 1)
        {
            dagueSurMap = new List<GameObject>();
            int randomPosition;
            GameObject toSpawn;
            Vector2 pos;
            for (int i = 0; i < numberIndice; i++)
            {
                toSpawn = spawnPool[0];
                randomPosition  = Random.Range(0, spawnPosition.Count);
                pos = spawnPosition[randomPosition].transform.position;
                pos.x += Random.Range(-Radius, Radius);
                pos.y += Random.Range(-Radius, Radius);
                Debug.Log(toSpawn.name);
                PhotonNetwork.Instantiate(toSpawn.name, pos, quaternion.identity,0);
                spawnPosition.Remove(spawnPosition[randomPosition]);
            }
            for (int i = 0; i < numberDague; i++)
            {
                toSpawn = spawnPool[1];
                randomPosition  = Random.Range(0, spawnPositionC.Count);
                pos = spawnPositionC[randomPosition].transform.position;
                pos.x += Random.Range(-Radius, Radius);
                pos.y += Random.Range(-Radius, Radius);
                var spawned =  PhotonNetwork.Instantiate(toSpawn.name, pos, quaternion.identity, 0);
                spawnPositionC.Remove(spawnPositionC[randomPosition]);
            }
        }
    }

    private void OnDrawGizmos()
    {
         Gizmos.color = Color.green;
         Gizmos.DrawWireSphere(this.transform.position,Radius); 
    }
    
}
