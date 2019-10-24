using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Creature; // ???

    void SpawnCreature()
    {
        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        //spawnpoint -> pitää kattoo ettei spawnaa sitä muitten päälle?? vai onko colliders
        Instantiate(Creature, SpawnPoint.position);
    }
}
