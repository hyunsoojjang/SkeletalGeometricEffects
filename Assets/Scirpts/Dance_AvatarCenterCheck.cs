using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dance_AvatarCenterCheck : MonoBehaviour
{

    string center = "Center";
    public bool  isCenter;
    public Vector3 smallScale;

    private void Update()
    {
        if (isCenter) 
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime*3);
        }
        else 
        { 
            transform.localScale = Vector3.Lerp(transform.localScale, smallScale, Time.deltaTime*3);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(center))
        {
            isCenter = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(center))
        {
            isCenter = false;
        }
    }
}
