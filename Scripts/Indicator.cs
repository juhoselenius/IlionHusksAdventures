using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    
    //controls the close combat circle UI


    CloseCombat cc;
    Slider slider;
    float maxVal;
    float minVal;

    void Start()
    {
        cc = FindObjectOfType<CloseCombat>();
        slider = GetComponentInChildren<Slider>();
        minVal = cc.cooldownTimer;
        maxVal = 1;
        slider.maxValue = maxVal;
        slider.minValue = 0;
        slider.minValue -= minVal;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = cc.timer;
    }
}
