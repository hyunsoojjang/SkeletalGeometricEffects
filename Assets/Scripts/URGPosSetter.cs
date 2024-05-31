using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



//  ★카메라 Position (0,10,0) Rotatation (90,0,0)  세팅★
//  ★Perpective Camera 와 OrthoCamera의 포지션과 로테이션은 같아야함★
public class URGPosSetter : MonoBehaviour
{
    UrgSensing urgSensing;
    public URGPosUI URGPosUI;
    public Camera orthoCam;
    public Camera perspectiveCamera;
    LidarAction liadrAction;

    public enum URGNumber
    {
        urg1,
        urg2,
    }
    public enum CamState
    {
        Orthographic
            ,
        Perspective
    }
    public enum LidarState  // Unity Vector Foward 기준으로
    {
        LookFoward,
        LookBackward,
        LookRight,
        LookLeft,
    }
    public enum CrossState  // xy-> vertical  // xz->horizontal
    {
        Horizontal,
        Vertical
    }

    const int StandardWidth = 1920; //라이다 세팅한 기준해상도
    const int StandardHeight = 1080;//라이다 세팅한 기준해상도

    [Header("URGNumber")]
    [Space(3)]
    public URGNumber urgNumber;
    [Header("CamSate")]
    [Space(3)]
    public CamState camState;
    [Header("Lidar State")]
    [Space(3)]
    public LidarState lidarState;
    [Header("Cross State")]
    [Space(3)]
    public CrossState crossState;

    [Space(10)]
    [Header("Sensing Cube")]
    [Space(3)]
    public List<GameObject> sensingCubePool;
    public List<GameObject> sensingBallCubePool;
    public GameObject sensingCube;
    public GameObject ballCube;
    public int sensingCubeCount;
    float sensingCubeSize;

    public bool ActiveSensingCube;
    public bool NormalizedVecter;



    [Space(10)]
    [Header("Position Setting")]
    [Space(3)]
    //public float ScaleX;
    //public float ScaleY;
    //public float ScaleZ;

    public float offsetX;   // x 보정값
    public float OffsetX
    {
        get { return offsetX; }
        set
        {


            offsetX = value;
           
            if (URGPosUI.useSlider)
            {
                if (URGPosUI.ValueSliders[0] != null)
                {


                    URGPosUI.ValueSliders[0].value = offsetX;


                }

            }
            else
            {
                if (URGPosUI.ValueInputs[0] != null)
                {


                    URGPosUI.ValueInputs[0].text = offsetX.ToString();


                }
            }
        }
    }


    public float offsetY;  // y 보정 값 (높이)
    public float OffsetY
    {
        get { return offsetY; }
        set
        {
            offsetY = value;

            if (URGPosUI.useSlider)
            {
                if (URGPosUI.ValueSliders[1] != null)
                {


                    URGPosUI.ValueSliders[1].value = offsetY;


                }

            }
            else
            {
                if (URGPosUI.ValueInputs[1] != null)
                {


                    URGPosUI.ValueInputs[1].text = offsetY.ToString();


                }
            }
        }
    }

    public float offsetZ; // z 보정값
    public float OffsetZ
    {
        get { return offsetZ; }
        set
        {
            offsetZ = value;

            if (URGPosUI.useSlider)
            {
                if (URGPosUI.ValueSliders[2] != null)
                {


                    URGPosUI.ValueSliders[2].value = offsetZ;


                }

            }
            else
            {
                if (URGPosUI.ValueInputs[2] != null)
                {


                    URGPosUI.ValueInputs[2].text = offsetZ.ToString();


                }
            }
        }
    }


    public float PresetX;
    public float PresetZ;

