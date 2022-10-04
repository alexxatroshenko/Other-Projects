using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //configs
    [SerializeField] private float moveSpeed = 1f;

    //cached
    private Rigidbody2D rb;
    //private Collider2D col;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //col = GetComponent<Collider2D>();
    }

  
    void Update()
    {
        Move();
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2((Mathf.Sign(rb.velocity.x)), 1f);
    }

    private void Move()
    {
        if(IsFacingRight())
            rb.velocity = new Vector2(-moveSpeed, 0);
        else
            rb.velocity = new Vector2(moveSpeed, 0);
    }

	

}
