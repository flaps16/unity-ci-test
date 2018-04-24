+using System;
 +using System.Collections;
 +using System.Collections.Generic;
 +using System.Diagnostics;
 +using System.Runtime.InteropServices;
 +using System.Timers;
 +using UnityEngine;
 +using UnityEngine.Networking;
 +using Valve.VR.InteractionSystem;
 +
 +public class MoveObject : MonoBehaviour, IMovementController, IMovementType
 +{
 +	[SerializeField] private GameObject _endPosition;
 +
 +	public bool Moving { get; set; }
 +	public bool Done { get; set; }
 +
 +	public MovementController _controller;
 +	
 +	private void OnEnable()
 +	{
 +		_controller.setController(this);
 +	}
 +
 +	// Use this for initialization
 +	void Start () 
 +	{
 +		if (_endPosition == null)
 +		{
 +			_endPosition = new GameObject();
 +			_endPosition.transform.position = transform.position 	+ new Vector3(3,0,3);
 +		}
 +	}
 +
 +	//
 +
 +	
 +
 +	// Update is called once per frame
 +	void FixedUpdate ()
 +	{
 +		if (Input.GetButtonDown("Jump"))
 +		{
 +			print("Pressed Space");
 +			_controller.StartMoving();
 +		}
 +		_controller.CheckState(Moving,transform.position,_endPosition.transform.position, gameObject);
 +	}
 +
 +
 +	public void InitType()
 +	{
 +		this.transform.parent.GetComponent<NetworkTransform>().transformSyncMode = NetworkTransform.TransformSyncMode.SyncTransform;
 +	}
 +
 +	public void MoveGameObject(Vector3 origin, Vector3 endPosition, float speed, GameObject gameObj)
 +	{
 +		if (_endPosition == null)
 +		{
 +			_endPosition = new GameObject();
 +		}
 +		_endPosition.transform.position = endPosition;
 +		gameObj.transform.position = GetNewPos(origin, endPosition, speed);
 +	}
 +
 +	public Vector3 GetNewPos(Vector3 origin, Vector3 endPosition, float speed)
 +	{
 +		return Vector3.MoveTowards(origin, endPosition, speed);
 +	}
 +}
 +
 +public interface IMovementType
 +{
 +	void InitType();
 +	void MoveGameObject(Vector3 origin, Vector3 endPosition, float speed, GameObject gameObj);
 +}
 +
 +[System.Serializable]
 +public class MovementController
 +{
 +	private IMovementController _movementController;
 +
 +	[SerializeField] public float _speed;
 +
 +	public void SetSpeed(float val)
 +	{
 +		_speed = val;
 +	}
 +
 +	public float GetSpeed()
 +	{
 +		return _speed;
 +	}
 +
 +
 +	public void CheckState(bool moving, Vector3 origin, Vector3 endPosition, GameObject gameObj)
 +	{
 +		if (Vector3.Distance(origin,endPosition) > 0.01f && moving)
 +		{
 +			MoveGameObject(origin, endPosition, _speed,Time.deltaTime,gameObj);
 +		}
 +		else
 +		{
 +			_movementController.Moving = false;
 +		}
 +
 +		if (Vector3.Distance(origin, endPosition) < 0.01f)
 +		{
 +			_movementController.Done = true;
 +		}
 +	}
 +	
 +	public void setController(IMovementController moveObject)
 +	{
 +		_movementController = moveObject;
 +	}
 +	
 +	public void StartMoving()
 +	{
 +		MoveThisObject();
 +	}
 +	
 +	private void MoveThisObject()
 +	{
 +		_movementController.Moving = true;
 +	}
 +
 +
 +
 +	public void MoveGameObject(Vector3 origin, Vector3 endPosition, float speed, float deltaTime, GameObject gameObj)
 +	{
 +		_movementController.MoveGameObject(origin, endPosition, speed*deltaTime, gameObj);
 +	}
 +}
 +
 +public interface IMovementController
 +{
 +	bool Moving { get; set; }
 +	bool Done { get; set; }
 +	void MoveGameObject(Vector3 origin, Vector3 endPosition, float speed, GameObject gameObj);
 +	Vector3 GetNewPos(Vector3 origin, Vector3 endPosition, float speed);
 +}
