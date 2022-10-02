using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSession : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    [SerializeField] private int score = 0;
    [SerializeField] private int minScoreAdding = 10;
    [SerializeField] private int maxScoreAdding = 40;

    private void Awake()
    {
        
        int LevelSessionCount = FindObjectsOfType<LevelSession>().Length;
        if (LevelSessionCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
            DontDestroyOnLoad(gameObject);
    }

    public void AddScore()
    {
        scoreText = GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>();
        score += Random.Range(minScoreAdding, maxScoreAdding);
        scoreText.text = "—чет: " + score.ToString();
    }

    public void AddScore(int scoreToAdd)
    {
        scoreText = GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>();
        score += scoreToAdd;
        scoreText.text = "—чет: " + score.ToString();
    }

    public int GetScore()
    {
        return score;
    }

    void OnLevelWasLoaded(int level)
    {
        scoreText = GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>();
        scoreText.text = "—чет: " + score.ToString();
    }
}
