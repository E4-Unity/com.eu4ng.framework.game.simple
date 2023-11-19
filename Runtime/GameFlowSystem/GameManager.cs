using System;
using System.Collections;
using UnityEngine;

namespace Eu4ng.Framework.Game.GameFlowSystem
{
	public enum GameState
	{
		Init,
		Enter,
		Start,
		End,
		Exit
	}

	public abstract class GameManager : MonoBehaviour
	{
		/* 필드 */
		bool _isPaused;
		float _originalTimeScale;
		GameState _gameState = GameState.Init;

		float _playTime;
		IEnumerator _gameTimer;

		/* 프로퍼티 */
		public bool IsPaused => _isPaused;
		public GameState State => _gameState;

		/* 이벤트 */
		public event Action OnGameEnter;
		public event Action OnGameStart;
		public event Action OnGameEnd;
		public event Action OnGameExit;

		public event Action OnGamePause;
		public event Action OnGameResume;

		/* MonoBehaviour */
		protected virtual void Awake()
		{
			_gameTimer = GameTimer();
		}

		/* API */
		public void EnterGame()
		{
			if (_gameState != GameState.Init) return;
			_gameState = GameState.Enter;

			_playTime = 0;

			OnGameEnter?.Invoke();
			OnEnterGame();
		}
		public void StartGame()
		{
			if (_gameState != GameState.Enter) return;
			_gameState = GameState.Start;

			StartCoroutine(_gameTimer);

			OnGameStart?.Invoke();
			OnStartGame();
		}

		public void EndGame()
		{
			if (_gameState != GameState.Start) return;
			_gameState = GameState.End;

			StopCoroutine(_gameTimer);

			OnGameEnd?.Invoke();
			OnEndGame();
		}

		public void ExitGame()
		{
			if (_gameState != GameState.End) return;
			_gameState = GameState.Exit;

			OnGameExit?.Invoke();
			OnExitGame();
		}

		public void PauseGame()
		{
			_isPaused = true;
			OnGamePause?.Invoke();

			_originalTimeScale = Time.timeScale;
			Time.timeScale = 0f;

			OnPauseGame();
		}

		public void ResumeGame()
		{
			_isPaused = false;
			OnGameResume?.Invoke();

			Time.timeScale = _originalTimeScale;

			OnResumeGame();
		}

		/* 메서드 */
		protected virtual void OnEnterGame(){}
		protected virtual void OnStartGame(){}
		protected virtual void OnEndGame(){}
		protected virtual void OnExitGame(){}

		protected virtual void OnPauseGame(){}
		protected virtual void OnResumeGame(){}

		IEnumerator GameTimer()
		{
			while (true)
			{
				_playTime += Time.deltaTime;
				yield return null;
			}
		}
	}
}
