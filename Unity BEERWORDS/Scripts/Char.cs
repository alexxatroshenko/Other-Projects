using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Char : MonoBehaviour
{
    private TextMeshProUGUI tmPro;
    [SerializeField] private string spotText;

    private void Start()
    {
        tmPro = GetComponentInChildren<TextMeshProUGUI>();
        tmPro.text = spotText;
    }
}
