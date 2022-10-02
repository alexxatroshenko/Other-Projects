using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelCollider : MonoBehaviour
{
    private int sceneIndex;
    [SerializeField] private float loadNextSceneDelay = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(FindObjectOfType<DieCollider>().gameObject);
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(loadNextSceneDelay);
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(++sceneIndex);
    }
}
