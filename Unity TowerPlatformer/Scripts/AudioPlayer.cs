using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private void Awake()
    {
        
        int numAudioPlayer = FindObjectsOfType<AudioPlayer>().Length;
        if (numAudioPlayer > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }

}
