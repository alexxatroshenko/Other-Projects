using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongBatMovement : MonoBehaviour
{
    [SerializeField] private float rotSpeed = 10f;
    void Update()
    {
        transform.Rotate(0f, rotSpeed * Time.deltaTime, 0f);
    }
}
