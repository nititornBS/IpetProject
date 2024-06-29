using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Home_Management : MonoBehaviour
{
    public GameObject Hunger_num;
    private Bird bird; // Assuming Bird is a subclass of Character_IPet and is accessible

    // Start is called before the first frame update
    void Start()
    {
        // Example: Assuming you have a way to access or instantiate your Bird character
        bird = FindObjectOfType<Bird>(); // Find the Bird character in the scene
        if (bird == null)
        {
            Debug.LogError("Bird character not found in the scene.");
            return;
        }

        // Example: Display initial hunger value
        UpdateHungerDisplay();
        StartCoroutine(DecreaseHungerRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
        // Example: Update hunger display continuously
        UpdateHungerDisplay();
    }

    void UpdateHungerDisplay()
    {
        if (bird != null && Hunger_num != null)
        {
            // Update UI or other display with the current hunger value
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

    // Example method to increase hunger
    public void IncreaseHunger()
    {
        if (bird != null)
        {
            bird.SetHunger(bird.GetHunger() + 10); // Example: Increase hunger by 10
            UpdateHungerDisplay(); // Update UI after changing hunger
        }
    }
}
