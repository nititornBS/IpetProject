using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_IPet : MonoBehaviour
{
    // Start is called before the first frame update
     private int Hunger = 50;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetHunger(int value)
    {
        Hunger = Mathf.Clamp(value, 0, 100);
    }

    public int GetHunger()
    {
        return Hunger;
    }
}
