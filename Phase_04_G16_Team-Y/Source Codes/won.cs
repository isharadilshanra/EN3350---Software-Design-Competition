using System;
using UnityEngine;
using UnityEngine.UI;

public class CompleteSceneController : MonoBehaviour
{
    public Text myTextBox;
    void Start()
    {
        Debug.Log("Set to won");

        staticdata.finalScore = 0f;

        if (staticdata.timeTaken <= 15f)
        {
            staticdata.finalScore = 60f; // If the maze is solved within 15 seconds, score 60 points
        }
        else if (staticdata.timeTaken <= 120f)
        {
            staticdata.finalScore = Mathf.Lerp(60f, 0f, (staticdata.timeTaken - 15f) / 105f); // Linearly interpolate the score from 60 to 0 based on time taken
        }
        else
        {
            staticdata.finalScore = 0f; // If the maze is not solved within 120 seconds, score 0 points
        }

        staticdata.finalScore += 40f; // Add 40 bonus points for solving the maze
        staticdata.finalScore = Mathf.Clamp(staticdata.finalScore, 0f, 100f); // Ensure the score is between 0 and 100

        Debug.Log("scene02 loaded " + staticdata.finalScore);

        int roundScore = Convert.ToInt32(staticdata.finalScore);
        myTextBox.text = roundScore.ToString();


    }
}