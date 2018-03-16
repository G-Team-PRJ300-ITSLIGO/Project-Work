using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_cameraTarget : MonoBehaviour
{
    public float SpeedUp = 1;
    public bool FastMode = false;

    private float time;
    private int current;
    public GameObject[] points;
    public float[] speeds;
    private string rotationCommand;

    // Use this for initialization

    public GameObject target;


    void Start()
    {
        current = 0;
        for (int i = 0; i < speeds.Length; i++)
        {
            speeds[i] = speeds[i] * SpeedUp;
        }
    }

    void Update()
    {

        //if (FastMode == true)
        //{
        //    for (int i = 0; i < speeds.Length; i++)
        //    {
        //        speeds[i] = speeds[i] * SpeedUp;
        //    }
        //    FastMode = false;
        //}

        if (!(current >= points.Length))
        {

            transform.position = Vector3.MoveTowards(transform.position, points[current].transform.position, speeds[current]);
            rotationCommand = points[current].tag;

            time += Time.deltaTime;
        }
        if (current < points.Length)
            if (transform.position == points[current].transform.position)
            {
                current++;
                time = 0;
            }
    }
}
