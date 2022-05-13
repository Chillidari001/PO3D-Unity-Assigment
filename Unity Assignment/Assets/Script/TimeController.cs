using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    [SerializeField] private float timeMultiplier;
    [SerializeField] private float startHour;
    [SerializeField] private TextMeshProUGUI timeText;
    private DateTime realTime;

    [SerializeField] private Light sun;
    [SerializeField] private float sunriseHour;
    [SerializeField] private float sunsetHour;
    [SerializeField] private float maxSunIntensity;

    [SerializeField] private Light moon;
    [SerializeField] private float maxMoonIntensity;

    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime;

    [SerializeField] private Color dayAmbientLight;
    [SerializeField] private Color nightAmbientLight;
    [SerializeField] private AnimationCurve lightChangeCurve;

    void Start()
    {
        realTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);

        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
    }

    private void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
    }

    void UpdateTimeOfDay()
    {
        realTime = realTime.AddSeconds(Time.deltaTime * timeMultiplier);

        if (timeText != null)
        {
            timeText.text = realTime.ToString("HH:mm");
        }
    }

    private void RotateSun()
    {
        float sunRotation;

        if (realTime.TimeOfDay > sunriseTime && realTime.TimeOfDay < sunsetTime)
        {
            TimeSpan sunriseToSunsetDuration = TimeDifference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = TimeDifference(sunriseTime, realTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;

            sunRotation = Mathf.Lerp(0, 180, (float)percentage);
        }
        else
        {
            TimeSpan sunsetToSunriseDuration = TimeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = TimeDifference(sunsetTime, realTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;
            sunRotation = Mathf.Lerp(180, 360, (float)percentage);
        }

        sun.transform.rotation = Quaternion.AngleAxis(sunRotation, Vector3.right);
    }

    private void UpdateLightSettings()
    {
        float dotProduct = Vector3.Dot(sun.transform.forward, Vector3.down);
        sun.intensity = Mathf.Lerp(0, maxSunIntensity, lightChangeCurve.Evaluate(dotProduct));
        moon.intensity = Mathf.Lerp(maxMoonIntensity, 0, lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));
    }

    private TimeSpan TimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;

        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }
}
