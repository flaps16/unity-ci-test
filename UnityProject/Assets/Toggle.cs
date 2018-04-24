using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

[System.Serializable]
public class ToggleEvent : UnityEvent<bool>{}

public class Toggle : NetworkBehaviour {


	[SerializeField] ToggleEvent onToggleLocal;
	[SerializeField] ToggleEvent onToggleRemote;

	private void Start()
	{
		EnablePlayer ();
	}

	void DisablePlayer()
	{
		if (isLocalPlayer)
			onToggleLocal.Invoke (false);
		else
			onToggleRemote.Invoke (false);
	}

	void EnablePlayer()
	{
		if (isLocalPlayer)
		{
			onToggleLocal.Invoke(true);
			gameObject.name = "Player(Local)";
		}

		else
		{
			onToggleRemote.Invoke(true);
			gameObject.name = "Player(Remote)";
		}
	}
}
