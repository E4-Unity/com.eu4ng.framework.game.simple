using System;
using System.Collections;
using UnityEngine;

namespace Eu4ng.Framework.Game.GameFlowSystem
{
	public enum StageState
	{
		Init,
		Ready,
		Start,
		End
	}
	public abstract class StageManager : MonoBehaviour
	{
		/* 필드 */
		StageState _stageState = StageState.Init;

		float _playTime;
		IEnumerator _stageTimer;

		/* 프로퍼티 */
		public StageState State => _stageState;
		public float PlayTime => _playTime;

		/* 이벤트 */
		public event Action OnStageReady;
		public event Action OnStageStart;
		public event Action OnStageEnd;

		/* MonoBehaviour */
		protected virtual void Awake()
		{
			_stageTimer = StageTimer();
		}

		/* API */
		public void ReadyStage()
		{
			if (_stageState is not (StageState.Init or StageState.End)) return;
			_stageState = StageState.Ready;

			_playTime = 0;

			OnStageReady?.Invoke();
			OnReadyStage();
		}

		public void StartStage()
		{
			if (_stageState != StageState.Ready) return;
			_stageState = StageState.Start;

			StartCoroutine(_stageTimer);

			OnStageStart?.Invoke();
			OnStartStage();
		}

		public void EndStage()
		{
			if (_stageState != StageState.Start) return;
			_stageState = StageState.End;

			StopCoroutine(_stageTimer);

			OnStageEnd?.Invoke();
			OnEndStage();
		}

		/* 메서드 */
		protected virtual void OnReadyStage(){}
		protected virtual void OnStartStage(){}
		protected virtual void OnEndStage(){}

		IEnumerator StageTimer()
		{
			while (true)
			{
				_playTime += Time.deltaTime;
				yield return null;
			}
		}
	}
}
