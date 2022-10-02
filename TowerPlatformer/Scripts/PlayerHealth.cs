using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float criticalSpeed = 25f;
    private Rigidbody rb;
    private PlayerMovement player;
    
    private bool tooFastY = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerMovement>();
        
    }
    private void Update()
    {
        Falling();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Damagable>() != null)
            Damage();
    }

    public void Damage()
    {
        FindObjectOfType<GameSession>().GetComponent<GameSession>().ReloadScene();
       // gameSession.ReloadScene();
    }

    private void Falling()
    {
        if (Mathf.Abs(rb.velocity.y) > criticalSpeed)
        {
            tooFastY = true;
        }
        else
            tooFastY = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (tooFastY)
        {
            Damage();
            Debug.Log("Damage");
        }
       
    }
}
