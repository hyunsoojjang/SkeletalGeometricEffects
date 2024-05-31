using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wave_DebugUI : MonoBehaviour
{
    public static Wave_DebugUI instance;
    public Text debugText;
    public Text errorText;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
  
}
