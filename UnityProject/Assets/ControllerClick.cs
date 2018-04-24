using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ControllerClick : MonoBehaviour
{
	private SteamVR_TrackedController _controller;
	public GameObject[] _interactibles;
	
	private Interactable _hoveringInteractable;
	private bool _hasAttachment = false;

	private void OnEnable()
	{
		_controller = GetComponent<SteamVR_TrackedController>();
		_controller.TriggerClicked += HandleTriggerClicked;
		_interactibles = GameObject.FindGameObjectsWithTag("Interactible");
	}

	private void OnDisable()
	{
		_controller.TriggerClicked -= HandleTriggerClicked;
	}

	
	private void HandleTriggerClicked(object sender, ClickedEventArgs e)
	{
		foreach (var interactible in _interactibles)
		{
			print("Trigger clicked");
			if (!(Vector3.Distance(gameObject.transform.position, interactible.transform.position) < .5f)) continue;
			print("Attaching/Detatching interactible");
			Attach(interactible);
		}
	}

	private void Update()
	{
		
		
	}

	private void Attach(GameObject interactible)
	{
		if (!_hasAttachment)
		{
			print("Attaching interactible");
			interactible.transform.SetParent(gameObject.transform, true);
			_hasAttachment = true;
		}
		else
		{
			print("Detaching interactible");
			interactible.transform.parent = null;
			_hasAttachment = false;
		}
	}


	/*public Interactable hoveringInteractable
	{
		get
		{
			print("HOVERING");
			return _hoveringInteractable;
		}
		set
		{
			if ( _hoveringInteractable != value )
			{
				if ( _hoveringInteractable != null )
				{
					_hoveringInteractable.SendMessage( "OnHandHoverEnd", this, SendMessageOptions.DontRequireReceiver );

					//Note: The _hoveringInteractable can change after sending the OnHandHoverEnd message so we need to check it again before broadcasting this message
					if ( _hoveringInteractable != null )
					{
						this.BroadcastMessage( "OnParentHandHoverEnd", _hoveringInteractable, SendMessageOptions.DontRequireReceiver ); // let objects attached to the hand know that a hover has ended
					}
				}

				_hoveringInteractable = value;

				if ( _hoveringInteractable != null )
				{
					_hoveringInteractable.SendMessage( "OnHandHoverBegin", this, SendMessageOptions.DontRequireReceiver );

					//Note: The _hoveringInteractable can change after sending the OnHandHoverBegin message so we need to check it again before broadcasting this message
					if ( _hoveringInteractable != null )
					{
						this.BroadcastMessage( "OnParentHandHoverBegin", _hoveringInteractable, SendMessageOptions.DontRequireReceiver ); // let objects attached to the hand know that a hover has begun
					}
				}
			}
		}
	}*/
}
