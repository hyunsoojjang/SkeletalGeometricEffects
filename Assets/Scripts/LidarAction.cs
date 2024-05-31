
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;


//public delegate void LidarActionDelegate(Vector2 pos, int index);
public class LidarAction : MonoBehaviour
{


    public string scene1Name;
    public string scene2Name;
    public Action<Vector3, int> UpdateLidar;
    internal UrgSensing urgSensing;

  

    private void Start()
    {
        SetSceneLidarAction(SceneManager.GetActiveScene().name);
    }

    void SetSceneLidarAction(string sceneName)
    {
        if (!string.IsNullOrEmpty(scene1Name) && sceneName.Contains(scene1Name))     //Main
        {
          
            UpdateLidar = Scene1Lidar;
        }
        else if (!string.IsNullOrEmpty(scene2Name) && sceneName.Contains(scene2Name)) // Physics Fluid
        {
            UpdateLidar = Scene2Lidar;

        }
     
    }



    Vector4 vec4Pos;
    void Scene1Lidar(Vector3 pos, int i)
    {
 
        vec4Pos = (Vector4)pos;
     
        //vec4Pos.z = Camera.main.transform.position.z;
       
      
    }


    //Physics Fluid
   
    void Scene2Lidar(Vector3 pos, int i)
    {
     
        //if (Simulation2D.instance) Simulation2D.instance.interactionPointsData.Add(vec4Pos);
    }

    void Scene3Lidar(Vector3 pos, int i)
    {

       
    }
    void Scene4Lidar(Vector3 pos, int i)
    {

    }

    public bool isUrgSensing;
    private void Update()
    {
        if (urgSensing && urgSensing.sensedObjs.Count < 1 )
        {
           
            isUrgSensing = false;
        }
        else
        {
            isUrgSensing = true;
        }
    }


}
