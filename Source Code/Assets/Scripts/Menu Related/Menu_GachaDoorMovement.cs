using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_GachaDoorMovement : MonoBehaviour {
	private Rigidbody rb;
	public float speed;
	public DoorState ds;
	private Transform startTrans;
	public bool isMoving;
	public Transform ClosedTrans;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
		ds = DoorState.Open;
		isMoving = false;
		startTrans = rb.transform;
	}
	// Update is called once per frame
	void Update () {
		if (ds == DoorState.Closed && isMoving) {
			transform.position = Vector3.MoveTowards(transform.position, startTrans.position, 5f);

			//rb.velocity = transform.up * speed;
		}
		if (ds == DoorState.Open && isMoving) {
			//rb.velocity = -transform.up * speed;
			transform.position = Vector3.MoveTowards(transform.position, ClosedTrans.position, 5f);
		}
		if (rb.transform.position == startTrans.position & isMoving) {
			isMoving = false;
			ds = DoorState.Open;
		}
		if (rb.transform.position == ClosedTrans.position & isMoving) {
			isMoving = false;
			ds = DoorState.Closed;
		}
	}

	public void Open()
	{
		if (ds == DoorState.Closed) {
			isMoving = true;
		}

	}

	public void Close()
	{
		if (ds == DoorState.Open) {
			isMoving = true;
		}

	}
}

public enum DoorState{
	Open,
	Closed
}