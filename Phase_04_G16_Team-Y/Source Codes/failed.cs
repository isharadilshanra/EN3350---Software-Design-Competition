using System;
using UnityEngine;
using UnityEngine.UI;

public class FailedSceneController : MonoBehaviour
{
    public Text myTextBox;

    void Start()
    {
        Debug.Log("Set to 0");
        
        staticdata.finalScore = 0f;
        float final_score_ = staticdata.finalScore;
        int roundScore_ = Convert.ToInt32(final_score_);
        myTextBox.text = roundScore_.ToString();
    }
}