using GeoFx;
using NuitrackSDK;
using Skinner;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dance_AvatarContorller : MonoBehaviour
{
    public Transform hip;
    Vector3 initHippos;
    public Skeleton slekVfx;
    public SkinnerGlitch skinnerGlitch;
    public SkinnerParticle skinnerParticle1;
    public SkinnerParticle skinnerParticle2;
    public SkinnerTrail skinnerTrail;


    public enum State
    {
        Skeleton,
        Skinner
    }
    public State state;
    public enum SkinnerState
    {
        Particle1,
        Particle2,
        Trail
    }
    public SkinnerState skinnerState;

    void Start()
    {
        initHippos = hip.position;

        NuitrackManager.Users.OnUserEnter += Users_OnUserEnter;
        NuitrackManager.Users.OnUserExit += Users_OnUserExit; ;
    }
    private void Users_OnUserEnter(UserData user)
    {
        StopCoroutine(nameof(IEUserExit));

        StopCoroutine(nameof(IEUserEnter));
        StartCoroutine(nameof(IEUserEnter));
    }
    IEnumerator IEUserEnter()
    {
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            switch (state)
            {
                case State.Skeleton:
                    SkeletionVisibleLerp();
                    SkinnerInVisibleLerp();
                    break;
                case State.Skinner:
                    SkeletionInVisibleLerp();
                    SkinnerVisibleLerp();
                    break;

            }


            yield return null;
        }





    }




    void SkeletionVisibleLerp()
    {
        if (slekVfx != null
            && slekVfx.gameObject.activeSelf)
        {
            slekVfx._stripWidth = Mathf.Lerp(slekVfx._stripWidth, 0.01f, Time.deltaTime * 0.5f);
            slekVfx._baseRadius = Mathf.Lerp(slekVfx._baseRadius, 0.1f, Time.deltaTime * 0.5f);
            //slekVfx._stripSpeed = Mathf.Lerp(slekVfx._stripSpeed, 1, Time.deltaTime);
            slekVfx._stripLength = Mathf.Lerp(slekVfx._stripLength, 3, Time.deltaTime * 0.5f);
        }
    }
    void SkeletionInVisibleLerp()
    {
        if (slekVfx != null)
        {
            slekVfx._stripWidth = Mathf.Lerp(slekVfx._stripWidth, 0, Time.deltaTime * 2f);
            slekVfx._baseRadius = Mathf.Lerp(slekVfx._baseRadius, 0.4f, Time.deltaTime * 2f);
            //slekVfx._stripSpeed = Mathf.Lerp(slekVfx._stripSpeed,3, Time.deltaTime * 2f);
            slekVfx._stripLength = Mathf.Lerp(slekVfx._stripLength, 0, Time.deltaTime * 2f);


        }
    }
    void SkinnerVisibleLerp()
    {
        if (skinnerGlitch && skinnerParticle1 && skinnerParticle2 && skinnerTrail)
        {
            skinnerGlitch.edgeThreshold = Mathf.Lerp(skinnerGlitch.edgeThreshold, 0.3f, Time.deltaTime);

            switch (skinnerState)
            {
                case SkinnerState.Particle1:
                    skinnerParticle1._speedToScale = Mathf.Lerp(skinnerParticle1._speedToScale, 0.3f, Time.deltaTime);
                    skinnerParticle2._speedToScale = Mathf.Lerp(skinnerParticle2._speedToScale, 0, Time.deltaTime);
                    skinnerTrail.maxWidth = Mathf.Lerp(skinnerTrail.maxWidth, 0, Time.deltaTime);

                    break;
                case SkinnerState.Particle2:
                    skinnerParticle2._speedToScale = Mathf.Lerp(skinnerParticle2._speedToScale, 0.05f, Time.deltaTime);

                    skinnerParticle1._speedToScale = Mathf.Lerp(skinnerParticle1._speedToScale, 0, Time.deltaTime);
                    skinnerTrail.maxWidth = Mathf.Lerp(skinnerTrail.maxWidth, 0, Time.deltaTime);

                    break;
                case SkinnerState.Trail:
                    skinnerTrail.maxWidth = Mathf.Lerp(skinnerTrail.maxWidth, 0.05f, Time.deltaTime);


                    skinnerParticle2._speedToScale = Mathf.Lerp(skinnerParticle2._speedToScale, 0, Time.deltaTime);


                    break;

            }
            //slekVfx._stripSpeed = Mathf.Lerp(slekVfx._stripSpeed, 1, Time.deltaTime);

        }
    }
    void SkinnerInVisibleLerp()
    {
        if (skinnerGlitch && skinnerParticle1 && skinnerParticle2 && skinnerTrail)
        {
            skinnerGlitch.edgeThreshold = Mathf.Lerp(skinnerGlitch.edgeThreshold, 0f, Time.deltaTime * 2);
            skinnerParticle1._speedToScale = Mathf.Lerp(skinnerParticle1._speedToScale, 0, Time.deltaTime * 2);
            skinnerParticle2._speedToScale = Mathf.Lerp(skinnerParticle2._speedToScale, 0, Time.deltaTime * 2);
            skinnerTrail.maxWidth = Mathf.Lerp(skinnerTrail.maxWidth, 0, Time.deltaTime * 2);

        }
    }
    private void Users_OnUserExit(UserData user)
    {
        StopCoroutine(nameof(IEUserEnter));
        StopCoroutine(nameof(IEUserExit));
        StartCoroutine(nameof(IEUserExit));
    }


    IEnumerator IEUserExit()
    {
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            switch (state)
            {
                case State.Skeleton:
                    SkeletionInVisibleLerp();
                    SkinnerInVisibleLerp();
                    break;
                case State.Skinner:
                    SkeletionInVisibleLerp();
                    SkinnerInVisibleLerp();
                    break;
                default:
                    break;
            }
            yield return null;
        }
    }

    int skinnerIdx;
    float skinnerLapseTime;
    public float skinnerChangeTime;

    int stateIdx;
    float curT;
    public float stateChageTime;

    private void Update()
    {
        curT += Time.deltaTime;
        if (curT >= stateChageTime)
        {
            stateIdx++;
            int idx = (int)state + 1;
            if (idx >= Enum.GetValues(typeof(State)).Length) idx = 0;
            state = (State)idx;

            curT = 0;
            skinnerLapseTime = 0;
        }




        if (state == State.Skinner)
        {
            skinnerLapseTime += Time.deltaTime;
            if (skinnerLapseTime >= skinnerChangeTime)
            {
                skinnerIdx++;
                int idx = (int)skinnerState + 1;
                if (idx >= Enum.GetValues(typeof(SkinnerState)).Length) idx = 0;
                skinnerState = (SkinnerState)idx;
                skinnerLapseTime = 0;
            }
        }
    }

    // Update is called once per frame

    private void OnDestroy()
    {
        NuitrackManager.Users.OnUserEnter -= Users_OnUserEnter;
        NuitrackManager.Users.OnUserExit -= Users_OnUserExit; ;

    }
}
