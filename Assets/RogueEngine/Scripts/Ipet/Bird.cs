using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Character_IPet
{

    void Start()
    {

    }

    void Update()
    {

    }

    public void SayJibJib()
    {
        Debug.Log("Jib! Jib!");
    }
    public void FeedIpet()
    {
        SetHunger(GetHunger() + 5);
    }
    public void GetHungerPet(){
        GetHunger();
    }


}
