using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsLimit : MonoBehaviour
{
    public int LimitedFPS = 60;
    public bool IsLimited = false;
    private void Awake()
    {
        if (IsLimited == true)
        {
            Application.targetFrameRate = LimitedFPS;
            QualitySettings.vSyncCount = 0;
        }
    }
}
