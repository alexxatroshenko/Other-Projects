using TMPro;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{

    [SerializeField] private int health = 3;
    
    private TextMeshProUGUI healthText;
    SceneLoader sceneLoader;
    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        healthText = GetComponent<TextMeshProUGUI>();
        UpdateText();
    }

    

    private void UpdateText()
    {
        healthText.text = health.ToString();
    }

    public void TakeLife() 
    {
        health -= 1;
        if (health <= 0)
        {
            sceneLoader.LoadGameOverScene();
        }
        UpdateText();
    }

}
