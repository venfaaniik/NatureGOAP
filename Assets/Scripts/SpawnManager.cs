//using System;
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    GameObject plant;               // Plantin prefab joka spawnataan
    GameObject creature;

    Vector3 plantPosition;
    bool is_dead;

    [Range(5f, 180f)] [Tooltip("Aika, jonka jälkeen kasvit tulevat takaisin kuoleman porttien takaa.")]
    public float spawnTime = 30f;            // Spawnväli esim kasvien kanssa
     
    public struct creationRequest {
        Vector3 creatureSpawnPoint;         // Kohta mihi laitetaan creature
    }

    private void Start()
    {
        //  Toistaa tätä hamaan ikuisuuteen
        InvokeRepeating("RespawnPlant", spawnTime, spawnTime);
    }

    public void RespawnPlant(GameObject plant, Vector3 plantPosition)
    {
        if (is_dead)
        {
            Instantiate(plant, plantPosition, Quaternion.identity);
        }
    }

    public void SpawnCreature()
    {
        //Instantiate(creature, /*creature position*/, Quaternion.identity);
    }
}
