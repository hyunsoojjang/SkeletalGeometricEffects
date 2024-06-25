using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dance_RigFix : MonoBehaviour
{
    public Transform zedRig;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        zedRig.position = transform.position;


    }
}
