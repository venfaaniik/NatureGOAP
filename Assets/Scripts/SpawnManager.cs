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
    }
}








