using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class MainMenuScript : MonoBehaviour
{
    private const string apiUrl = "http://20.15.114.131:8080/api/user/profile/view";


    private UserData userdata;
    public Text Prompt;


    IEnumerator FetchPlayerProfile()
    {
        if (string.IsNullOrEmpty(staticdata.JWTtoken))
        {
            Debug.LogError("Token not found.");
            yield break; // Exit the coroutine if token is not found
        }

        using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
        {
            // Set authorization header with JWT token
            request.SetRequestHeader("Authorization", "Bearer " + staticdata.JWTtoken);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                UserDataResponse userDataResponse = JsonUtility.FromJson<UserDataResponse>(jsonResponse);
                if (userDataResponse != null && userDataResponse.user != null)
                {
                    userdata = userDataResponse.user;
                    staticdata.Email = userdata.email;

                    Debug.Log("Player data received");
                    if (string.IsNullOrEmpty(userdata.lastname) || string.IsNullOrEmpty(userdata.email))
                    {
                        Prompt.text = "Please complete your player profile....!";
                    }
                    else
                    {
                        Prompt.text = "your profile is already completed. Game loading....!";
                        yield return new WaitForSeconds(2f);
                        Prompt.text = "";
                        yield return new WaitForSeconds(1f);
                        SceneManager.LoadScene("GameEntrance");
                    }
                }
                else
                {
                    Debug.LogError("Error: User data is null or invalid JSON response structure.");
                }

            }
            else
            {
                Debug.LogError("Failed to fetch player profile: " + request.error);
            }
        }
    }


    public void OnPlayButton()
    {
        StartCoroutine(FetchPlayerProfile());
    }



    [System.Serializable]
    private class UserDataResponse
    {
        public UserData user;
    }

    [System.Serializable]
    private class UserData
    {
        public string firstname;
        public string lastname;
        public string username;
        public string nic;
        public string phoneNumber;
        public string email;

    }
}
