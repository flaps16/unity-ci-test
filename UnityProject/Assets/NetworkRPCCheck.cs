using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkRPCCheck : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool isThisObejctServer()
	{
		return isServer;
	}
	
	[ClientRpc]
	public void RpcMoveObj(GameObject gameObj, Vector3 newPos)
	{
		gameObj.transform.position = newPos;
	}
}
