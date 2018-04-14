﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transitions : MonoBehaviour
{
    public Texture2D[] waves;
    private Texture2D whiteCube;
    private Color[] waveColours;

    public bool startWavesUp = false;
    public float[] wavePercent;

    private bool isWaving = false;
    private const float endPercent = 1.4f, travelTime = 1f;

    private void Start()
    {
        whiteCube = new Texture2D(1, 1);
        whiteCube.SetPixel(0, 0, Color.white);
        whiteCube.Apply();

        wavePercent = new float[waves.Length];
        waveColours = new Color[waves.Length];
        for (int i = 0; i < wavePercent.Length; i++)
        {
            wavePercent[i] = startWavesUp ? 1f : 0f;
            waveColours[i] = waves[i].GetPixel(0, 0);
        }
    }

    private void OnGUI()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            if(wavePercent[i] > 0)
            {
                float actualHeight = ((float)Screen.width / waves[i].width) * Screen.height;
                GUI.DrawTexture(new Rect(0f, Screen.height * (1f - wavePercent[i]), Screen.width, actualHeight), waves[i]);

                if (i == waves.Length - 1)
                {
                    float startPos = Screen.height * (1f - wavePercent[i]) + actualHeight;
                    GUI.color = waveColours[i];
                    GUI.DrawTexture(new Rect(0f, startPos, Screen.width, Screen.height - startPos + 1), whiteCube);
                    GUI.color = Color.white;
                }
            }           
        }
    }

    public void DoWave(bool _showWaves)
    {
        if (!isWaving)
        {
            isWaving = true;
            StartCoroutine(ActualDoWaves(_showWaves));
        }
    }

    private IEnumerator ActualDoWaves(bool _showWaves)
    {
        for (int i = _showWaves ? 0 : waves.Length-1; _showWaves ? (i < waves.Length) : (i >= 0);  i += (_showWaves ? 1 : -1))
        {
            StartCoroutine(SlideWave(i, travelTime));
        }

        yield return new WaitForSeconds(travelTime);

        isWaving = false;
    }

    private IEnumerator SlideWave(int _wave, float _travelTime)
    {
        float startTime = Time.time;
        float startPos = 0f, endPos = 0f;

        if (wavePercent[_wave] == 0f)
        {
            endPos = endPercent;
        }
        else if (wavePercent[_wave] == endPercent)
        {
            startPos = endPercent;
        }
        else
        {
            //Mid Animation
            yield return 0;
        }

        while(Time.time - startTime < _travelTime)
        {
            wavePercent[_wave] = Mathf.Lerp(startPos, endPos, (Time.time - startTime) / _travelTime);
            yield return null;
        }

        wavePercent[_wave] = endPos;
    }
}
