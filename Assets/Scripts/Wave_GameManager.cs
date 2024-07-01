using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Wave_GameManager : MonoBehaviour
{
    public Settings settings;
    public Game_SoundManager soundManager;
    int endHour;
    int endMin;
    int endSec;
    int secondEndHour;
    int secondEndMin;


    public bool isUseCountTime;
    public static Wave_GameManager instance;
    public ZEDManager zedManager;
    public GameObject canvasVideo;
    public GameObject canvasFloor;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        zedManager.OnZEDReady += ZedManager_OnZEDReady;

        endHour = int.Parse(settings.timer.endHour);
        endMin = int.Parse(settings.timer.endtMin);
        endSec = int.Parse(settings.timer.endSec);
        Debug.Log($"First : {endHour}hour {endMin}min {endSec}sec ");

        if (endMin >= 30)
        {
            secondEndMin = endMin - 30;
            //secondEndHour= endHour+1;
        }
        else
        {
            secondEndMin = endMin + 30;
            //secondEndHour = endHour;
        }

        Debug.Log($"Second : {secondEndHour}hour {secondEndMin}min {endSec}sec ");


    }
    private void OnDestroy()
    {
        zedManager.OnZEDReady -= ZedManager_OnZEDReady;

    }

    private void ZedManager_OnZEDReady()
    {
        canvasVideo.SetActive(true);
       canvasFloor.SetActive(true);
    }

    DateTime now;
    bool isEnd;
    float curT;
    void Update()
    {
        curT += Time.deltaTime;
        now = DateTime.Now;
        if (isEnd) return;

        if (isUseCountTime)
        {
            CheckByCountTime(curT);
        }
        else
        {
            CheckByDateTime(now);
        }

    }
    public void CheckByDateTime(DateTime now)
    {
        if (now.Minute == endMin
         && now.Second == endSec)
        {

            soundManager.SoundFadeOut();
            isEnd = true;
        }
        else if (now.Minute == secondEndMin
              && now.Second == endSec)
        {
            soundManager.SoundFadeOut();
            isEnd = true;

        }
    }
    public float endTimeCount;
    public void CheckByCountTime(float curT)
    {
      
        if (curT > endTimeCount)
        {
            soundManager.SoundFadeOut();
            isEnd = true;
        }
    }

    public void EndSignals()
    {
        //Protocol.instance.SendDataToServer("FacadeEnd");
        SendRequester.instance.EndZed();
        SendRequester.instance.SendRequeest();

    }
}
