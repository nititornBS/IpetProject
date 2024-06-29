using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManagement : MonoBehaviour
{
    private List<Character_IPet> characters = new List<Character_IPet>();
    private Bird bird; // Declare bird variable

    void Start()
    {
        // Create a Bird character
        bird = CreateCharacter<Bird>();
        bird.SetName("Tweety");
        bird.SetSpecies("Canary");
        bird.SetGender("Female");
        bird.SetHealth(100);
        bird.SetHunger(50);
        bird.SetHappiness(80);
        bird.SetEnergy(90);
        bird.SetOwner("Alice");

        // Example interaction with the bird
        Speak();
        FeedIpet();
        Debug.Log("Bird's Hunger after feeding: " + bird.GetHunger());
    }

    public void Speak()
    {
        bird.SayJibJib();
    }

    public void FeedIpet()
    {
        bird.FeedIpet();
    }

    public T CreateCharacter<T>() where T : Character_IPet
    {
        GameObject characterObject = new GameObject(typeof(T).Name);
        T character = characterObject.AddComponent<T>();
        characters.Add(character);
        return character;
    }
}
