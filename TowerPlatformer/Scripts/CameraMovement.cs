using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform LookAtPlayer;
    [SerializeField] private Transform LookAtPlayerHandler;
    [SerializeField] private float boundRotate = 1f;
    [SerializeField] private float boundY = 13f;
    [SerializeField] private float rotationSpeed = 10f;

    private void LateUpdate()
    {
        Vector3 deltaPos = Vector3.zero;
        Vector3 deltaRot = Vector3.zero;
        // This is to check if we are inside the bounds on the X axis
        float deltaX = LookAtPlayerHandler.rotation.y - transform.rotation.y;
        
        if (deltaX > boundRotate || deltaX < -boundRotate)
        {
            if (transform.rotation.y < LookAtPlayerHandler.rotation.y)
            {
                deltaRot.y = deltaX - boundRotate;
            }
            else
            {
                deltaRot.y = deltaX + boundRotate;
            }
        }

        // This is to check if we are inside the bounds on the Y axis
        float deltaY = LookAtPlayer.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < 0)
        {
            if (transform.position.y < LookAtPlayer.position.y)
            {
                deltaPos.y = deltaY - boundY;
            }
            else
            {
                deltaPos.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3(0, deltaPos.y, 0);
        transform.rotation = Quaternion.Euler(90, deltaRot.y * rotationSpeed, 0f);
        //transform.Rotate(0f, deltaRot.y, 0f);
    }
}
