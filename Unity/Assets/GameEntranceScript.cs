using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class PlayerDataWrapper
{
    public List<Player> players;
}

[System.Serializable]
public class Player
{
    public int id;
    public string email;
    public int score;
    public bool attempted;
}

public class GameEntranceScript : MonoBehaviour
{
    private string GetEmailUrl = "http://localhost:8080/Players/getScores";
    private string PostEmailUrl= "http://localhost:8080/Players/uploadEmails";
    public Text status;
    private bool found=false;

    public void OnQuizButton()
    {
        StartCoroutine(GetPlayerEmails());
    }

    IEnumerator GetPlayerEmails()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(GetEmailUrl))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {   
                Debug.Log("List of Emails received......!");

                // Deserialize the JSON response into PlayerDataWrapper object
                PlayerDataWrapper dataWrapper = JsonUtility.FromJson<PlayerDataWrapper>(request.downloadHandler.text);
                List<Player> players = dataWrapper.players;
               
                Debug.Log(players);

                List<string> playerEmails = new List<string>();

                foreach (Player player in players)
                {
                    playerEmails.Add(player.email);
                }
                
                foreach (string email in playerEmails)
                {
                    if (email == staticdata.Email)
                    {
                        Debug.Log(email + " already attempted the quiz");
                        status.text = "you are already attempted the quiz!";
                        yield return new WaitForSeconds(3f);
                        status.text = "";
                        found=true;
                        break;

                    }
                }
                if (found==false)
                {
                    Debug.Log("New Player");
                    status.text = "Good Luck..!";
                    yield return new WaitForSeconds(1f);
                    status.text = "";
                    PostRequest();
                    string url = "http://localhost:5173/";
                    Application.OpenURL(url);
                }
            }
            else
            {
                Debug.LogError("Error fetching player data: " + request.error);
            }
        }
    }

    void PostRequest()
  {
    StartCoroutine(SendNewPlayerEmail());
  }

 public IEnumerator SendNewPlayerEmail()
    {
  
    string jsonString = staticdata.Email;
    UnityWebRequest request = new UnityWebRequest(PostEmailUrl, "POST");
    request.SetRequestHeader("Content-Type", "application/json");
    byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);
    request.uploadHandler = new UploadHandlerRaw(bodyRaw);
   
    yield return request.SendWebRequest();

    if (request.result != UnityWebRequest.Result.Success)
    {
        Debug.LogError("Error sending email: " + request.error);
    }
    else
    {
        Debug.Log("Email sent successfully!");
    } 
    } 
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Networking;
// using UnityEngine.UI;
// //using Newtonsoft.Json;

// public class GameEntranceScript : MonoBehaviour
// {
//     private string GetEmailUrl = "http://localhost:8080/Players/getScores";
//     private string PostEmailUrl= "http://localhost:8080/Players/uploadEmails";
//     public Text status;
    
//     public void OnQuizButton()
//     {
//         StartCoroutine(GetPlayerEmails());
//         // Debug.Log("New Player");
//         // SendMessage();
//         //string url = "http://localhost:5173/";
//         //Application.OpenURL(url);
//         //PostRequest();
//     }
//    public IEnumerator GetPlayerEmails()
//    {
//         using (UnityWebRequest request = UnityWebRequest.Get(GetEmailUrl))
//         {
//             yield return request.SendWebRequest();

//             if (request.result == UnityWebRequest.Result.Success)
//             {   
//                 Debug.Log("List of Emails received......!");

//                 List<Player> players = JsonUtility.FromJson<List<Player>>(request.downloadHandler);
               
//                 //Debug.Log(players);

//                 List<string> playerEmails = new List<string>();


//                 foreach (Player player in players)
//                 {
//                     playerEmails.Add(player.email);
              
//                 }

//                 foreach (string email in playerEmails)
//                 {
                    
//                     if (email==staticdata.Email)
//                     {
//                         Debug.Log(email+" already attempted the quiz");
//                         status.text = "you are already attempted the quiz!";
//                     }
//                     else
//                     {   
//                         Debug.Log("New Player");
//                         PostRequest();
//                         string url = "http://localhost:5173/";
//                         Application.OpenURL(url);
//                     }
//                 }
//             }
//             else
//             {
//                 Debug.LogError("Error fetching player data: " + request.error);
//             }
//         }
  
//    }   

// void PostRequest()
// {
//     StartCoroutine(SendNewPlayerEmail());
// }

//  public IEnumerator SendNewPlayerEmail()
//     {
  
//     string jsonString = staticdata.Email;
//     UnityWebRequest request = new UnityWebRequest(PostEmailUrl, "POST");
//     request.SetRequestHeader("Content-Type", "application/json");
//     byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);
//     request.uploadHandler = new UploadHandlerRaw(bodyRaw);
   
//     yield return request.SendWebRequest();

//     if (request.result != UnityWebRequest.Result.Success)
//     {
//         Debug.LogError("Error sending email: " + request.error);
//     }
//     else
//     {
//         Debug.Log("Email sent successfully!");
//     } 
//     }  



//     [System.Serializable]
//     public class Player
//     {
//         public int id;
//         public string email;
//         public int score;
//         public bool attempted;
//     }

// }


