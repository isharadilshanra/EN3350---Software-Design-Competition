using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.WSA;

public class LoadingPageScript : MonoBehaviour
{
    public Text info;
    public int counter;
    public int rate = 200;
    public bool isLoading = true;
    public GameObject gameStartInterface;

    // Start is called before the first frame update
    void Start()
    {
        info.text = "Press START to launch the game";
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLoading)
        {
            startLoading();
        }
        else
        {
            clearInfo();
        }
        
    }

    public void startLoading()
    {
        if (counter == rate)
        {
            info.text = "Press START to launch the game . ";
        }
        if (counter == 2 * rate)
        {
            info.text = "Press START to launch the game . .";
        }
        if (counter == 3 * rate)
        {
            info.text = "Press START to launch the game . . .";
            counter = 0;
        }
        counter++;
    }

    public void Loading()
    {
        if (counter == rate)
        {
            info.text = "Authenticating . ";
        }
        if (counter == 2 * rate)
        {
            info.text = "Authenticating ..";
        }
        if (counter == 3 * rate)
        {
            info.text = "Authenticating ...";
            counter = 0;
        }
        counter++;
    }

    public void clearInfo()
    {
        //info.text = "Loading ...";
        Loading();
    }

    public void startingGame()
    {
        isLoading = false;
        Debug.Log("Game Started!");
    }

    public void disableButton()
    {
        gameStartInterface.SetActive(false);
    }
}
