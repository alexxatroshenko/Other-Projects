using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthCanvasSingleton : MonoBehaviour
{
    private void Awake()
    {
        int numPlayerHealth = FindObjectsOfType<HealthCanvasSingleton>().Length;
        if (numPlayerHealth > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        
    }
}
