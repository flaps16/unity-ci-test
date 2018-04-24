using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Xml.Serialization;
using NSubstitute;
using NSubstitute.Core;
using NSubstitute.Core.Arguments;

public class UnitTest {

	[Test]
	public void MoveGameObjectCallGoesThrough()
	{
		
		Vector3 origin = new Vector3(6,0,6);
		Vector3 end = new Vector3(3,0,3);
		var startDistance = Vector3.Distance(origin,end);
		float speed = -0.5f;
		IMovementController movementController = (IMovementController)GetMovementControllerMock();
		var moveObject = GetMovementController(movementController);
		GameObject go = new GameObject();
		moveObject.MoveGameObject(origin,end,speed,0.01f,go);
		Debug.Log(movementController.GetNewPos(end,origin,speed));

		movementController.Received().MoveGameObject(origin,end,speed*0.01f,go);
	}

	private MovementController GetMovementController(IMovementController movementController)
	{
		var controller = Substitute.For<MovementController>();
		controller.setController(movementController);
		return controller;
	}

	private object GetMovementControllerMock()
	{
		return Substitute.For<IMovementController>();
	}

	[Test]
	public void UnitTestSimplePasses() {
		// Use the Assert class to test conditions.
	}

	[Datapoint]
	public float zero = 0;
	[Datapoint]
	public float positive = 1;
	[Datapoint]
	public float negative = -1;


	[Theory]
	public void TestMovement(float num)
	{
		Assume.That(num> 0.000 || num < 0.000);
		GameObject go = new GameObject();
		Vector3 origin = new Vector3(0,0,0);
		go.transform.position = origin;
		Vector3 end = new Vector3(3,0,3);
		var startDistance = Vector3.Distance(go.transform.position,end);
		IMovementController movementController = (IMovementController)GetMovementControllerMock();

		movementController.GetNewPos(origin, end, num).ReturnsForAnyArgs(origin + (num / 10 * end));
		go.transform.position = movementController.GetNewPos(origin, end, num);
		var endDistance = Vector3.Distance(go.transform.position, end);
		
		Assert.That(startDistance, !Is.EqualTo(endDistance).Within(0.0001f));
	}

	[Theory]
	public void NegativeMovement(float num)
	{
		Assume.That(num < 0.0 && num > float.MinValue);
		Vector3 origin = new Vector3(0,0,0);
		Vector3 end = new Vector3(3,3,3);
		GameObject go = new GameObject();
		go.transform.position = origin;
		var startDistance = Vector3.Distance(go.transform.position,end);
		
		IMovementController movementController = (IMovementController)GetMovementControllerMock();		MovementController moveObject = GetMovementController(movementController);
		movementController.GetNewPos(origin,end,num).ReturnsForAnyArgs(origin + (num / 10 * end));
		
		Vector3 newPos = movementController.GetNewPos(origin,end,num);

		go.transform.position = newPos;
		var endDistance = Vector3.Distance(go.transform.position, end);
		
		Assert.That(endDistance, !Is.LessThan(startDistance));

	}


	
}



