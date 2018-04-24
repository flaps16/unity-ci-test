using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.VR;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ConnectToServerSript : MonoBehaviour {
	public NetworkManager manager;
	private SteamVR_TrackedController _controller;


	private void OnEnable()
	{
		_controller = GetComponent<SteamVR_TrackedController>();
		_controller.TriggerClicked += HandleTriggerClicked;
	}
	
	private void OnDisable()
	{
		_controller.TriggerClicked -= HandleTriggerClicked;
	}
	
	private void HandleTriggerClicked(object sender, ClickedEventArgs e)
	{
		Connect();
	}

	// Use this for initialization
	void Start ()
	{
		manager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManager>();
	}

	private void Connect()
	{
		manager.StartClient();
	}
}
