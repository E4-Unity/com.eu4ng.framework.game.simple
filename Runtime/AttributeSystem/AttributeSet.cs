using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Eu4ng.Framework.Game.AttributeSystem
{
	public abstract class AttributeSet : MonoBehaviour, INotifyPropertyChanged
	{
		/* 필드, 프로퍼티 */
		// Health
		Attribute _health = new Attribute(100f);

		public float Health
		{
			get => _health.Value;
			set
			{
				if (!SetAttribute(_health, value)) return;

				if(IsDead) OnDied?.Invoke(gameObject);
			}
		}

		public float MaxHealth
		{
			get => _health.MaxValue;
			set => SetMaxAttribute(_health, value);
		}

		// Dead
		public bool IsDead => Mathf.Approximately(_health.Value, 0);

		/* 이벤트 */
		public event Action<GameObject> OnDied;

		/* MonoBehaviour */
		protected virtual void OnEnable()
		{
			// Max 값으로 초기화
			_health.Value = _health.MaxValue;
		}

		/* INotifyPropertyChanged */
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected bool SetAttribute(Attribute field, float value, [CallerMemberName] string propertyName = null)
		{
			// 동일한 값은 무시
			if (Mathf.Approximately(field.Value, value)) return false;

			// 새로운 값 대입
			field.Value = value;

			// 값이 변했는지 확인
			if (!Mathf.Approximately(field.Value, value)) return false;

			// 값이 변한 경우에만 이벤트 호출
			OnPropertyChanged(propertyName);
			return true;
		}

		protected bool SetMaxAttribute(Attribute field, float value, [CallerMemberName] string propertyName = null)
		{
			// 동일한 값은 무시
			if (Mathf.Approximately(field.MaxValue, value)) return false;

			// 새로운 값 대입
			field.MaxValue = value;

			// 값이 변했는지 확인
			if (!Mathf.Approximately(field.MaxValue, value)) return false;

			// 값이 변한 경우에만 이벤트 호출
			OnPropertyChanged(propertyName);
			return true;
		}
	}
}
