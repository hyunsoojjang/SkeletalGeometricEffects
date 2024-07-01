using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class InfoData
{
    public bool isContents;
}
public class SendRequester : MonoBehaviour
{
    public static SendRequester instance;
    private void Awake()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("info");
        if (jsonFile != null)
        {
            InfoData infoData = JsonUtility.FromJson<InfoData>(jsonFile.text);

            if (infoData != null)
            {
                nextIsContents = infoData.isContents;
                Debug.Log("nextIsContents: " + nextIsContents);
            }
            else
            {
                Debug.LogError("JSON Parsing Fail");
            }
        }
        else
        {
            Debug.LogError("JSON FIle Load Fail");
        }

        if (instance == null)
        {
            instance = this;
        }
    }
    internal string contentsUrl = "http://localhost:8080/start-next?interactive=true"; 
    internal string videoUrl = "http://localhost:8080/start-next?interactive=false";

    public bool nextIsContents;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            EndZed();
            StartCoroutine(nameof(SendEndGetRequest));
        }
    }
    public GameObject zedrig;
    public GameObject zedbody;
    public void EndZed()
    {
        DestroyImmediate(zedrig);
        DestroyImmediate(zedbody);
    }
    public void SendRequeest()
    {
        StartCoroutine(nameof(SendEndGetRequest));
    }
    IEnumerator SendEndGetRequest()
    {
        yield return new WaitForSeconds(0.5f);

        Debug.Log("SendENd in");
        string uri;
        if (nextIsContents) { uri = contentsUrl; }
        else { uri = videoUrl; }

        UnityWebRequest webRequest = UnityWebRequest.Get(uri);
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Get Success");
            Debug.Log("Res : " + webRequest.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Get failed: " + webRequest.error);
        }
    }
}
