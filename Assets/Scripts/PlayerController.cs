using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public float jump_force = 10.0f;
    public int num_jumps = 0;
    public TextMeshProUGUI countText;
    public GameObject WinTextObject;
    
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        WinTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(num_jumps < 2)
            {
                rb.AddForce(new Vector3(0, jump_force, 0));
                num_jumps++;
            }
         }
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 12)
        {
            WinTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
        else if(other.gameObject.CompareTag("Ground"))
        {
            num_jumps = 0;
         }
    }
}
