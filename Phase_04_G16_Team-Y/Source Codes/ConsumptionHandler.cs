using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ConsumptionHandler : MonoBehaviour
{
    public string apiKey = "NjVkNDIyMjNmMjc3NmU3OTI5MWJmZGI1OjY1ZDQyMjIzZjI3NzZlNzkyOTFiZmRhYg";
    public string loginEndpoint = "http://20.15.114.131:8080/api/login";
    public string consumptionEndpoint = "http://20.15.114.131:8080/api/power-consumption/current/view";
    private string token;

    public float cloud_range = 3f;  // Public variable to be accessed by other scripts

    private float previousConsumption = -1;  // Initialize to -1 to indicate no previous value
    private const float checkInterval = 10f;  // Interval to check the API value

    void Start()
    {
        // Start the authentication process
        StartCoroutine(GetToken());
    }

    // Method to fetch JWT token
    public IEnumerator GetToken()
    {
        using (UnityWebRequest request = UnityWebRequest.Post(loginEndpoint, new WWWForm()))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes("{\"apiKey\": \"" + apiKey + "\"}");
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // Extract token from JSON response
                string jsonResponse = request.downloadHandler.text;
                TokenResponse response = JsonUtility.FromJson<TokenResponse>(jsonResponse);
                token = response.token;
                Debug.Log("Token fetched: " + token);

                // Start fetching power consumption data
                StartCoroutine(FetchConsumptionData());
            }
            else
            {
                Debug.LogError("Failed to fetch token: " + request.error);
            }
        }
    }

    // Method to fetch power consumption data
    private IEnumerator FetchConsumptionData()
    {
        while (true)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(consumptionEndpoint))
            {
                request.SetRequestHeader("Authorization", "Bearer " + token);
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    string jsonResponse = request.downloadHandler.text;
                    ConsumptionResponse response = JsonUtility.FromJson<ConsumptionResponse>(jsonResponse);


                    // Calculate rate of change if previous value exists
                    if (previousConsumption >= 0)
                    {
                        float rateOfChange = response.currentConsumption - previousConsumption;
                        UpdateCloudRange(rateOfChange);

                    }

                    // Update previous consumption value

                    previousConsumption = response.currentConsumption;
                }
                else
                {
                    Debug.Log("Failed to fetch consumption data: ");
                }

                // Wait for the defined interval before the next request
                yield return new WaitForSeconds(checkInterval);
            }
        }
    }

    // Method to update cloud_range based on rate of change
    private void UpdateCloudRange(float rateOfChange)
    {
        float absRate = Mathf.Abs(rateOfChange);  // Use absolute value for mapping
        if (absRate >= 0 && absRate <= 0.25f)
        {
            cloud_range = 4;
        }
        else if (absRate > 0.25f && absRate <= 0.5f)
        {
            cloud_range = 3.5f;
        }
        else if (absRate > 0.5f && absRate <= 0.75f)
        {
            cloud_range = 3;
        }
        else if (absRate > 0.75f && absRate <= 1f)
        {
            cloud_range = 2.5f;
        }
        else if (absRate > 1f && absRate <= 1.25f)
        {
            cloud_range = 2;
        }
        else if (absRate > 1.25f && absRate <= 1.5f)
        {
            cloud_range = 1.5f;
        }
        else if (absRate > 1.5f)
        {
            cloud_range = 1;
        }
        else
        {
            cloud_range = 1;  // Set to a minimum range value for safety
        }
        Debug.Log("Updated Cloud Range: " + cloud_range);
    }

    private class TokenResponse
    {
        public string token;
    }

    private class ConsumptionResponse
    {
        public float currentConsumption;
    }
}
