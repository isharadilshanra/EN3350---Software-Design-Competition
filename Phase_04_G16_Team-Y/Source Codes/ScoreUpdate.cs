using System;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxHandler : MonoBehaviour
{
    public Text myTextBox; // Reference to the Text component

    void Start()
    {
        //float final_score = PlayerPrefs.GetFloat("FinalScore", 0f);
        float final_score = staticdata.finalScore;
        int roundScore = Convert.ToInt32(final_score);
        if (myTextBox != null)
        {
            // Assign a value to the text box
            Debug.Log("game won roundScore : " + roundScore);
            myTextBox.text = roundScore.ToString();
        }
    }
}