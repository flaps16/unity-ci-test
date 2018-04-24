using System;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Diagnostics;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;

//using NSubstitute;

public class NetworkTests
{

	
	
	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator TestConnection()
	{
		var Player = GetPlayer("PlayerXYZ");
		var NetworkMan = GetNetworkMan();
		NetworkManager netMan= NetworkMan.GetComponent<NetworkManager>();
		NetworkClient nc = netMan.StartClient();

		for (int i = 0; i < 50; i++)
		{
			yield return null;
		}
		
		Assert.That(nc.isConnected, Is.EqualTo(true));
	}

	[UnityTest]
	public IEnumerator TestMovementOnNetwork()
	{
		var Player = GetPlayer("PlayerXYZ");
		Player.name = "Player(Local)";
		var NetworkMan = GetNetworkMan();
		NetworkManager netMan= NetworkMan.GetComponent<NetworkManager>();
		netMan.StartClient();
		Vector3 origin = new Vector3(0,0,0);
		Player.transform.position = origin;
		Vector3 end = new Vector3(3,0,3);
		var moveGObj = GetMovementObj();
		moveGObj.GetComponent<MoveObject>().enabled = true;
		MovementController movementController = moveGObj.GetComponent<MoveObject>()._controller;
		MoveObject moveObj = moveGObj.GetComponent<MoveObject>();
		moveObj.enabled = true;

		while (moveObj.Done == false)
		{
			yield return FixedUpd(movementController, origin, end, 0.5f, Player);
		}
		
		Assert.That(origin, !Is.EqualTo(Player.gameObject.transform.position));
	}
	
	[UnityTest]
	public IEnumerator TestMovementOnNetworkIsOnlyOnLocalPlayer()
	{
		var Player = GetPlayer("PlayerXYZ");
		Player.name = "Player(Local)";
		var NetworkMan = GetNetworkMan();
		NetworkManager netMan= NetworkMan.GetComponent<NetworkManager>();
		netMan.StartClient();
		Vector3 origin = new Vector3(0,0,0);
		Player.transform.position = origin;
		Vector3 end = new Vector3(3,0,3);
		var moveGObj = GetMovementObj();
		moveGObj.GetComponent<MoveObject>().enabled = true;
		MovementController movementController = moveGObj.GetComponent<MoveObject>()._controller;
		MoveObject moveObj = moveGObj.GetComponent<MoveObject>();
		moveObj.enabled = true;
		for (int i = 0; i < 100; i++)
		{
			yield return null;
		}
		GameObject remotePlayer = GameObject.Find("PlayerXYZ(Clone)");
		Assert.That(remotePlayer, Is.Not.Null);
		Transform remoteStartPos = remotePlayer.transform;
		
		while (!moveObj.Done == true)
		{
			yield return FixedUpd(movementController, origin, end, 0.5f, Player);
		}
		Transform remoteEndPos = remotePlayer.transform;
		Assert.That(remoteStartPos, Is.EqualTo(remoteEndPos));
	}


	

	[UnityTest]
	public IEnumerator TestMovementOnNetworkRigidBody()
	{
		var Player = GetPlayer("PlayerXYZ");
		yield return null;
		Player.name = "Player(Local)";
		var NetworkMan = GetNetworkMan();
		NetworkManager netMan= NetworkMan.GetComponent<NetworkManager>();
		netMan.StartClient();
		Vector3 origin = new Vector3(0,0,0);
		Player.transform.position = origin;
		Vector3 end = new Vector3(3,0,3);
		var moveGObj = GetMovementObj();
		moveGObj.GetComponent<MoveObject>().enabled = true;
		MovementController movementController = moveGObj.GetComponent<MoveObject>()._controller;
		MoveObject3DRigid moveObj = moveGObj.GetComponent<MoveObject3DRigid>();
		moveObj._controller = movementController;
		moveGObj.GetComponent<MoveObject>().enabled = false;
		moveObj.enabled = true;
		

		while (!moveObj.Done == true)
		{
			yield return FixedUpd(movementController, origin, end, 0.5f, Player);
		}
		Assert.That(origin, Is.Not.EqualTo(Player.gameObject.transform.position));
	}

	[UnityTest]
	public IEnumerator TestMovementOnNetworkCharacterController()
	{
		yield return null;
		Assert.True(false);	
	}
	[UnityTest]
	public IEnumerator TestMovementOnNetworkTransform()
	{
		yield return null;
		Assert.True(false);
	}

	[UnityTest]
	public IEnumerator TestMovementOnNetworkRpc()
	{
		var Player = GetPlayer("PlayerXYZ");
		Player.name = "Player(Local)";
		var NetworkMan = GetNetworkMan();
		NetworkManager netMan= NetworkMan.GetComponent<NetworkManager>();
		netMan.StartClient();
		Vector3 origin = new Vector3(0,0,0);
		Player.transform.position = origin;
		Vector3 end = new Vector3(3,0,3);
		var moveGObj = GetMovementObj();
		moveGObj.GetComponent<MoveObject>().enabled = true;
		MovementController movementController = moveGObj.GetComponent<MoveObject>()._controller;
		MoveObjectRPC moveObj = moveGObj.GetComponent<MoveObjectRPC>();
		Assert.That(moveObj._controller, Is.Not.Null);
		moveObj._controller = movementController;
		moveGObj.GetComponent<MoveObject>().enabled = false;
		moveObj.enabled = true;
		

		Stopwatch sw = new Stopwatch();
		sw.Start();
		while (!moveObj.Done == true)
		{
			yield return FixedUpd(movementController, origin, end, 0.5f, Player);
		}
		sw.Stop();
		Debug.Log(sw.Elapsed);
		Assert.That(origin, Is.Not.EqualTo(Player.gameObject.transform.position));
	}

	private IEnumerator FixedUpd(MovementController moveObj, Vector3 origin, Vector3 end,float speed, GameObject Player)
	{
		moveObj.MoveGameObject(origin, end, speed, 0.01f, Player);
		yield return null;
	}

	private GameObject GetPlayer(String playerToLoad)
	{
		return GameObject.Instantiate(Resources.Load(playerToLoad, typeof(GameObject))) as GameObject;
	}

	private GameObject GetNetworkMan()
	{	
		return GameObject.Instantiate(Resources.Load("NetworkMan", typeof(GameObject))) as GameObject;
	}
	
	private GameObject GetMovementObj()
	{
		return GameObject.Find("Player(Local)/Hmd");
	}
	
}
