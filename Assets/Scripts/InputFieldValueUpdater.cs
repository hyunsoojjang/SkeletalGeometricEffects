using UnityEngine;
using UnityEngine.UI;

public class InputFieldValueUpdater : MonoBehaviour
{
    InputField inputFied;
    URGPosUI urgposui;

    private void Start()
    {
        urgposui = transform.root.GetComponent<URGPosUI>();
        inputFied = GetComponent<InputField>();
        //valueText = transform.Find("ValueText").GetComponent<Text>();
        //valueText.text = slider.value.ToString();

        inputFied.onValueChanged.AddListener(OnSliderValueChanged);
        inputFied.onEndEdit.AddListener(OnSliderValueChanged);

    }


    public void OnSliderValueChanged(string value)
    {
        //valueText.text = slider.value.ToString();
        urgposui.SetURGvalueByInputField();
    }


}