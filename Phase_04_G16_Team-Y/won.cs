using UnityEngine;
using UnityEngine.UI;

public class CompleteSceneController : MonoBehaviour
{

    void Start()
    {
        float finalScore = 0f;
        float timeTaken = PlayerPrefs.GetFloat("TimeTaken", 0f); // Retrieve the time taken to solve the maze

        if (timeTaken <= 15f)
        {
            finalScore = 60f; // If the maze is solved within 15 seconds, score 60 points
        }
        else if (timeTaken <= 120f)
        {
            finalScore = Mathf.Lerp(60f, 0f, (timeTaken - 15f) / 105f); // Linearly interpolate the score from 60 to 0 based on time taken
        }
        else
        {
            finalScore = 0f; // If the maze is not solved within 120 seconds, score 0 points
        }

        finalScore += 40f; // Add 40 bonus points for solving the maze
        finalScore = Mathf.Clamp(finalScore, 0f, 100f); // Ensure the score is between 0 and 100

        PlayerPrefs.SetFloat("FinalScore", finalScore); // Save the final score in PlayerPrefs
        Debug.Log("scene02 loaded " + finalScore);


    }
}