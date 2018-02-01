using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Config;
using Config.Characters;

using Scenes.Game.Components;
using Scenes.Game.Components.Characters;
using Scenes.Game.Components.Objectives;
using Scenes.Game.States;

using UnityEngine;

using Utils.Scenes;
using Utils.StateMachine;

namespace Scenes.Game
{
	public class GameView : SceneComponent
	{
		public CharacterPrefabFactory CharacterFactory;

		public StateMachine<GameStates, GameView> States;

		private LevelComponent level;
		private ObjectiveManager GameObjectiveManager;
		private ObjectiveManager RoundObjectiveManager;
		private readonly List<PlayerData> players = new List<PlayerData>();

		public void ClickButton() => LoadNextScene();

		public void ClickPauseButton() => States.ChangeState(GameStates.Pause);

		protected override void Awake()
		{
			base.Awake();
			DebugSetup();
			InitGameStates();

			// Create players
			for (var i = 1; i <= 4; i++)
			{
				players.Add(new PlayerData("Player " + i, i, i == 1));
			}

			// First to score 5 -> win the match
			GameObjectiveManager = new ObjectiveManager();
			GameObjectiveManager.Add(new ObjectiveGameFinish(5));
			GameObjectiveManager.Completed += (sender, args) => States.ChangeState(GameStates.GameResult);
		}

		public IEnumerator Start()
		{
			LoadAdditiveScene("Level1");
			yield return this.WaitForMessage(EventTopics.SceneLoadAdditiveLoaded);

			level = FindObjectOfType<LevelComponent>();
			if (level == null)
			{
				throw new System.Exception($"{this} cant find a level component");
			}

			level.Setup(CharacterFactory);

			States.ChangeState(GameStates.Intro);
		}

		private void DebugSetup()
		{
			this.SubscribeWithTopic(EventTopics.SceneBase, (topic) => Debug.Log($"[Debug] Scene: {topic}"));
			this.SubscribeWithTopic(EventTopics.GamePlayerBase, (topic) => Debug.Log($"[Debug] Player: {topic}"));
			this.SubscribeWithTopic(EventTopics.GameCharacterBase, (topic) => Debug.Log($"[Debug] Character: {topic}"));
			this.SubscribeWithTopic(EventTopics.GameStateBase, (topic) => Debug.Log($"[Debug] Game States: {topic}"));
			this.SubscribeWithTopic(EventTopics.GameObjectiveBase, (topic) => Debug.Log($"[Debug] Objective: {topic}"));
		}

		private void InitGameStates()
		{
			// Create States
			States = new StateMachine<GameStates, GameView>(this);
			States.Add(GameStates.Loading, new GameState());
			States.Add(GameStates.Intro, new GameStateTransition(GameStates.RoundIntro));
			States.Add(GameStates.RoundIntro, new GameStateTransition(GameStates.Run));
			States.Add(GameStates.Run, new GameState());
			States.Add(GameStates.Pause, new GameStateTransition(GameStates.Run));
			States.Add(GameStates.RoundResult, new GameStateTransition(GameStates.RoundIntro));
			States.Add(GameStates.GameResult, new GameStateTransition(GameStates.Next));
			States.Add(GameStates.Next, new GameState());

			// Loading
			States.States[GameStates.Loading].StateEnter += (sender, arg) => LoadingBegin();
			States.States[GameStates.Loading].StateExit += (sender, arg) => LoadingEnd();

			// Time
			Time.timeScale = 0;
			States.States[GameStates.Run].StateEnter += (sender, arg) => Time.timeScale = 1;
			States.States[GameStates.Run].StateExit += (sender, arg) => Time.timeScale = 0;

			// Animated Transition
			(States.States[GameStates.Pause] as GameState)?.SetEnterTransitionAuto(false);
			(States.States[GameStates.Pause] as GameState)?.SetExitTransitionAuto(false);

			// New Round
			States.States[GameStates.RoundIntro].StateEnter += (sender, args) => InitNewRound();

			// Result Screen Finish
			States.States[GameStates.Next].StateEnter += (sender, args) => LoadNextScene();

			States.StateChanged += (sender, args) => this.Publish(EventTopics.GameStatesChanged, this);
			States.Start();
		}

		private void InitNewRound()
		{
			// Create new Round
			level.CleanRound();

			// Create round objectives (60s timeout or kill everyone)
			RoundObjectiveManager = new ObjectiveManager();
			RoundObjectiveManager.Add(new ObjectiveTimer(60));
			RoundObjectiveManager.Add(new ObjectiveRoundFinish());
			RoundObjectiveManager.Completed += FinishRound;
			RoundObjectiveManager.Failed += FinishRound;

			level.InitRound(players.ToList());
		}

		private void FinishRound(object sender, EventArgs args)
		{
			if (States.Current.Id != GameStates.Run)
			{
				return;
			}
			States.ChangeState(GameStates.RoundResult);
		}

		private void Update()
		{
			if (Time.timeScale <= 0f)
			{
				return;
			}
			GameObjectiveManager?.Update(Time.deltaTime);
			RoundObjectiveManager?.Update(Time.deltaTime);
		}
	}
}
