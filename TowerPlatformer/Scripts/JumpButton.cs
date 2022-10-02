using UnityEngine.EventSystems;
using UnityEngine;

public class JumpButton : MonoBehaviour, IPointerDownHandler
{
    
    public void OnPointerDown(PointerEventData eventData)
    {
        FindObjectOfType<PlayerMovement>().Jump();
    }

  
    
}
