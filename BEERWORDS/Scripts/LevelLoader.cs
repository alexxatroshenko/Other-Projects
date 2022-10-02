using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private int firstLevelSceneIndex = 2;
    
    
    private int sceneIndex;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Loading Scene")
        {
            StartCoroutine(LoadGame());
        }
    }

    private IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(5);
        LoadMainMenu();
    }

    public void LoadLevelCompletedScene()
    {
        SceneManager.LoadScene("Level Completed");
    }

    public void LoadNextLevel()
    {
        
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex + 1);
    }

    public void PlayUICClick()
    {
        FindObjectOfType<LevelMusic>().UIClickSFX();
}

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(firstLevelSceneIndex);
    }
    public void LoadMainMenu()
    {

        SceneManager.LoadScene(1);
        var levelSession = FindObjectOfType<LevelSession>();
        levelSession.AddScore(-levelSession.GetScore());

    }
}
