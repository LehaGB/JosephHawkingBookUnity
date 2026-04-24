using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeController
{
    public static void SetUpOn()
    {
        Time.timeScale = 1.0f;
    }
    public static void SetUpOff()
    {
        Time.timeScale = 0.0f;
    }
}
