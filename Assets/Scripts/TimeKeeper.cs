using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeKeeper : MonoBehaviour
{
    float time = 0f;

    public TimeSpan timeFormat;

    // TimeSpan time = TimeSpan.FromSeconds(seconds);

    // backslash is used to ":" colon formatting you will not see it in output
    // string str = time.ToString(@"hh\:mm\:ss");

    static TimeKeeper instance;

    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton() // saves the time across scenes
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        time += Time.deltaTime;
        timeFormat = TimeSpan.FromSeconds(time);
    }

    public TimeSpan GetTime()
    {
        // TimeSpan timeFormat = TimeSpan.FromSeconds(time);
        return timeFormat;
        //return time;
    }

    public float GetTimeInSeconds()
    {
        return time;
    }

    public void ModifyTime(int value)
    {
        time += value;
    }

    public void ResetTime()
    {
        time = 0f;
    }
}
