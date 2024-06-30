using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Character_IPet
{
   

    void Start()
    {
        // Any additional setup if needed
    }

    void Update()
    {
        // Add any additional updates here
    }

    public void InitializeCharacter(string name, string species, string gender, int health, int hunger, int happiness, int energy, string owner)
    {
        SetName(name);
        SetSpecies(species);
        SetGender(gender);
        SetHealth(health);
        SetHunger(hunger);
        SetHappiness(happiness);
        SetEnergy(energy);
        SetOwner(owner);
    }

    public void SayJibJib()
    {
        Debug.Log("Jib! Jib!");
    }

    public void FeedIpet()
    {
        SetHunger(GetHunger() + 5);
    }

    public void GetHungerPet()
    {
        GetHunger();
    }
}