    public float realScaleRate;
    public float RealScaleRate
    {
        get { return realScaleRate; }
        set
        {
            realScaleRate = value;

            if (URGPosUI.useSlider)
            {
                if (URGPosUI.ValueSliders[3] != null)
                {


                    URGPosUI.ValueSliders[3].value = realScaleRate;


                }

            }
            else
            {
                if (URGPosUI.ValueInputs[3] != null)
                {


                    URGPosUI.ValueInputs[3].text = realScaleRate.ToString();


                }
            }
        }
    }

    [Space(10)]
    [Header("<Right Offset>")]
    public bool isUsingRightOffset; // 라이다 기준 좌우 따로 보정
    public float rightOffsetX;   // x 보정값
    public float RightOffsetX
    {
        get { return rightOffsetX; }
        set
        {
            rightOffsetX = value;

            if (URGPosUI.useSlider)
            {
                if (URGPosUI.ValueSliders[4] != null)
                {


                    URGPosUI.ValueSliders[4].value = rightOffsetX;


                }

            }
            else
            {
                if (URGPosUI.ValueInputs[4] != null)
                {


                    URGPosUI.ValueInputs[4].text = rightOffsetX.ToString();


                }
            }
        }
    }

    public float rightOffsetZ; // z 보정값
    public float RightOffsetZ
    {
        get { return rightOffsetZ; }
        set
        {
            rightOffsetZ = value;

            if (URGPosUI.useSlider)
            {
                if (URGPosUI.ValueSliders[5] != null)
                {


                    URGPosUI.ValueSliders[5].value = rightOffsetZ;


                }

            }
            else
            {
                if (URGPosUI.ValueInputs[5] != null)
                {


                    URGPosUI.ValueInputs[5].text = rightOffsetZ.ToString();


                }
            }
        }
    }

    public float rightRealScaleRate;
    public float RightRealScaleRate
    {
        get { return rightRealScaleRate; }
        set
        {
            rightRealScaleRate = value;

            if (URGPosUI.useSlider)
            {
                if (URGPosUI.ValueSliders[6] != null)
                {


                    URGPosUI.ValueSliders[6].value = rightRealScaleRate;


                }

            }
            else
            {
                if (URGPosUI.ValueInputs[6] != null)
                {


                    URGPosUI.ValueInputs[6].text = rightRealScaleRate.ToString();


                }
            }
        }
    }
    [Space(20)]


    public float ScreenWidth; // 현재 해상도 X
    public float ScreenHeight; // 현재 해상도 Y

    public float OrthoCamSize; // 0,0 에서 해상도의 위아래 끝까지 거리
    public float CenterToSideSize; // 0,0 에서 해상도 좌우 끝까지 거리


    public bool isLivingScene;

