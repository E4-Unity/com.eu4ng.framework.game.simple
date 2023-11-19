using System;
using UnityEngine;

namespace Eu4ng.Framework.Game.AttributeSystem
{
	[Serializable]
	public class Attribute
	{
		float _value;
		float _maxValue = float.MaxValue;

		public float Value
		{
			get => _value;
			set => _value = Mathf.Clamp(value, 0, _maxValue);
		}

		public float MaxValue
		{
			get => _maxValue;
			set
			{
				_maxValue = Mathf.Max(value, 0);
				Value = Mathf.Min(Value, _maxValue);
			}
		}

		public Attribute(){}

		public Attribute(float maxValue)
		{
			_maxValue = maxValue;
		}
	}
}
