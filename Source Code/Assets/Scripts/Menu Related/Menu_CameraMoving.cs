using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_CameraMoving : MonoBehaviour {
	public List<GameObject> targetsToImport;
	public LinkedList<GameObject> Targets;
	private LinkedListNode<GameObject> currentTarget;
	private bool hasToMove;
	public float[] speeds;
	public float SpeedUp = 1;
	public float rotationSpeed;
	public GameObject c;
	private float time;
	private int current;
	public Quaternion originalRotation;

	// Use this for initialization
	void Start () {
		Targets = new LinkedList<GameObject> ();
		foreach (var t in targetsToImport)
		{
			Targets.AddLast (t);
		}
		originalRotation.eulerAngles = transform.rotation.eulerAngles;
		for (int i = 0; i < speeds.Length; i++)
		{
			speeds[i] = speeds[i] * SpeedUp;
		}
		current = 0;
		currentTarget = Targets.First;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (hasToMove)
		{
			transform.rotation.SetLookRotation (originalRotation.eulerAngles);
			if (transform.position != currentTarget.Value.transform.position) {
				transform.position = Vector3.MoveTowards (transform.position, currentTarget.Value.transform.position, speeds [current]);

				time += Time.deltaTime;
			}
			if (transform.position == currentTarget.Value.transform.position & transform.rotation != currentTarget.Value.transform.rotation) {
//				transform.LookAt (new Vector3 (
//					Mathf.Clamp (transform.rotation.x, currentTarget.Value.transform.rotation.x, currentTarget.Value.transform.rotation.x),
//					Mathf.Clamp (transform.rotation.y, currentTarget.Value.transform.rotation.y, currentTarget.Value.transform.rotation.y),
//					Mathf.Clamp (transform.rotation.z, currentTarget.Value.transform.rotation.z, currentTarget.Value.transform.rotation.z)
//				));
				transform.rotation = currentTarget.Value.transform.rotation;

				//transform.Translate (Vector3.right * time * rotationSpeed);

				hasToMove = false;
				time = 0;
			}
		}
		if (currentTarget != null) {
			c = currentTarget.Value;
		}
	}

	public void GoToNextPoint()
	{
		if(currentTarget.Next != null)
		{
			originalRotation.eulerAngles = currentTarget.Next.Value.transform.rotation.eulerAngles;
			currentTarget = currentTarget.Next;
			current++;
			hasToMove = true;
		}

	}

	public void GoToPreviousPoint()
	{
		if(currentTarget.Previous != null)
		{
			originalRotation.eulerAngles = currentTarget.Previous.Value.transform.rotation.eulerAngles;
			currentTarget = currentTarget.Previous;
			current--;

			hasToMove = true;
		}
	}

	public void GoToSpecificPoint(string name)
	{
		if (!string.IsNullOrEmpty(name))
		{
			currentTarget = Targets.Find (GameObject.Find (name));

			if (currentTarget.Previous != null) {
				originalRotation.eulerAngles = currentTarget.Previous.Value.transform.rotation.eulerAngles;

			} 
			else {
				originalRotation.eulerAngles = currentTarget.Value.transform.rotation.eulerAngles;
			}
			hasToMove = true;
			Debug.Log ("Welp I'm moving now!");
		}
	}

}
