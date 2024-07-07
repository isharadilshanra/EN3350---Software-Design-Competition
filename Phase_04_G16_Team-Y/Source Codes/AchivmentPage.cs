/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AchivmentPage : MonoBehaviour
{
    private string GetEmailUrl = "http://localhost:8080/Players/getScores";
    public Text score;
    public Text booster;


    void Start()
    {
        score.text = "0";
        booster.text = "0";
        StartCoroutine(GetPlayerScore());

    }

    public IEnumerator GetPlayerScore()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(GetEmailUrl))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                List<Player> players = JsonUtility.FromJson<List<Player>>(request.downloadHandler.text);

                bool playerFound = false;
                foreach (Player player in players)
                {
                    if (player.email == staticdata.Email)
                    {
                        // Get the score of the player
                        Debug.Log("Player score: " + player.score);
                        score.text = player.score.ToString();
                        booster.text =((player.score)/0.2).ToString();
                        break;
                    }
                    else
                    {
                        score.text = "0";
                        booster.text = "0";
                    }
                }

                if (!playerFound)
                {
                    Debug.LogWarning("Player with email " + staticdata.Email + " not found.");
                }
            }
            else
            {
                Debug.LogError("Error fetching player data: " + request.error);
            }
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
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AchivmentPage : MonoBehaviour
{
    private string GetEmailUrl = "http://localhost:8080/Players/getScores";
    public Text score;
    public Text booster;
    private bool found = false;



    void Start()
    {
        score.text = "0";
        booster.text = "0";
        StartCoroutine(GetPlayerScore());

    }


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

    public IEnumerator GetPlayerScore()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(GetEmailUrl))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                PlayerDataWrapper dataWrapper = JsonUtility.FromJson<PlayerDataWrapper>(request.downloadHandler.text);
                List<Player> players = dataWrapper.players;

                foreach (Player player in players)
                {
                    if (player.email == staticdata.Email)
                    {
                        // Get the score of the player
                        Debug.Log("Player score: " + player.score);
                        score.text = player.score.ToString();
                        booster.text = ((player.score) / 0.2).ToString();
                        found = true;
                        break;
                    }
                }
                if (found == false)
                {
                    score.text = "Not Attempted";
                    booster.text = "Not Attempted";
                }
            }
            else
            {
                Debug.LogError("Error fetching player data: " + request.error);
            }
        }
    }

}