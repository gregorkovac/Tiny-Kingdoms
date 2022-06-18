using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScr : MonoBehaviour
{

    float timerInterval = 10f;
    float timer;

    float speed = 200f;

    public Color day;
    public Color night;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.PingPong(Time.time, speed) / speed;
       // Debug.Log(t);
        this.GetComponent<Light>().color = Color.Lerp(day, night, t);
        this.GetComponent<Light>().intensity = 1-t;
        this.GetComponent<Light>().intensity = Mathf.Clamp(this.GetComponent<Light>().intensity, 0.4f, 1f);
        transform.Rotate(Vector3.up * 1f * Time.deltaTime, Space.World);
    }
}
