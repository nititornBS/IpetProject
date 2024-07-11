using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Home_Management : MonoBehaviour
{
    public GameObject Hunger_num;
    public GameObject Energy_num;
    public GameObject Name_pet;
    private Bird bird;
    public GameObject Api_text;
    private string apiUrl = "https://z41di2.buildship.run/hello";

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

    IEnumerator GetDataFromAPI()
    {
        using (UnityWebRequest webRequest = new UnityWebRequest(apiUrl, "POST"))
        {
            // Create a JSON object to send in the body
            Dictionary<string, string> jsonBody = new Dictionary<string, string>
            {
                { "parameter1", "value1" },
                { "parameter2", "value2" }
            };

            // Convert the JSON object to a byte array
            string jsonData = JsonUtility.ToJson(jsonBody);
            byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonData);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            // Send the request and wait for the response
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string responseText = webRequest.downloadHandler.text;
                Api_text.GetComponent<Text>().text = responseText;
                Debug.Log("Response: " + responseText);
            }
        }
    }
}
