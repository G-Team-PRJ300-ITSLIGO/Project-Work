using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_camera : MonoBehaviour {

    public float SpeedUp = 1;
    public bool FastMode = false;

    public float[] speeds;

    public Quaternion originalRotation;
    public float requiredRotation;
    public float rotationSpeed;
    private float time;
    private int current;
    private int currentTarget;
    public GameObject[] points;
    private string rotationCommand;

    public GameObject target;


	void Start () {
        current = 0;
        currentTarget = 0;
        originalRotation = transform.rotation;
        for (int i = 0; i < speeds.Length; i++)
        {
            speeds[i] = speeds[i] * SpeedUp;
        }
    }
	
	void Update () {

        transform.LookAt(target.transform.position);
        if (!(current >= points.Length))
        {

            transform.position = Vector3.MoveTowards(transform.position, points[current].transform.position, speeds[current]);

            time += Time.deltaTime;
        }
        if (current < points.Length)
            if (transform.position == points[current].transform.position)
            {
                transform.LookAt(target.transform.position);
                transform.Translate(Vector3.right * time * rotationSpeed);
                current++;
                time = 0;
            }
        currentTarget++;
    }
}
