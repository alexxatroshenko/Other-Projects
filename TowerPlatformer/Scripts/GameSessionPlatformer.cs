using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSessionPlatformer : MonoBehaviour
{
    [SerializeField] private int playerLifes = 3;
    [SerializeField] private int waitForRespawn = 1;
    
    private void Awake()
    {
        
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1)
        {
            
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        
    }
    public void ProcessPlayerDeath()
    {
        if(playerLifes > 1)
        {
            StartCoroutine(TakeLife());
        }
        else
        {
            StartCoroutine(PlayerDeath());
        }
    }

    private IEnumerator PlayerDeath()
    {
        yield return new WaitForSeconds(waitForRespawn);
        ResetGameSession();
    }

    

    private IEnumerator TakeLife()
    {
        playerLifes--;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSeconds(waitForRespawn);
        FindObjectOfType<PlayerHealth>().GetComponent<Animator>().SetInteger("Health", playerLifes);
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void ResetGameSession()
    {
        FindObjectOfType<PlayerHealth>().GetComponent<Animator>().SetInteger("Health", 3);
        playerLifes = 3;
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
