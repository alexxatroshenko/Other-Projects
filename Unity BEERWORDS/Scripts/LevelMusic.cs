using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    [SerializeField] private AudioClip clickUIClip;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        int LevelMusicCount = FindObjectsOfType<LevelMusic>().Length;
        if (LevelMusicCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            
            DontDestroyOnLoad(gameObject);
        }
    }

    public void UIClickSFX()
    {
        audioSource.PlayOneShot(clickUIClip, 10f);
    }
}
