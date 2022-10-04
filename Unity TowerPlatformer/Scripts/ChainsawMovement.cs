using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainsawMovement : MonoBehaviour
{
    private BoxCollider rotCollider;
    [SerializeField] private float speed = 15;
    private int direction = 1;

    private void Start()
    {
        rotCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        transform.Rotate(0f,speed * Time.deltaTime * direction, 0f);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            direction *= -1;
        }
    }
   
}
