using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShowUrg : MonoBehaviour
{

    public Camera ConnectedCam;
    public GameObject TouchPoint, TouchParent;
    public float ScaleX, ScaleY, ScaleZ, OffsetX, OffsetY, OffsetZ, PresetX;
    public Vector3 center;
    public bool ShowBool = true;
    public Vector3 PartiMoveVec;
    Vector3 InsPos, TargetPos;

    public Transform playerSphere;

    UrgControl ctrl;
    private void Awake()
    {
        ctrl = GetComponent<UrgControl>();
    }


    // Update is called once per frame
    void Start()
    {
        StartCoroutine(DelayedShowURGPoint());
    }

    IEnumerator DelayedShowURGPoint()
    {
        while (true)
        {
            if (ShowBool)
            {
                //if (ctrl.ip == "192.168.0.76")
                //{
                    ShowURGPoint3();
                    ShowBool = false;
                //}
                //else
                //{

                //    ShowURGPoint2();
                //    ShowBool = false;
                //}

                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                yield return null;
            }
        }
    }

    public void ShowURGPoint()
    {
        InsPos = new Vector3(-(center.x * (ScaleX - (PresetX * center.x)) + OffsetX), center.z * ScaleZ + OffsetZ, center.y * ScaleY + OffsetY);
        //Debug.Log(InsPos);

        TargetPos = new Vector3(-(center.x * (ScaleX - (PresetX * center.x)) + OffsetX), center.z * ScaleZ + OffsetZ, -90);
        Ray ray = new Ray(ConnectedCam.transform.position, TargetPos - ConnectedCam.transform.position);
        ////Ray ray = 

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {


        }

        //spawnPos = new Vector3(spawnPos.x, spawnPos.y, -30);


        ShowBool = false;
    }

    //public TransitionManager transitionManager;
    public GameObject targetPoscube;
    public void ShowURGPoint2()
    {
        InsPos = new Vector3(-(center.x * (ScaleX - (PresetX * center.x)) + OffsetX), center.z * ScaleZ + OffsetZ, center.y * ScaleY + OffsetY);
        //Debug.Log(InsPos);

        TargetPos = new Vector3((center.z * ScaleZ + OffsetZ), 0, -(center.x * (ScaleX - (PresetX * center.x)) + OffsetX));
        Ray ray = new Ray(TargetPos, Vector3.down);
        ////Ray ray = 
        targetPoscube.transform.position = TargetPos;


        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {

            GameObject hitObject = hit.collider.gameObject;

            Debug.Log(11111111111111);
            if (hitObject.CompareTag("Floor"))
            {


                Debug.Log(hitObject.name);
                //if (transitionManager.currentFloorParticle)
                //{
                //    GameObject particle = Instantiate(transitionManager.currentFloorParticle, hit.point, Quaternion.identity);

                //}

            }

        }

        //spawnPos = new Vector3(spawnPos.x, spawnPos.y, -30);


        ShowBool = false;
    }
     public void ShowURGPoint3()
    {
        InsPos = new Vector3(-(center.x * (ScaleX - (PresetX * center.x)) + OffsetX), center.z * ScaleZ + OffsetZ, center.y * ScaleY + OffsetY);
        //Debug.Log(InsPos);

        TargetPos = new Vector3(-(center.z * ScaleZ + OffsetZ), 10, (center.x * (ScaleX - (PresetX * center.x)) + OffsetX));
        Ray ray = new Ray(TargetPos, Vector3.down);
        ////Ray ray = 
        targetPoscube.transform.position = TargetPos;


        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {

            GameObject hitObject = hit.collider.gameObject;

          
            if (hitObject.CompareTag("Floor"))
            {
                playerSphere.transform.position = hit.point;

                Debug.Log(hitObject.name);
                //if (transitionManager.currentFloorParticle)
                //{
                //    GameObject particle = Instantiate(transitionManager.currentFloorParticle, hit.point, Quaternion.identity);

                //}

            }

        }

        //spawnPos = new Vector3(spawnPos.x, spawnPos.y, -30);


        ShowBool = false;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawLine(ConnectedCam.transform.position, TargetPos);
    //}
}
