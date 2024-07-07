/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameEntranceScript : MonoBehaviour
{
    private string GetEmailUrl = "http://localhost:8080/Players/getScores";
    private string PostEmailUrl= "http://localhost:8080/";
    public Text status;
    
    public void OnQuizButton()
    {
        StartCoroutine(GetPlayerEmails());
    }


    

    public IEnumerator GetPlayerEmails()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(GetEmailUrl))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                List<Player> players = JsonUtility.FromJson<List<Player>>(request.downloadHandler.text);

                List<string> playerEmails = new List<string>();


                foreach (Player player in players)
                {
                    playerEmails.Add(player.email);
              
                }

                foreach (string email in playerEmails)
                {
                    
                    if (email==staticdata.Email)
                    {
                        Debug.Log(email+" already attempted the quiz");
                        status.text = "you are already attempted the quiz!";
                    }
                    else
                    {   
                        SendMessage(staticdata.Email);
                        //Go to the Quiz url ( TODO )
                    }
                }
            }
            else
            {
                Debug.LogError("Error fetching player data: " + request.error);
            }
        }
    }

    new void SendMessage(string email)
    {
        string jsonString = "{\"email\": \"" + email + "\"}";

        UnityWebRequest request = new UnityWebRequest(PostEmailUrl, "POST");

        request.SetRequestHeader("Content-Type", "application/json");

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);

        request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error sending message: " + request.error);
        }
    }




    [System.Serializable]
    public class Player
    {
        public int id;
        public string email;
        public int score;
        public bool attempted;
    }

}
*/




using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    private string PostEmailUrl = "http://localhost:8080/Players/uploadEmails";
    public Text status;
    private bool found = false;

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
                        found = true;
                        SceneManager.LoadScene("start");
                        break;

                    }
                }
                if (found == false)
                {
                    Debug.Log("New Player");
                    status.text = "Good Luck..!";
                    yield return new WaitForSeconds(1f);
                    status.text = "";
                    PostRequest();
                    string url = "http://localhost:5173/";
                    Application.OpenURL(url);
                    SceneManager.LoadScene("start");
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