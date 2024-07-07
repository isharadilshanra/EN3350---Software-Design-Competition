using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RandomText : MonoBehaviour
{
    public Text[] Scores;
    public Text[] Names;

    public void Start()
    {
        bool topPlayer = false;

        List<string> nameList = staticdata.usernames;
        nameList.Add(staticdata.Firstname);
        string currentPlayer = nameList[nameList.Count - 1];
        
        List<int> randomValues = GenerateRandomIntegers(30, 10, 101);
        float currentScore = staticdata.finalScore;
        int score = Convert.ToInt32(currentScore);
        randomValues.Add(score);
        List<NameScore> nameScores = new List<NameScore>();

        for (int i = 0; i < nameList.Count; i++)
        {
            nameScores.Add(new NameScore { Name = nameList[i], Score = randomValues[i] });
        }

        // Sort the list based on scores in descending order
        nameScores.Sort((x, y) => y.Score.CompareTo(x.Score));

        // Assign sorted scores and names to the Text components
        for (int i = 0; i < 10; i++)
        {   
            if ((i == 9) && (topPlayer == false))
            {
                Scores[i].color = Color.white;
                Names[i].color = Color.white;

                for(int j = 9; j < nameList.Count; j++)
                {
                    if (nameScores[j].Name == currentPlayer)
                    {
                        Scores[i].color = Color.white;
                        Names[i].color = Color.white;
                        Scores[i].text = nameScores[j].Score.ToString();
                        Names[i].text = (j + 1).ToString("D3") + "   " + nameScores[j].Name;
                    }
                }
                return;
            }
            else
            {
                if (Scores[i] == null || Names[i] == null)
                {
                    Debug.LogError("TextBox is not assigned at index " + i);
                    continue;
                }

                if (nameScores[i].Name == currentPlayer)
                {
                    topPlayer = true;
                    Scores[i].color = Color.white;
                    Names[i].color = Color.white;
                }
                else
                {
                    Scores[i].color = Color.yellow;
                    Names[i].color = Color.yellow;
                }

                Scores[i].text = nameScores[i].Score.ToString();
                Names[i].text = (i + 1).ToString("D3") + "   " + nameScores[i].Name;
            }
                
        }
    }

    List<int> GenerateRandomIntegers(int count, int min, int max)
    {
        List<int> randomValues = new List<int>();

        for (int i = 0; i < count; i++)
        {
            int randomValue = UnityEngine.Random.Range(min, max); // max is exclusive
            randomValues.Add(randomValue);
        }

        return randomValues;
    }

    // Class to hold name and score
    public class NameScore
    {
        public string Name { get; set; }
        public int Score { get; set; }
    }
}
