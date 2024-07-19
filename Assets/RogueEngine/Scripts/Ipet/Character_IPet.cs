using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RogueEngine
{
[CreateAssetMenu (fileName = "CreatePet" , menuName = "TcgEngine/Pet/CreatePet" ,  order = 1)]
public class Character_IPet : ScriptableObject
{
    public string Name;
    private string species;
    private string gender;
    private int health;
    private int hunger;
    private int happiness;
    private int energy;
    private string owner;
    private string trait;
    private bool isDead = false;
    void Start()
    {
       
    }

    // Update is called once per frame
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

    public void SetTrait(string value)
    {
        trait = value;
    }
    public string GetTrait()
    {
        return trait;
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
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetHunger(int value)
    {
        hunger = value;
    }

    public int GetHunger()
    {
        return hunger;
    }

    public void SetHappiness(int value)
    {
        happiness = Mathf.Clamp(value, 0, 100);
    }

    public int GetHappiness()
    {
        return happiness;
    }

    public void SetEnergy(int value)
    {
        energy = value;
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

    
}
}