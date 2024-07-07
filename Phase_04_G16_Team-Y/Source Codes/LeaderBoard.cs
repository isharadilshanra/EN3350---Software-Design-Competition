using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using static LeaderBoard;
using static System.Net.WebRequestMethods;
using Unity.VisualScripting.Antlr3.Runtime;
using Unity.VisualScripting;

public class LeaderBoard : MonoBehaviour
{

    private const string apiUrl = "http://20.15.114.131:8080/api/user/profile/list";
    public List<string> usernames;
    private string username;


    public void Start() {
        StartCoroutine(FetchUserNameList());
    }
    IEnumerator FetchUserNameList()
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
                UserDataResponse userDataResponse = JsonConvert.DeserializeObject<UserDataResponse>(jsonResponse);
              
                if (userDataResponse != null && userDataResponse.userViews != null)
                {
                    usernames = new List<string>();
                    
                    foreach (UserData user in userDataResponse.userViews)
                    {
                    usernames.Add(user.username);
                    }
                    staticdata.usernames = usernames;
                    Debug.Log("Usernames: " + string.Join(", ", usernames));

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

    public void GetPlayerList()
    {
        StartCoroutine(FetchUserNameList());
    }
    
     [System.Serializable]
    private class UserDataResponse
    {
        public List<UserData> userViews;
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

