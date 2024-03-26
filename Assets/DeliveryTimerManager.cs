using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeliveryTimerManager
{
    private static float startTime;
    private static float endTime;
    private static bool timerRunning = false;

    public static void StartTimer()
    {
        startTime = Time.time;
        timerRunning = true;
    }

    public static void StopTimer()
    {
        if (timerRunning)
        {
            endTime = Time.time;
            timerRunning = false;
        }
    }

    public static float GetDeliveryTime()
    {
        if (timerRunning)
        {
            return Time.time - startTime;
        }
        else
        {
            return endTime - startTime;
        }
    }
}
