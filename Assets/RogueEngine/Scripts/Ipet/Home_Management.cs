using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Home_Management : MonoBehaviour
{
    public GameObject Hunger_num;
    private Bird bird; 

 
    void Start()
    {
    
        bird = FindObjectOfType<Bird>(); 
        if (bird == null)
        {
            Debug.LogError("Bird character not found in the scene.");
            return;
        }


        UpdateHungerDisplay();
        StartCoroutine(DecreaseHungerRoutine());
    }

   
    void Update()
    {
        
      
        UpdateHungerDisplay();
    }

    void UpdateHungerDisplay()
    {
        if (bird != null && Hunger_num != null)
        {
           
            Hunger_num.GetComponent<Text>().text = bird.GetHunger().ToString();
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

  
    public void IncreaseHunger()
    {
        if (bird != null)
        {
            bird.FeedIpet(); 
            UpdateHungerDisplay();
        }
    }
}
