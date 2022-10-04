using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CostTexter : MonoBehaviour
{
    [SerializeField] Defender defender;
    [SerializeField] TextMeshProUGUI costText;
    void Start()
    {
        costText.text = defender.GetStarCost().ToString();
       
    }

    
}
