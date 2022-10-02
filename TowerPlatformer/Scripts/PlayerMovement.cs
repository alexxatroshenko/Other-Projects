using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private float keyboardDirection;
    [SerializeField] private float velocity = 1.0f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] FixedJoystick joystick;
    private bool isGrounded = false;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Run();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            Jump();
    }
    private void Run()
    {
        keyboardDirection = Input.GetAxisRaw("Horizontal");
        transform.Rotate(0, -keyboardDirection * Time.deltaTime * velocity, 0);
        transform.Rotate(0, -joystick.Direction.x * Time.deltaTime * velocity, 0);
    }
    public void Jump()
    {

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;

    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        // then check if it is ground
        isGrounded = false;
    }
     public bool IsGrounded()
    {
        return isGrounded;
    }


}
