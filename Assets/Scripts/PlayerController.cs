using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 200f;
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    [SerializeField] float jumpForce = 1000;
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public LayerMask groundLayer; // LayerMask for what is considered 'ground'
    private const float groundCheckDistance = 0.6f; // Raycast distance
    RaycastHit hit;
    public float height;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnJump()
    {
        if (IsGrounded())
        {
            rb.AddForce(new Vector3(0, jumpForce + 200, 0), ForceMode.Acceleration);
            Debug.Log("Jumped");
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
        Debug.Log(movementValue);
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0, movementY) * speed;
        rb.AddForce(movement);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && IsGrounded())
        {
            rb.AddForce(new Vector3(0, jumpForce + 200, 0), ForceMode.Acceleration);
        }
        
        Ray ray = new Ray(transform.position, -Vector3.up);

        Debug.DrawRay(transform.position, Vector3.down * height, Color.red);

        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.tag == "Ground")
            {
                Debug.Log(hit.distance);
            }
        }
    }

    bool IsGrounded()
    {
        Debug.Log("Checking if grounded..."); // This line will help you know when this function is called.
        bool grounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
        if (grounded)
        {
            Debug.Log("Player is grounded."); // This will confirm if the player is detected as grounded.
        }
        Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, Color.green);
        return grounded;
    }


    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag("JumpBoost"))
        {
            rb.AddForce(new Vector3(0, jumpForce + 1000, 0), ForceMode.Acceleration);
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 10)
        {
            winTextObject.SetActive(true);
        }
    }
    
}
