using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

[System.Serializable]
public class Timer
{
    public string startHour;
    public string startMin;
    public string startSec;

    public string endHour;
    public string endtMin;
    public string endSec;
}

public class Settings : MonoBehaviour
{
    public Timer timer = new Timer()
    {
        startHour = "18",
        startMin = "59",
        startSec = "55",

        endHour = "21",
        endtMin = "50",
        endSec = "00",
    };

    [SerializeField] string filePath;

    private void Awake()
    {
        filePath = Application.dataPath + "/Settings.json";
        Debug.Log(filePath);
        FileSetting();
    }

    private void Start()
    {
    }

    private void FileSetting()
    {
        //File.Delete(filePath);
        string jsonData = JsonUtility.ToJson(timer);
        if (File.Exists(filePath) == false)
        {
            var file = File.CreateText(filePath);
            file.Write(jsonData);
            file.Close();
        }

        StreamReader sr = new StreamReader(filePath);
        string json = sr.ReadToEnd();
        timer = JsonUtility.FromJson<Timer>(json);
        Debug.Log(json);

    }

    //private void TimeSetting(Timer timer)
    //{
    //    gameManager.StartH = timer.startHour;
    //    gameManager.StartM = timer.startMin;
    //    gameManager.StartS = timer.startSec;

    //    gameManager.EndH = timer.endHour;
    //    gameManager.EndM = timer.endtMin;
    //    gameManager.EndS = timer.endSec;
    //}
}
