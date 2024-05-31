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
        CheckingPlayer();
    }

    float curT;
    public float userCheckingTime;
    void CheckingPlayer()
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

    IEnumerator ShowShoolCenter()
    {
        while (true)
        {


        yield return null;
        }
    }
    IEnumerator HideShoolCenter()
    {
        yield return null;
    }

}
