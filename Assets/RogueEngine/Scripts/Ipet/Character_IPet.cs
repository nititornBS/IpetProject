using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_IPet : MonoBehaviour
{
    private string Name;
    private string species;
    private string gender;
    private int health;
    private int hunger;
    private int happiness;
    private int energy;
    private string owner;

    void Start()
    {
        StartCoroutine(DecreaseHungerRoutine());
        StartCoroutine(DecreaseEnergyRoutine());
    }

    void Update()
    {

    }

    public void SetName(string value)
    {
        Name = value;
    }

    public string GetName()
    {
        return Name;
    }

    public void SetSpecies(string value)
    {
        species = value;
    }

    public string GetSpecies()
    {
        return species;
    }

    public void SetGender(string value)
    {
        gender = value;
    }

    public string GetGender()
    {
        return gender;
    }

    public void SetHealth(int value)
    {
        health = value;
        CheckHealthEffects();
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetHunger(int value)
    {
        hunger = Mathf.Clamp(value, 0, 100);
        CheckHungerEffects();
    }

    public int GetHunger()
    {
        return hunger;
    }

    public void SetHappiness(int value)
    {
        happiness = Mathf.Clamp(value, 0, 100);
        CheckMoodEffects();
    }

    public int GetHappiness()
    {
        return happiness;
    }

    public void SetEnergy(int value)
    {
        energy = Mathf.Clamp(value, 0, 100);
        CheckEnergyEffects();
    }

    public int GetEnergy()
    {
        return energy;
    }

    public void SetOwner(string value)
    {
        owner = value;
    }

    public string GetOwner()
    {
        return owner;
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

    private IEnumerator DecreaseHungerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            SetHunger(GetHunger() - 1);
        }
    }

    private IEnumerator DecreaseEnergyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            SetEnergy(GetEnergy() - 1);
        }
    }
}
