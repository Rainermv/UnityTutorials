using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour {

    const float HOUR_ANGLE = 30f;
    const float MIN_ANGLE = 6f;
    const float SEC_ANGLE = 6f;

    public bool continuous;

    public Transform hoursTransform;
    public Transform minutesTransform;
    public Transform secondsTransform;

    void Awake() {

        Debug.Log(DateTime.Now);

    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        

        if (continuous) {
            UpdateContinuous();
        }
        else {
            UpdateDiscreet();
        }


       
    }

    void UpdateContinuous() {

        TimeSpan time = DateTime.Now.TimeOfDay;

        Quaternion hour_rot = Quaternion.Euler(0, (float)time.TotalHours * HOUR_ANGLE, 0f);
        Quaternion min_rot = Quaternion.Euler(0f, (float)time.TotalMinutes * MIN_ANGLE, 0f);
        Quaternion sec_rot = Quaternion.Euler(0f, (float)time.TotalSeconds * SEC_ANGLE, 0f);

        hoursTransform.localRotation = hour_rot;
        minutesTransform.localRotation = min_rot;
        secondsTransform.localRotation = sec_rot;

    }

    void UpdateDiscreet() {

        DateTime time = DateTime.Now;

        Quaternion hour_rot = Quaternion.Euler(0f, time.Hour * HOUR_ANGLE, 0f);
        Quaternion min_rot = Quaternion.Euler(0f, time.Minute * MIN_ANGLE, 0f);
        Quaternion sec_rot = Quaternion.Euler(0f, time.Second * SEC_ANGLE, 0f);

        hoursTransform.localRotation = hour_rot;
        minutesTransform.localRotation = min_rot;
        secondsTransform.localRotation = sec_rot;

    }
}
