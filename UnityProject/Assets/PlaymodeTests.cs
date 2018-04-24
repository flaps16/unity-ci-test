using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
//using NSubstitute;

public class PlaymodeTests
{

	
	
	[Test]
	public void NewPlayModeTestSimplePasses() {
		// Use the Assert class to test conditions.
	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator TestMovement() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		Vector3 origin = new Vector3(0,0,0);
		GameObject go = new GameObject();
		go.transform.position = origin;
		Vector3 end = new Vector3(3,0,3);
		IMovementController movementController = (IMovementController)GetMovementControllerMock();
		var moveObj = GetMovementController(movementController);
		
		moveObj.MoveGameObject(origin,end,0.5f,0.01f,go);
		yield return null;
		Assert.That(origin, Is.Not.EqualTo(go.gameObject.transform.position));
	}
	
	[UnityTest]
	public IEnumerator TestChangingMovingBoolToFalseMidMovementOriginNotEqualToCurrent() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		Vector3 end;
		GameObject go;
		IMovementController movementController;
		MovementController moveObj;
		var origin = SetupChangeMovingBool(out end, out go, out movementController,out moveObj);
		moveObj.MoveGameObject(go.transform.position, end, 1f, 0.01f, go);
		yield return null;
		moveObj.MoveGameObject(go.transform.position, end, 1f, 0.01f, go);
		yield return null;
		moveObj.MoveGameObject(go.transform.position, end, 1f, 0.01f, go);
		yield return null;
		Vector3 stopPos;
		ChangeMovingBoolForTest(movementController, go, out stopPos);
		Assert.That(origin, Is.Not.EqualTo(go.gameObject.transform.position));

	}

	

	[UnityTest]
	public IEnumerator TestChangingMovingBoolToFalseMidMovementOriginNotEqualToStopPosition() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		Vector3 end;
		GameObject go;
		IMovementController movementController;
		MovementController moveObj;
		var origin = SetupChangeMovingBool(out end, out go, out movementController,out moveObj);
		moveObj.MoveGameObject(go.transform.position, end, 1f, 0.01f, go);
		yield return null;
		moveObj.MoveGameObject(go.transform.position, end, 1f, 0.01f, go);
		yield return null;
		moveObj.MoveGameObject(go.transform.position, end, 1f, 0.01f, go);
		yield return null;
		Vector3 stopPos;
		ChangeMovingBoolForTest(movementController, go, out stopPos);
		Assert.That(origin, Is.Not.EqualTo(stopPos));

	}
	[UnityTest]
	public IEnumerator TestChangingMovingBoolToFalseMidMovementStopPosIsEqualToCurrentPosWithinReason() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		Vector3 end;
		GameObject go;
		IMovementController movementController;
		MovementController moveObj;
		var origin = SetupChangeMovingBool(out end, out go, out movementController,out moveObj);
		moveObj.MoveGameObject(go.transform.position, end, 1f, 0.01f, go);
		yield return null;
		moveObj.MoveGameObject(go.transform.position, end, 1f, 0.01f, go);
		yield return null;
		moveObj.MoveGameObject(go.transform.position, end, 1f, 0.01f, go);
		yield return null;
		Vector3 stopPos;
		ChangeMovingBoolForTest(movementController, go, out stopPos);
		Assert.That(stopPos,Is.EqualTo(go.transform.position).Within(0.001f));

	}
	[UnityTest]
	public IEnumerator TestChangingMovingBoolToFalseMidMovementMovingBooleanIsFalse() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		Vector3 end;
		GameObject go;
		IMovementController movementController;
		MovementController moveObj;
		var origin = SetupChangeMovingBool(out end, out go, out movementController,out moveObj);
		moveObj.MoveGameObject(go.transform.position, end, 1f, 0.01f, go);
		yield return null;
		moveObj.MoveGameObject(go.transform.position, end, 1f, 0.01f, go);
		yield return null;
		moveObj.MoveGameObject(go.transform.position, end, 1f, 0.01f, go);
		yield return null;
		Vector3 stopPos;
		ChangeMovingBoolForTest(movementController, go, out stopPos);
		Assert.That(movementController.Moving, Is.False);

	}
	[UnityTest]
	public IEnumerator TestChangingMovingBoolToFalseMidMovementEndIsNotEqualToStopPos() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		Vector3 end;
		GameObject go;
		IMovementController movementController;
		MovementController moveObj;
		var origin = SetupChangeMovingBool(out end, out go, out movementController,out moveObj);
		moveObj.MoveGameObject(go.transform.position, end, 1f, 0.01f, go);
		yield return null;
		moveObj.MoveGameObject(go.transform.position, end, 1f, 0.01f, go);
		yield return null;
		moveObj.MoveGameObject(go.transform.position, end, 1f, 0.01f, go);
		yield return null;
		Vector3 stopPos;
		ChangeMovingBoolForTest(movementController, go, out stopPos);
		Assert.That(end, Is.Not.EqualTo(stopPos));

	}
	
	[UnityTest]
	public IEnumerator TestChangeEndTransformMidMovementToNegativeEndNewEndIsFurther() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		Vector3 end;
		GameObject go;
		IMovementController movementController;
		MovementController moveObj;
		var origin = SetupChangeMovingBool(out end, out go, out movementController,out moveObj);
		moveObj.MoveGameObject(go.transform.position, end, 1f, 0.01f, go);
		yield return null;
		moveObj.MoveGameObject(go.transform.position, end, 1f, 0.01f, go);
		yield return null;
		moveObj.MoveGameObject(go.transform.position, end, 1f, 0.01f, go);
		yield return null;
		var DistToOrig = Vector3.Distance(go.transform.position, end);
		end = -end;
		var DistToNew = Vector3.Distance(go.transform.position, end);
		Assert.That(DistToOrig, Is.LessThan(DistToNew));
	}
	
	[UnityTest]
	public IEnumerator TestChangeEndTransformMidMovementMoreToNewEndNewEndCloserThanOldEnd() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		Vector3 end;
		GameObject go;
		IMovementController movementController;
		MovementController moveObj;
		var origin = SetupChangeMovingBool(out end, out go, out movementController,out moveObj);

		for (int i = 0; i < 3; i++)
		{
			moveObj.MoveGameObject(go.transform.position, end, 10f, 0.01f, go);
			yield return null;
		}
		
		end = -end;
		var DistToNew = Vector3.Distance(go.transform.position, end);

		for (int i = 0; i < 20; i++)
		{
			moveObj.MoveGameObject(go.transform.position, end, 10f, 0.01f, go);
			yield return null;
		}
		var NewDistToNew = Vector3.Distance(go.transform.position, end);
		Assert.That(NewDistToNew , Is.LessThan(DistToNew));
	}
	
	[UnityTest]
	public IEnumerator TestChangeOriginTransformMidMovementCompareDistanceDifferentt() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		Vector3 end;
		GameObject go;
		IMovementController movementController;
		MovementController moveObj;
		var origin = SetupChangeMovingBool(out end, out go, out movementController,out moveObj);
		var OriginalDirection = end - go.transform.position;
		for (int i = 0; i < 3; i++)
		{
			moveObj.MoveGameObject(go.transform.position, end, 10f, 0.01f, go);
			yield return null;
		}
		
		origin = new Vector3(6,0,3);
		var DistToNew = Vector3.Distance(go.transform.position, end);

		moveObj.MoveGameObject(origin, end, 100f, 0.01f, go);
		yield return null;
		var NewDirection = end - origin;
		var NewDistToNew = Vector3.Distance(go.transform.position, end);
		Assert.That(DistToNew , Is.Not.EqualTo(NewDistToNew));
	}
	
	[UnityTest]
	public IEnumerator TestChangeOriginTransformMidMovementCompareDirectionDifferent() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		Vector3 end;
		GameObject go;
		IMovementController movementController;
		MovementController moveObj;
		var origin = SetupChangeMovingBool(out end, out go, out movementController,out moveObj);
		for (int i = 0; i < 3; i++)
		{
			movementController.GetNewPos(go.transform.position, end, 10);
			moveObj.MoveGameObject(go.transform.position, end, 10f, 0.01f, go);
			yield return null;
		}
		
		origin = new Vector3(6,0,3);
		var OrginalDirection = movementController.GetNewPos(go.transform.position,end,0.1f);

		moveObj.MoveGameObject(origin, end, 100f, 0.01f, go);
		yield return null;
		var NewDirection = movementController.GetNewPos(origin, end, 1);
		Assert.That(OrginalDirection, Is.Not.EqualTo(NewDirection));
	}
	
	private MovementController GetMovementController(IMovementController movementController)
	{
		var controller = new MovementController();
		controller.setController(movementController);
		return controller;
	}

	private object GetMovementControllerMock()
	{
		return new MoveObject();
	}
	
	private static void ChangeMovingBoolForTest(IMovementController movementController, GameObject go, out Vector3 stopPos)
	{
		movementController.Moving = false;
		stopPos = go.transform.position;
	}

	private Vector3 SetupChangeMovingBool(out Vector3 end, out GameObject go, out IMovementController movementController)
	{
		Vector3 origin = new Vector3(0, 0, 0);
		go = new GameObject();
		go.transform.position = origin;
		end = new Vector3(3, 0, 3);
		movementController = (IMovementController) GetMovementControllerMock();
		var moveObj = GetMovementController(movementController);

		return origin;
	}
	private Vector3 SetupChangeMovingBool(out Vector3 end, out GameObject go, out IMovementController movementController, out MovementController moveObj)
	{
		Vector3 origin = new Vector3(0, 0, 0);
		go = new GameObject();
		go.transform.position = origin;
		end = new Vector3(3, 0, 3);
		movementController = (IMovementController) GetMovementControllerMock();
		moveObj = GetMovementController(movementController);

		return origin;
	}
}
