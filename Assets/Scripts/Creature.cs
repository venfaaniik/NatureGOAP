using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    // When this rises too high, send a path request to food? clamp 0-1?
    public float hunger;
    // When this rises too high, send a path request to water? clamp 0-1?
    public float thirst;
    // While this is above hunger and thirst, look for mate
    public float breeding_urge;
    // How fast the creature moves
    public float max_speed;
    // Can move fast / wants to sleep
    public float energy;
    // Vision range, if food/water not in sight, wander around until something in sight
    public float sight_radius;
    // Determinedness, affects how likely the creature changes direction while wandering
    public float determinedness;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Creature(float _hunger, float _thirst, float _breeding_urge, float _max_speed, float _energy, float _sight_radius, float _determinedness)
    {
        hunger = _hunger;
        thirst = _thirst;
        breeding_urge = _breeding_urge;
        max_speed = _max_speed;
        energy = _energy;
        sight_radius = _sight_radius;
        determinedness = _determinedness;
    }

    // get path to water/food?
    public void GetPathToTarget(Vector3 some_place)
    {

    }

    
    public void Wander()
    {

    }

    public void Die()
    {
        //play some death animation? yes
        print("die");
        Destroy(gameObject);
    }

}
