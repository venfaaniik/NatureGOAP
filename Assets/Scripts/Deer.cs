using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : Creature
{

    // what is this v
    public Deer(float _hunger, float _thirst, float _breeding_urge, float _max_speed, float _energy, float _sight_radius, float _determinedness)
        : base(_hunger, _thirst, _breeding_urge, _max_speed, _energy, _sight_radius, _determinedness)
    { }


    // Start is called before the first frame update
    void Start()
    {
        energy = 512;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

        // what even is this v
        hunger += (0.001f - (energy / 2048) / 1000) * dt;
        thirst += 0.0015f * dt;
        energy -= 0.1f * dt;

        hunger = Mathf.Clamp01(hunger);
        thirst = Mathf.Clamp01(thirst);

        if (thirst >= 1.0f || hunger >= 1.0f)
        {
            Die();
        }

        Wander();



    }

}
