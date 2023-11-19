using System;
using System.ComponentModel;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using Random = UnityEngine.Random;

internal class AttributeSystemTests
{
    [Test]
    public void AttributeSetTests()
    {
	    bool isHealthUpdated = false;
	    bool isMaxHealthUpdated = false;
	    bool isDiedCalled = false;

	    var testObject = new GameObject();
	    var attribute = testObject.AddComponent<TestAttributeSet>();
	    attribute.PropertyChanged += OnPropertyChanged_Event;

	    attribute.OnDied += delegate(GameObject target)
	    {
		    isDiedCalled = true;
		    Assert.AreEqual(testObject, target, "OnDied 이벤트를 호출한 대상과 전달된 대상이 다릅니다.");
		    Assert.True(attribute.IsDead, "IsDead 가 true일 때만 OnDied 이벤트가 호출됩니다.");
	    };

	    attribute.Health = float.MinValue;
	    attribute.Health = float.MaxValue;
	    attribute.MaxHealth = float.MinValue;
	    attribute.MaxHealth = float.MaxValue;

	    for (int i = 0; i < 10000; i++)
	    {
		    var testValue = Random.Range(0, attribute.MaxHealth);
		    attribute.Health = testValue;
		    attribute.MaxHealth = testValue;
	    }

	    Assert.True(isHealthUpdated, "HealthUpdated 이벤트가 단 한 번도 호출되지 않았습니다.");
	    Assert.True(isMaxHealthUpdated, "MaxHealthUpdated 이벤트가 단 한 번도 호출되지 않았습니다.");
	    Assert.True(isDiedCalled, "IsDied 이벤트가 단 한 번도 호출되지 않았습니다.");

	    // Local 함수
	    void OnPropertyChanged_Event(object sender, PropertyChangedEventArgs e)
	    {
		    switch (e.PropertyName)
		    {
			    case "Health":
				    if (GetProperty<float>(out var health))
				    {
					    isHealthUpdated = true;
					    Assert.AreEqual(health, attribute.Health, "이벤트로 전달받은 값이 Health 값과 다릅니다.");

					    if(health < 0 || health > attribute.MaxHealth)
						    Assert.Fail("Health 값이 범위를 벗어났습니다.");

					    if (Mathf.Approximately(health, 0))
						    Assert.True(attribute.IsDead, "Health 값이 0이면 IsDead = true여야 합니다");
					    else
						    Assert.False(attribute.IsDead, "Health 값이 0이 아니라면 IsDead = false여야 합니다");
				    }
				    break;
			    case "MaxHealth":
				    if (GetProperty<float>(out var maxHealth))
				    {
					    isMaxHealthUpdated = true;
					    Assert.AreEqual(maxHealth, attribute.MaxHealth, "이벤트로 전달받은 값이 MaxHealth 값과 다릅니다.");

					    if(maxHealth < 0)
						    Assert.Fail("MaxHealth 값이 범위를 벗어났습니다.");
				    }
				    break;
		    }

		    bool GetProperty<T>(out T value) where T : new()
		    {
			    value = new T();

			    // Property Info 추출
			    var propertyInfo = sender.GetType().GetProperty(
				    e.PropertyName,
				    BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
				    null,
				    typeof(T),
				    Type.EmptyTypes,
				    null
			    );

			    if (propertyInfo is null) return false;

			    value = (T)propertyInfo.GetValue(sender);
			    return true;
		    }
	    }
    }
}
