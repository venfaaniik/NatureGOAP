using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    // When this drops too low, send a path request to food?
    public float hunger;
    // When this drops too low, send a path request to water?
    public float thirst;
    // How fast the creature moves
    public float max_speed;
    // Can move fast / wants to sleep
    public float energy;
        
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
