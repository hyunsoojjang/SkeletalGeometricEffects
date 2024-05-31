using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_SoundManager : MonoBehaviour
{
    public AudioClip[] audios;
    int index;
    public AudioSource audioSource;
    float initVolume;
    public float soundFadeTime;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        initVolume = audioSource.volume;
        index = 0;
        if (audios.Length > 0)
        {
        audioSource.clip = audios[index];
        audioSource.Play();
        StartCoroutine(nameof(Sounding));
        }
        else
        {
            Debug.LogWarning("no audio clips!");
        }
    }
    public void SoundFadeOut()
    {
        StartCoroutine(nameof(SoundFade));
    }
        float curT=0;
    IEnumerator SoundFade()
    {
        while (audioSource.volume>0)
        {
            curT += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(initVolume, 0, curT / soundFadeTime);
            AudioListener.volume = Mathf.Lerp(1, 0, curT /soundFadeTime);
            //Debug.Log("curT"+curT);
            Debug.Log(audioSource.volume);

            yield return null;
        }
        //SendRequester.instance.SendRequeest();
        Wave_GameManager.instance.EndSignals();
    }

    IEnumerator Sounding()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            //Debug.Log(audioSource.isPlaying);
            //Debug.Log($"Cur Audio time : {(int)audioSource.time}/ Whole Audio Time : {(int)audioSource.clip.length}");
            if (!audioSource.isPlaying)
            {
                index++;
                if (index >= audios.Length) index = 0;
                Debug.Log("bgm loop");
                audioSource.clip = audios[index];
                audioSource.Play();
            }
        }
    }
}
