using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FpsCounter1 : MonoBehaviour
{

    public Text TextFPS;
    int fpsCounter = 0;
    float TimeCounter = 0.0f;
    int lastFrameRate = 0;
    public float refreshTime = 0.5f;
    const string format = "{0} fps";
    void Update()
    {
        if (TimeCounter < refreshTime)
        {
            fpsCounter++;
            TimeCounter += Time.deltaTime;
        }
        else
        {
            lastFrameRate = fpsCounter * 2;
            TimeCounter = 0.0f;
            fpsCounter = 0;
        }

        TextFPS.text = string.Format(format, lastFrameRate);
    }
}