    void Start()
    {
        liadrAction = GetComponent<LidarAction>();
        if (!orthoCam || !perspectiveCamera)
        {
            Debug.LogWarning("Unassigned Camera!");
            return;
        }

        switch (camState)
        {
            case CamState.Orthographic:
                if (perspectiveCamera) perspectiveCamera.gameObject.SetActive(false);
                break;
            case CamState.Perspective:
                //orthoCam.orthographicSize = CalculateOrthoSizeFromFOV(perspectiveCamera.fieldOfView);
                if (perspectiveCamera) perspectiveCamera.fieldOfView = CalculateFOVFromOrthoSize(orthoCam.orthographicSize, -orthoCam.transform.position.z);
                break;
        }


        ScreenWidth = Screen.width;
        ScreenHeight = Screen.height;

        OrthoCamSize = orthoCam.orthographicSize;


        CenterToSideSize = ScreenWidth * OrthoCamSize / ScreenHeight;



        //sensingCubeSize = OrthoCamSize * 0.05f;
        sensingCubeSize = 0.4F;


        switch (urgNumber)
        {
            case URGNumber.urg1:
                urgSensing = GameObject.Find("urg1").GetComponent<UrgSensing>();
                break;
            case URGNumber.urg2:
                urgSensing = GameObject.Find("urg2").GetComponent<UrgSensing>();
                break;
        }



        if (ActiveSensingCube) CreateSensingCube(); // Debuging Cube
        //CreateBallSeinsingCube();
        if (NormalizedVecter) CalculateNormalized();  // was used  Cam Dependennt Shader project

        if (SceneManager.GetActiveScene().name.Contains("Living"))
        {
            isLivingScene = true;
        }
        else if (SceneManager.GetActiveScene().name.Contains("Fluid"))
        {
            isLivingScene = false;
        }


        //positions = new Vector3[ObjectManager.instatnce.objs.Length];
    }
    void CalculateNormalized()
    {
        OrthoCamSize = 0.5f;
        CenterToSideSize = ScreenWidth * OrthoCamSize / ScreenHeight;
    }
    void CreateSensingCube()
    {
        for (int i = 0; i < sensingCubeCount; i++)
        {
            GameObject cube = Instantiate(sensingCube);
            cube.name = "Cube" + i;
            cube.transform.localScale = new Vector3(sensingCubeSize, sensingCubeSize, sensingCubeSize);
            sensingCubePool.Add(cube);
        }
    }
    void CreateBallSeinsingCube()
    {
        for (int i = 0; i < sensingCubeCount; i++)
        {
            GameObject cube = Instantiate(ballCube);
            cube.name = "BallSensingCube" + i;
            cube.transform.localScale = new Vector3(sensingCubeSize, sensingCubeSize, sensingCubeSize);
            sensingBallCubePool.Add(cube);
        }
    }

    public Vector3 center;
    void Update()
    {
        CalculatePosRay();
    }

    Vector3 ResizingCenter(Vector3 center)
    {
        if (!isUsingRightOffset)
        {

            return new Vector3
                        (
                        (center.x * (OrthoCamSize - (PresetX * center.x)) / (realScaleRate/*StandardWidth / ScreenWidth*/) + (OffsetX * OrthoCamSize))     //x
                        , OffsetY                                                                                                   //y
                        , (center.z * (OrthoCamSize - (PresetZ * center.z)) / (realScaleRate/*StandardHeight / ScreenHeight*/) + (OffsetZ * OrthoCamSize))         //z
                        );
        }
        else
        {
            if (center.x >= 0)
            {
                return new Vector3
                   (
                   (center.x * (OrthoCamSize - (-PresetX * center.x)) / (RightRealScaleRate/*StandardWidth / ScreenWidth*/) + (RightOffsetX * OrthoCamSize))     //x
                   , OffsetY                                                                                                   //y
                   , (center.z * (OrthoCamSize - (-PresetZ * center.z)) / (RightRealScaleRate/*StandardHeight / ScreenHeight*/) + (RightOffsetZ * OrthoCamSize))         //z
                   );
            }
            else
            {
                return new Vector3
                 (
                 (center.x * (OrthoCamSize - (PresetX * center.x)) / (realScaleRate/*StandardWidth / ScreenWidth*/) + (OffsetX * OrthoCamSize))     //x
                 , OffsetY                                                                                                   //y
                 , (center.z * (OrthoCamSize - (PresetZ * center.z)) / (realScaleRate/*StandardHeight / ScreenHeight*/) + (OffsetZ * OrthoCamSize))         //z
                 );
            }
        }

    }

