using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class URGPosUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider[] ValueSliders;
    public InputField[] ValueInputs;
    public URGPosSetter urgPosSetter;
    public UrgControl urgCtrl;

    public bool useSlider;

    public GameObject urgposuiSlider;
    public GameObject urgposuiInput;

    GameObject curUrgPosUi;

    void Awake()
    {
        urgposuiSlider.SetActive(false);
        urgposuiInput.SetActive(false);
        if (useSlider)
        {
            curUrgPosUi = urgposuiSlider;
        ValueSliders = new Slider[urgposuiSlider.transform.childCount];
        for (int i = 0; i < urgposuiSlider.transform.childCount; i++)
        {
            ValueSliders[i] = urgposuiSlider.transform.GetChild(i).GetComponent<Slider>();
        }
        ValueSliders[0].value = urgPosSetter.OffsetX;
        ValueSliders[1].value = urgPosSetter.OffsetY;
        ValueSliders[2].value = urgPosSetter.OffsetZ;
        ValueSliders[3].value = urgPosSetter.realScaleRate;

        ValueSliders[4].value = urgPosSetter.RightOffsetX;
        ValueSliders[5].value = urgPosSetter.RightOffsetZ;
        ValueSliders[6].value = urgPosSetter.RightRealScaleRate;
        //ValueSliders[7].value = urgCtrl.angleOffset;

        }
        else
        {
            curUrgPosUi = urgposuiInput;
            ValueInputs = new InputField[urgposuiInput.transform.childCount];
            for (int i = 0; i < urgposuiInput.transform.childCount; i++)
            {
                ValueInputs[i] = urgposuiInput.transform.GetChild(i).GetComponent<InputField>();
            }
            ValueInputs[0].text = urgPosSetter.OffsetX.ToString();
            ValueInputs[1].text = urgPosSetter.OffsetY.ToString();
            ValueInputs[2].text = urgPosSetter.OffsetZ.ToString();
            ValueInputs[3].text = urgPosSetter.realScaleRate.ToString();

            ValueInputs[4].text = urgPosSetter.RightOffsetX.ToString();
            ValueInputs[5].text = urgPosSetter.RightOffsetZ.ToString();
            ValueInputs[6].text = urgPosSetter.RightRealScaleRate.ToString();
            //ValueSliders[7].text = urgCtrl.angleOffset;
        }

    }
    public void SetURGvalueBySlider()
    {
        urgPosSetter.OffsetX = ValueSliders[0].value;
        urgPosSetter.OffsetY = ValueSliders[1].value;
        urgPosSetter.OffsetZ = ValueSliders[2].value;
        urgPosSetter.realScaleRate = ValueSliders[3].value;

        urgPosSetter.RightOffsetX = ValueSliders[4].value;
        urgPosSetter.RightOffsetZ = ValueSliders[5].value;
        urgPosSetter.RightRealScaleRate = ValueSliders[6].value;
        //urgCtrl.angleOffset = ValueSliders[7].value;



    }
    public void SetURGvalueByInputField()
    {
   
        if (float.TryParse(ValueInputs[0].text, out float x))
        {
            urgPosSetter.OffsetX = x;
        }
        if (float.TryParse(ValueInputs[1].text, out float y))
        {
            urgPosSetter.OffsetY = y;
        }
        if (float.TryParse(ValueInputs[2].text, out float z))
        {
            urgPosSetter.OffsetZ = z;
        }
        if (float.TryParse(ValueInputs[3].text, out float scale))
        {
            urgPosSetter.realScaleRate = scale;
        }
        if (float.TryParse(ValueInputs[4].text, out float rx))
        {
            urgPosSetter.RightOffsetX = rx;
        }
        if (float.TryParse(ValueInputs[5].text, out float rz))
        {
            urgPosSetter.RightOffsetZ = rz;
        }
        if (float.TryParse(ValueInputs[6].text, out float rScale))
        {
            urgPosSetter.RightRealScaleRate = rScale;
        }
        

        //urgPosSetter.OffsetX = float.Parse( ValueInputs[0].text);
        //urgPosSetter.OffsetY = float.Parse(ValueInputs[1].text);
        //urgPosSetter.OffsetZ = float.Parse(ValueInputs[2].text);
        //urgPosSetter.realScaleRate = float.Parse(ValueInputs[3].text);

        //urgPosSetter.RightOffsetX = float.Parse(ValueInputs[4].text);
        //urgPosSetter.RightOffsetZ = float.Parse(ValueInputs[5].text);
        //urgPosSetter.RightRealScaleRate = float.Parse(ValueInputs[6].text);
        ////urgCtrl.angleOffset = ValueSliders[7].value;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (curUrgPosUi.activeSelf)
            {
                curUrgPosUi.SetActive(false);

            }
            else
            {
                curUrgPosUi.SetActive(true);

            }
        }
    }


}
