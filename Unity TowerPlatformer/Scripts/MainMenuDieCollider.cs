using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuDieCollider : MonoBehaviour
{
    [SerializeField] private GameObject startPos;
    private Collider2D dieCollider;

    private void Start()
    {
        dieCollider = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.transform.position = startPos.transform.position;
    }
}
