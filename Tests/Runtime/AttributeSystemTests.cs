using NUnit.Framework;
using UnityEngine;

internal class AttributeSystemTests
{
    [Test]
    public void AttributeSetTests()
    {
	    var testObject = new GameObject();
	    var attribute = testObject.AddComponent<TestAttributeSet>();

	    Assert.AreEqual(attribute.Health, attribute.MaxHealth, "Health 값은 Max Health 값으로 초기화되어야 합니다");
    }
}
