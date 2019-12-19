//using System;
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    enum animal
    {
        Deer,
        Wolf
    };

    void SpawnCreature(Vector3 creaturePosition, animal animal)
    {
<<<<<<< HEAD
        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        //spawnpoint -> pitää kattoo ettei spawnaa sitä muitten päälle?? vai onko colliders
        //Instantiate(Creature, SpawnPoint.position);
=======
        //Instantiate(creature, /*creature position*/, Quaternion.identity);
        GameObject go;
        switch (animal)
        {
            case animal.Deer:
                go = new GameObject("Deer");
                Instantiate(go, creaturePosition, Quaternion.identity);
                go.AddComponent<AStarUnit>();

                break;
            case animal.Wolf:
                go = new GameObject("Wolf");
                Instantiate(go, creaturePosition, Quaternion.identity);
                go.AddComponent<AStarUnit>();
                break;
        }
>>>>>>> de9a162b074c4fe5dd6d6acb9379fe3d73563737
    }
}








