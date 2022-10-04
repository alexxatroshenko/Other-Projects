using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WordsArray : MonoBehaviour, IPointerExitHandler,  IPointerUpHandler
{
    //configuration
    [SerializeField] private string[] words;
    [SerializeField] private float secondsToWaitNextLevel = 1;
    [SerializeField] private AudioClip rightWordAudioClip;
    [SerializeField] private AudioClip levelDrunk;
    [SerializeField] GameObject particlesOutro;

    [Header("Particles Outro Offset")]
    [SerializeField] private float outroParticlesOffsetX = 0f;
    [SerializeField] private float outroParticlesOffsetY = -4f;

    //states
    private int charsCount = 0;
    private string combinedString;

    //cached
    [SerializeField] private List<Char> rightWords;
    private List<string> inputedStrings;
    private PaintChar chars;
    private Animator animator;
    private AudioSource audioSource;

    private void Start()
    {
        chars = GetComponent<PaintChar>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsTheWordRight();
        DoIfWordRight();
        LevelComplete();

    }

    public bool IsTheWordRight()
    {
        CombineInputedStrings();
        foreach (string word in words)
        {
            if (word == combinedString)
            {
                return true;
            }
        }
        return false;
    }

    private void DoIfWordRight()
    {
        if (IsTheWordRight())
        {
            for (int i = 0; i < chars.GetInputedChars().Count; i++)
            {
                chars.GetInputedChars()[i].GetComponent<BoxCollider2D>().enabled = true;

                rightWords.Add(chars.GetInputedChars()[i]);
                
            }
            FindObjectOfType<LevelSession>().AddScore();
            animator.SetTrigger("WordIsRight");
            audioSource.PlayOneShot(rightWordAudioClip);
        }
    }

    public List<Char> GetRightWords()
    {
        return rightWords;
    }

    private void LevelComplete()
    {
        CombineInputedStrings();
        foreach (string word in words)
        {
            if (word == combinedString)
            {
                charsCount += inputedStrings.Count;
                if (charsCount >= chars.GetCharsAmount())
                {
                    StartCoroutine(LevelComplete(secondsToWaitNextLevel));
                }
            }
        }
    }

    private IEnumerator LevelComplete(float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait / 2);
        audioSource.PlayOneShot(levelDrunk);
        yield return new WaitForSeconds(secondsToWait / 2);
        GameObject outro = Instantiate(particlesOutro, new Vector2(transform.position.x - outroParticlesOffsetX, transform.position.y - outroParticlesOffsetY),
            Quaternion.Euler(-90f,0f,0f));
        yield return new WaitForSeconds(secondsToWait * 2);
        FindObjectOfType<LevelLoader>().LoadNextLevel();
    }

    private void CombineInputedStrings()
    {
        inputedStrings = chars.GetInputedStrings();
        combinedString = string.Join("", inputedStrings);
    }

}
