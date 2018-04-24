using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Timers;
using UnityEngine;
using UnityEngine.Networking;
using Valve.VR.InteractionSystem;

public class MoveObjectRPC : MonoBehaviour, IMovementController, IMovementType
{
	[SerializeField] private GameObject _endPosition;

	public bool Moving { get; set; }
	public bool Done { get; set; }

	public MovementController _controller;
	public NetworkRPCCheck _networkStatusType;
	
	private void OnEnable()
	{
		_controller.setController(this);
		_networkStatusType = GameObject.FindObjectOfType<NetworkRPCCheck>();
	}

	// Use this for initialization
	void Start () 
	{
		if (_endPosition == null)
		{
			_endPosition = new GameObject();
			_endPosition.transform.position = transform.position 	+ new Vector3(3,0,3);
		}
	}

	//

	

	// Update is called once per frame
	void FixedUpdate ()
	{
		if (Input.GetButtonDown("Jump"))
		{
			print("Pressed Space");
			_controller.StartMoving();
		}
		_controller.CheckState(Moving,transform.position,_endPosition.transform.position, gameObject);
	}


	public void InitType()
	{
		this.transform.parent.GetComponent<NetworkTransform>().transformSyncMode =
			NetworkTransform.TransformSyncMode.SyncRigidbody3D;
		if (transform.parent.GetComponent<Rigidbody>() == null)
		{
			Rigidbody rigid = transform.parent.gameObject.AddComponent<Rigidbody>();
			rigid.useGravity = false;
		}
		else
		{
			transform.parent.GetComponent<Rigidbody>().useGravity = false;
		}
	}

	public void MoveGameObject(Vector3 origin, Vector3 endPosition, float speed, GameObject gameObj)
	{
		if (!_networkStatusType.isThisObejctServer())
		{
			return;
		}
		if (_endPosition == null)		
		{
			_endPosition = new GameObject();
		}
		_endPosition.transform.position = endPosition;
		_networkStatusType.RpcMoveObj(gameObj,GetNewPos(origin,endPosition,speed));
	}

	
	
	public Vector3 GetNewPos(Vector3 origin, Vector3 endPosition, float speed)
	{
		return Vector3.MoveTowards(origin, endPosition, speed);
	}
}


