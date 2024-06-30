using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petstatus : MonoBehaviour
{

    private int happiness;
    private int hunger;
    private int energy;
    private int health;
  

    // Methods to update and check status
    void UpdateHappiness(int value)
    {
        happiness = Mathf.Clamp(happiness + value, 0, 100);
        CheckMoodEffects();
    }

    void UpdateHunger(int value)
    {
        hunger = Mathf.Clamp(hunger + value, 0, 100);
        CheckHungerEffects();
    }

    void UpdateEnergy(int value)
    {
        energy = Mathf.Clamp(energy + value, 0, 100);
        CheckEnergyEffects();
    }

    void UpdateHealth(int value)
    {
        health = Mathf.Clamp(health + value, 0, 100);
        CheckHealthEffects();
    }

    void CheckMoodEffects()
    {
        if (happiness < 30)
        {
            // Apply sad effects
        }
        else if (happiness > 60)
        {
            // Apply happy effects
        }
        // Apply boredom effects if needed
    }

    void CheckHungerEffects()
    {
        if (hunger < 20)
        {
            // Apply very hungry effects
        }
        else if (hunger > 70)
        {
            // Apply full effects
        }
        // Apply foodie trait effects if needed
    }

    void CheckEnergyEffects()
    {
        if (energy < 20)
        {
            // Apply very tired effects
        }
        else if (energy < 40)
        {
            // Apply tired effects
        }
    }

    void CheckHealthEffects()
    {
        if (health < 15)
        {
            // Apply critical health effects
        }
        // Apply sickness effects if needed
    }
}

