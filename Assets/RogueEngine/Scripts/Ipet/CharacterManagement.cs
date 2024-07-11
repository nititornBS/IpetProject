using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;

public class CharacterManagement : MonoBehaviour
{
    public string ID = "1";
    public string Name = "Tweety";
    public string species = "Canary";
    public string gender = "Female";
    public int health = 100;
    public int hunger = 50;
    public int happiness = 90;
    public int energy = 80;
    public string owner = "Alice";
    private List<Character_IPet> characters = new List<Character_IPet>();
    private Bird bird;

    void Start()
    {
        bird = CreateCharacter<Bird>();
        bird.InitializeCharacter(Name, species, gender, health, hunger, happiness, energy, owner);





    }


    public void Speak()
    {
        bird.SayJibJib();
    }



    public T CreateCharacter<T>() where T : Character_IPet
    {
        GameObject characterObject = new GameObject(typeof(T).Name);
        T character = characterObject.AddComponent<T>();
        characters.Add(character);
        return character;
    }
}
