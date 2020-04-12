using System;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private float updateInterval = 0.5F;
    [SerializeField] private Text text;

    private double lastInterval; 
    private int frames = 0;
    private double fps;

    void Start()
    {
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;
    }

    void Update()
    {
        frames++;
        float timeNow = Time.realtimeSinceStartup;

        if (timeNow > lastInterval + updateInterval)
        {
            fps = (float)(frames / (timeNow - lastInterval));
            frames = 0;
            lastInterval = timeNow;

            if(text != null)
                text.text = Converter.ConvertToString(Math.Round(fps, 1));
        }
    }
}