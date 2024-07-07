using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime; // Increment the timer by the time passed since the last frame
        staticdata.timeTaken = timer;
        //PlayerPrefs.SetFloat("TimeTaken", timer); // Save the elapsed time in PlayerPrefs
        Debug.Log("Elapsed Time: " + timer); // Log the elapsed time to the console
    }
}