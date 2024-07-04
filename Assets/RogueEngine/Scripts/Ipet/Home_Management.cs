using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Home_Management : MonoBehaviour
{
    public GameObject Hunger_num;
    public GameObject Energy_num;
    public GameObject Name_pet;
    private Bird bird; 

 
    void Start()
    {
    
        bird = FindObjectOfType<Bird>(); 
        if (bird == null)
        {
            Debug.LogError("Bird character not found in the scene.");
            return;
        }

        SetPetName();   
        UpdateHungerDisplay();
        UpdateEnergyDisplay();
        StartCoroutine(DecreaseHungerRoutine());
        StartCoroutine(DecreaseEnergyRoutine());

    }

   
    void Update()
    {

        UpdateEnergyDisplay();
        UpdateHungerDisplay();
    }

    void UpdateHungerDisplay()
    {
        if (bird != null && Hunger_num != null)
        {
           
            Hunger_num.GetComponent<Text>().text = bird.GetHunger().ToString();
        }
    }
    void UpdateEnergyDisplay()
    {
        if (bird != null && Hunger_num != null)
        {

            Energy_num.GetComponent<Text>().text = bird.GetEnergy().ToString();
        }
    }
    
    void SetPetName()
    {
        if (bird != null && Name_pet != null)
        {

            Name_pet.GetComponent<Text>().text = bird.GetName().ToString();
        }
    }
    private IEnumerator DecreaseHungerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            if (bird != null)
            {
                bird.SetHunger(bird.GetHunger() - 1); 
                UpdateHungerDisplay(); 
            }
        }
    }
    private IEnumerator DecreaseEnergyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            if (bird != null)
            {
                bird.SetEnergy(bird.GetEnergy() - 5);
                UpdateEnergyDisplay(); 
            }
        }
    }
 

  
    public void IncreaseHunger()
    {
        if (bird != null)
        {
            bird.FeedIpet(); 
            UpdateHungerDisplay();
        }
    }
}
