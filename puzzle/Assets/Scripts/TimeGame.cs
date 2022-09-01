using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeGame : MonoBehaviour
{
    public Slider slider;
    public float time = 10f;
    public float times = 10f;

    public static TimeGame Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    
    public void addTime(float _time)
    {
        time += _time;
    }

    void Update()
    {
        time -= Time.deltaTime;

        slider.value = time / times;

        if(time > times)
        {
            time = 20;
        }

    }
}
