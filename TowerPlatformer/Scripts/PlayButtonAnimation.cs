using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("highlighted", true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("highlighted", false);
    }
    
}
