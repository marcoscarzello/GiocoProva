using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLightAlarm : MonoBehaviour
{
    private bool _alarm = false;
    private Light lt;
    private Color _actualColor;

    [SerializeField] private float _duration = 1.0f;
    [SerializeField] private Color _alarmColor;

    public void Alarm()
    {
        _alarm = true;
    }

    void Start()
    {
        lt = GetComponent<Light>();
        _actualColor = lt.color;
    }

    
    void Update()
    {
        if (_alarm)
        {
            float t = Mathf.PingPong(Time.time, _duration) / _duration;
            lt.color = Color.Lerp(_actualColor, _alarmColor, t);
        }

        //if (Mathf.RoundToInt(Time.time) == 5)
        //    Alarm();
    }
}