    Vector3 finalPos;
    //List<Vector3> positions = new List<Vector3>();
    void CalculatePosRay()
    {
        //positions.Clear();
        if (urgSensing.sensedObjs.Count > 0)
        {
            for (int i = 0; i < urgSensing.sensedObjs.Count; i++)
            {


                center = urgSensing.sensedObjs[i].center;

                //Debug.Log("URG Center Point : " + center);
                finalPos = ResizingCenter(center);



                switch (lidarState)
                {
                    case LidarState.LookFoward: //  center x z 증가시 cube x z 증가
                        finalPos = new Vector3(finalPos.x, OffsetY, finalPos.z - OrthoCamSize);
                        break;
                    case LidarState.LookBackward:  // center x  증가시  cube x 감소 
                        finalPos = new Vector3(-finalPos.x, OffsetY, -finalPos.z + OrthoCamSize);
                        break;
                    case LidarState.LookRight: // center x  증가시  cube z 감소 
                        finalPos = new Vector3(finalPos.z - CenterToSideSize, OffsetY, -finalPos.x);
                        break;
                    case LidarState.LookLeft: // center x  증가시  cube z 증가 
                        finalPos = new Vector3(-finalPos.z + CenterToSideSize, OffsetY, finalPos.x);
                        break;
                }

                switch (crossState)
                {
                    case CrossState.Horizontal:
                        break;
                    case CrossState.Vertical:
                        finalPos = new Vector3(finalPos.x, finalPos.z, 0);
                        break;
                }

                if (ActiveSensingCube) SensingCubePooling(finalPos,center);

               if(liadrAction.UpdateLidar!=null) liadrAction.UpdateLidar(finalPos, i);
                //ObjectManager.instatnce.ObjMove(finalPos, i);

                //positions.Add(finalPos);
                //if (isLivingScene) FloorRay(pos);
                //else if (!isLivingScene) AddFluidList(pos);

            }

            //SengsingBallCubePosSet();
            //ObjectManager.instatnce.pos=positions;

        }
        else
        {

            SensingBallCubeInit();
        }

        //ObjectManager.instatnce.ObjMove(positions, urgSensing.sensedObjs.Count);
    }

    void AddFluidList(Vector2 pos)
    {

        //fluid.calculatePos.Add(pos);
    }
    void SensingCubePooling(Vector3 pos, Vector3 centerPos)
    {
        sensingCubePool[0].transform.position = pos;
        if (centerPos.x > 0) sensingCubePool[0].GetComponent<Renderer>().material.color = Color.red;
        else sensingCubePool[0].GetComponent<Renderer>().material.color = Color.white;
        GameObject first = sensingCubePool[0];
        sensingCubePool.RemoveAt(0);
        sensingCubePool.Add(first);
    }
    void SensingBallCubePooling(Vector3 pos)
    {
        sensingBallCubePool[0].transform.position = pos;
        GameObject first = sensingBallCubePool[0];
        sensingBallCubePool.RemoveAt(0);
        sensingBallCubePool.Add(first);
    }

    Vector3 pos = new Vector3(500, 500, 500);
    void SensingBallCubeInit()
    {
        foreach (var cube in sensingBallCubePool)
        {
            cube.transform.position = pos;
        }
    }
    int frame;
    void FloorRay(Vector3 pos)
    {

        RaycastHit hit;
        Ray ray = new Ray(pos, Vector3.down);
        if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Floor")))
        {

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);


                //curParticle.affectors.Insert(0, hit.point);
                //if (curParticle.affectors.Count >= 20)
                //{
                //    curParticle.affectors.RemoveAt(curParticle.affectors.Count - 1);
                //}


                //frame++;
                //if (frame>250)
                //{
                //    GameObject go = Instantiate(lineParticle);

                //    go.transform.position = hit.point;
                //    frame = 0;
                //}


            }
        }
    }
    private float CalculateOrthoSizeFromFOV(float fov)
    {
        // Using tangent formula to calculate orthographic size
        float halfFovRad = fov * 0.5f * Mathf.Deg2Rad;
        float distance = perspectiveCamera.transform.position.y; // Distance from camera to target
        float orthoSize = distance * Mathf.Tan(halfFovRad);
        return orthoSize;
    }
    private float CalculateFOVFromOrthoSize(float orthoSize, float distance)
    {
        // Using arctangent formula to calculate FOV
        float halfFovRad = Mathf.Atan(orthoSize / distance);
        float fov = halfFovRad * 2f * Mathf.Rad2Deg;
        return fov;
    }
}
