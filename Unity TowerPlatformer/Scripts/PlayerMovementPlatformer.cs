using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementPlatformer : MonoBehaviour
{
    //config
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] private GameObject startPos;

    //Cached
    private Animator animator;
    private Rigidbody2D rb;
    private CapsuleCollider2D myBodyCollider2D;
    private BoxCollider2D feetCollider;

    //state
    private float gravityScaleAtStart;
    private bool isAlive = true;
    [SerializeField] private float deathKick = 5f;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = rb.gravityScale;

        transform.position = startPos.transform.position;
    }
    private void Update()
    {
        if (!isAlive)
        {
            return;
        }

        MoveCharacter();
        Jump();
        ClimbLadder();
        Die();
    }

    private void MoveCharacter()
    {
        float MoveX = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(MoveX * movementSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;
        if (MoveX < 0)
        {
            animator.SetBool("IsWalking", true);
            transform.localScale = new Vector3(-1f, 1f, 0f);
        }
        else if (MoveX == 0)
        {
            animator.SetBool("IsWalking", false);
            transform.localScale = transform.localScale;
        }
        else
        {
            animator.SetBool("IsWalking", true);
            transform.localScale = new Vector3(1f, 1f, 0f);
        }

    }

    private void ClimbLadder()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rb.gravityScale = gravityScaleAtStart;
            animator.SetBool("IsClimbing", false);
            return;
        }

        rb.gravityScale = 0f;

        float GetY = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(rb.velocity.x, GetY * climbSpeed);
        rb.velocity = climbVelocity;

        bool playerHasVerticalspeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        animator.SetBool("IsClimbing", playerHasVerticalspeed);
    }

    private void Jump()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            rb.velocity += jumpVelocityToAdd;
        }
    }

    private void Die()
    {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            animator.SetTrigger("Die");
            rb.velocity = new Vector2(0f, deathKick);
            isAlive = false;
            FindObjectOfType<GameSessionPlatformer>().ProcessPlayerDeath();
        }
    }

    public Vector3 GetStartPos()
    {
        return startPos.transform.position;
    }
}
