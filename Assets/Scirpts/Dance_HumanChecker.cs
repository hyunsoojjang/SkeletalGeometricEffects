using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dance_HumanChecker : MonoBehaviour
{

   

    public bool someoneinCenter;
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("avatarHip"))
    //    {
    //        someoneinCenter = true;
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("avatarHip"))
    //    {
    //        someoneinCenter = false;

    //    }
    //}

    private void Start()
    {
        checkRadius = transform.localScale.x*0.5f;
    }
    public float checkRadius;

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius);

        someoneinCenter = false;
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("avatarHip"))
            {
                someoneinCenter = true;
                break;
            }
        }

      
    }
}
