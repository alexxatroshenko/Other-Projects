using UnityEngine.EventSystems;
using UnityEngine;

public class JumpButton : MonoBehaviour, IPointerDownHandler
{
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Jump");
        FindObjectOfType<PlayerMovement>().Jump();
    }    
}
