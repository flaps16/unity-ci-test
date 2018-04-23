using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class EditModeTests {

    [Test]
    public void EditModeTestsSimplePasses()
    {
        // Use the Assert class to test conditions.
    }
    [Test]
    public void EditModeTestsSimplePassesFail()
    {
        Assert.True(false);
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
	public IEnumerator EditModeTestsWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}
}
