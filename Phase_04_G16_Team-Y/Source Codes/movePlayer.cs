using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.VFX;
//using static MovePlayer;
//using static System.Net.Mime.MediaTypeNames;
using UnityEngine.UI;

public class MovePlayer : MonoBehaviour
{
    public VisualEffect vfxRenderer;

    public float runSpeed = 3f; // Lower speed for running
    public float jumpForce = 6f; // Force applied when the character jumps
    public int maxJumps = 2; // Maximum number of jumps (for double jump)
    public float groundCheckThreshold = 2f; // Time threshold in seconds

    private int jumpCount = 0; // Counter for jumps
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private bool isGrounded; // Check if the character is grounded
    private float timeSinceLastGrounded = 0f; // Timer to check if the player has been on the ground
    private bool facingRight = true; // Check the direction the player is facing
    public ConsumptionHandler handler;

    private string GetEmailUrl = "http://localhost:8080/Players/getScores";
    public Text booster;
    public Text QuizScore;
    public Text visibilty;

    void Start()
    {
        StartCoroutine(GetBooster());
        float radius = 1f;
        visibilty.text = radius.ToString();
        vfxRenderer.SetFloat("collisionRadius", radius);
        vfxRenderer.SetVector3("colliderPos", transform.position);

        rb = GetComponent<Rigidbody2D>();
        // Debug to confirm Rigidbody2D is attached
        Debug.Log("Rigidbody2D component initialized: " + (rb != null));
    }

    [System.Serializable]
    public class PlayerDataWrapper
    {
        public List<Player> players;
    }

    void Update()
    {
        StartCoroutine(GetBooster());
        float radius = handler.cloud_range;
       
        vfxRenderer.SetFloat("collisionRadius", radius);
        vfxRenderer.SetVector3("colliderPos", transform.position);
        visibilty.text = (((radius*100)/4.0f).ToString()) + " %";

        // Debug statement to check if the Update method is running
        //Debug.Log("Update method running");

        // Handle running
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * runSpeed, rb.velocity.y);

        // Handle flipping
        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Debug statement to check if space key is pressed
            Debug.Log("Space key pressed");

            if (isGrounded || jumpCount < maxJumps)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpCount++;

                // Debug statement to check jump logic
                Debug.Log("Jump! Jump Count: " + jumpCount);
            }
            else
            {
                Debug.Log("Cannot jump. isGrounded: " + isGrounded + ", jumpCount: " + jumpCount);
            }
        }

        if (!isGrounded)
        {
            timeSinceLastGrounded += Time.deltaTime;
        }
        else
        {
            timeSinceLastGrounded = 0f;
        }

        if (timeSinceLastGrounded > groundCheckThreshold)
        {
            Debug.Log("Game Over");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.collider.name);

        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0; // Reset jump count when touching the ground
            timeSinceLastGrounded = 0f;

            Debug.Log("Landed on ground, jump count reset");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;

            Debug.Log("Left ground");
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
    }


    public IEnumerator GetBooster()
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
                        QuizScore.text = player.score.ToString();
                        booster.text = Calculate(player.score).ToString();
                        //vfxRenderer.SetVector3("colliderPos", transform.position);
                        //float radius = Calculate(player.score); // Get the cloud_range from ApiHandler
                        break;
                    }
                }
            }
            else
            {
                Debug.LogError("Error fetching player data: " + request.error);
            }
        }
    }

    public static int Calculate(int number)
    {
        // Perform the calculations
        double result = (number / 100.0) * 3 + 1;

        // Round the result to the nearest integer
        int roundedResult = (int)Math.Round(result);

        return roundedResult;
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
