using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RogueEngine
{
public class CharacterManagement : MonoBehaviour
{
    private List<ScriptableObject> characters = new List<ScriptableObject>();
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
        bird.EatFood();
    }

    public T CreateCharacter<T>() where T : ScriptableObject 
    {
        GameObject characterObject = new GameObject(typeof(T).Name);
        T character = ScriptableObject.CreateInstance<T>();
        characters.Add(character);
        return character;
    }
}

}
