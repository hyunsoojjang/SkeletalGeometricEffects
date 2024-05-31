using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEvent : MonoBehaviour
{
   
  void FadeInEnd()
    {
        Wave_GameManager.instance.soundManager.gameObject.SetActive(true);
    }
}
