using UnityEngine;
using UnityEngine.UI;

public class SliderValueUpdater : MonoBehaviour
{
     Slider slider;
    Text valueText;
    URGPosUI urgposui;

    private void Start()
    {
        urgposui = transform.root.GetComponent<URGPosUI>();
        slider = GetComponent<Slider>();
        valueText = transform.Find("ValueText").GetComponent<Text>();
        valueText.text = slider.value.ToString();

        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

 
    public void OnSliderValueChanged(float newValue)
    {
        valueText.text = slider.value.ToString();
        urgposui.SetURGvalueBySlider();
    }

  
}