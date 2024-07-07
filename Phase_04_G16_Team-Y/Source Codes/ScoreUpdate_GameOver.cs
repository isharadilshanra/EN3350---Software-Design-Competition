using System;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxHandler_ : MonoBehaviour
{
    public Text myTextBox; // Reference to the Text component

    void Start()
    {
        //float final_score = PlayerPrefs.GetFloat("FinalScore", 0f);
        float final_score_ = staticdata.finalScore;
        int roundScore_ = Convert.ToInt32(final_score_);
        
        if (myTextBox != null)
        {
            Debug.Log("gameover roundScore_ : " + roundScore_);
            // Assign a value to the text box
            myTextBox.text = roundScore_.ToString();
        }
    }
}