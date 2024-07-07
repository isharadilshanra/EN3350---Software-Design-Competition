using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerAuthentication1 : MonoBehaviour
{
    public string token;

    // Method to fetch JWT token
    public IEnumerator GetToken()
    {//Debug.LogError("inside get token");
        string apiKey = "NjVkNDIyMjNmMjc3NmU3OTI5MWJmZGI1OjY1ZDQyMjIzZjI3NzZlNzkyOTFiZmRhYg";
        string endPoint = "http://20.15.114.131:8080/api/login";

        using (UnityWebRequest request = UnityWebRequest.Post(endPoint, new WWWForm()))
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
                staticdata.JWTtoken = token;
                Debug.Log("Token fetched: " + token);

                // Load the MainMenu scene
               SceneManager.LoadScene("MainMenu");

            }
            else
            {
                Debug.LogError("Failed to fetch token: " + request.error);
            }
        }
    }
    private class TokenResponse
    {
        public string token;
    }


    // Method to fetch player profile using JWT token
 

    void PlayGame()
    {
        Debug.Log("inside play Game");
       StartCoroutine(GetTokenCoroutine());
       

    }

    private IEnumerator GetTokenCoroutine()
    {
        yield return StartCoroutine(GetToken());
        //StartCoroutine(FetchPlayerProfile());
      
    }

   

    public void OnPlayButton()
    {
        Debug.Log("Starting the game...");
        PlayGame();
        Debug.Log("End the game...");
    }
}
