// Character Animator: control character behavior (run, fall, idle)


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    // Check if character hit something
    public Transform rayStart;
    public GameObject crystalEffect;
    // Rigibody in order to access the movement components
    private Rigidbody rb;
    // Object to check if moving left or right
    private bool walkingRight = true;
    // Animator
    private Animator anim;

    private GameManager gameManager;

    void Awake()
    {
        // Initialize movement, animator, and game manager
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Function to move character forward
    private void FixedUpdate()
    {
        // Check if game does NOT start from game manager
        if (!gameManager.gameStarted)
            return;
        else
            // Set trigger for animator, change from idle to run
            anim.SetTrigger("gameStarted");

        rb.transform.position = transform.position + transform.forward * 2 * Time.deltaTime;
    }

    // Check Input
    void Update()
    {
        // If user press "Space", character will turn either way
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Switch();
        }
        
        RaycastHit hit;
        
        // Condition if character NOT hit the bottom (meaning not hit square and fall down below)
        if (!Physics.Raycast(rayStart.position, -transform.up, out hit, Mathf.Infinity))
        {
            // Trigger the "falling" phrase
            anim.SetTrigger("isFalling");
            Debug.Log("Falling");
        }

        // Check if character is falling, then end game
        if (transform.position.y < -2)
        {
            gameManager.EndGame();
        }
    }

    // Function to turn either way for character
    private void Switch()
    {
        if (!gameManager.gameStarted)
            return;

        walkingRight = !walkingRight;

        if (walkingRight)
            transform.rotation = Quaternion.Euler(0, 45, 0);
        else
            transform.rotation = Quaternion.Euler(0, -45, 0);
    }

    // If character collide with crytals, increase scores and destroy objects crystal
    private void OnTriggerEnter(Collider other)
    {
        // Hit "Crystal" object
        if (other.tag == "Crystal")
        {
            gameManager.IncreaseScore();
            // Instantiate crystal effect game object
            GameObject g = Instantiate(crystalEffect, rayStart.transform.position, Quaternion.identity);
            Destroy(g, 2);
            Destroy(other.gameObject);
        }
    }
}
