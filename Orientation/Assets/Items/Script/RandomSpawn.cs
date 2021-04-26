﻿using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public int numberIndice;
    public int numberDague;
    public List<GameObject> spawnPool;
    public List<GameObject> spawnPosition;
    public List<GameObject> spawnPositionC;

    public float Radius = 10;
    void Start()
    {
    }

    public void spawnObject()
    {
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
            Instantiate(toSpawn, pos, quaternion.identity);
            spawnPosition.Remove(spawnPosition[randomPosition]);
        }
        for (int i = 0; i < numberDague; i++)
        {
            toSpawn = spawnPool[1];
            randomPosition  = Random.Range(0, spawnPositionC.Count);
            pos = spawnPositionC[randomPosition].transform.position;
            pos.x += Random.Range(-Radius, Radius);
            pos.y += Random.Range(-Radius, Radius);
            Instantiate(toSpawn, pos, quaternion.identity);
            spawnPositionC.Remove(spawnPositionC[randomPosition]);
        }
    }

    private void OnDrawGizmos()
    {
         Gizmos.color = Color.green;
         Gizmos.DrawWireSphere(this.transform.position,Radius); 
    }
    

}
