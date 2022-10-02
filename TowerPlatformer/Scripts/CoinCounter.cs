using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    private int coinsCount = 0;
    private TextMeshProUGUI coinsText;

    private void Start()
    {
        coinsText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        coinsText.text = coinsCount.ToString();
    }

    public void AddCoin()
    {
        coinsCount++;
    }
}
