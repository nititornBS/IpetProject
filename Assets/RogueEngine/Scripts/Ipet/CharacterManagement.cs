using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManagement : MonoBehaviour
{
    private List<Character_IPet> characters = new List<Character_IPet>();
    private Bird bird;

    void Start()
    {
        // Create and initialize a Bird character
        bird = CreateCharacter<Bird>();
        bird.InitializeCharacter("Tweety", "Canary", "Female", 100, 50, 80, 90, "Alice");

     
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
