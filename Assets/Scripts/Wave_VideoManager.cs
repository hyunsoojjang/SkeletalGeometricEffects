using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Wave_VideoManager : MonoBehaviour
{
  
    public VideoPlayer introVideo;
    public GameObject introRenderObj;
    public GameObject fadeObj;
    public GameObject floorCam;

    public RawImage shoolCenterVideoRawimage;

    public Dance_HumanChecker humanChecker;
  
    void Start()
    {
        introVideo.loopPointReached += IntroVideo_loopPointReached;
    }

    private void IntroVideo_loopPointReached(VideoPlayer source)
    {
        introRenderObj.SetActive(false);
        floorCam.SetActive(true);
        fadeObj.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (introVideo.isPlaying) introVideo.Pause();
            else if (introVideo.isPaused) introVideo.Play();

        }
        //CheckingPlayer();
        CheckingPlayerbyZed();
    }

    float curT;
    public float userCheckingTime;
    void CheckingPlayerbyNuitrack()
    {
        if (NuitrackManager.Users.Count<1)
        {
            curT += Time.deltaTime;
            if (curT >= userCheckingTime)
            {
            shoolCenterVideoRawimage.CrossFadeAlpha(1, 2, true);
                curT = 0;
            }
        }
        else
        {
                shoolCenterVideoRawimage.CrossFadeAlpha(0, 0.1f,false);

            //StartCoroutine(nameof(ShowShoolCenter));
        }
    }
    void CheckingPlayerbyZed()
    {
        if (!humanChecker.someoneinCenter)
        {
            curT += Time.deltaTime;
            if (curT >= userCheckingTime)
            {
                shoolCenterVideoRawimage.CrossFadeAlpha(1, 2, true);
                curT = 0;
            }
        }
        else
        {
            shoolCenterVideoRawimage.CrossFadeAlpha(0, 0.1f, false);

            //StartCoroutine(nameof(ShowShoolCenter));
        }
    }
}
