using Config;

using Utils.StateMachine;

namespace Scenes.Game.States
{
	public class GameState : State<GameStates, GameView>
	{
		public GameState()
		{
			StateEnter += GameStateBase_StateEnter;
			StateEntered += GameState_StateEntered;
			StateExit += GameStateBase_StateExit;
			StateExited += GameState_StateExited;
		}

		#region State Events

		private void GameStateBase_StateEnter(object sender, System.EventArgs e) => Entity.Publish(string.Format(EventTopics.GameStateEnter, Id.ToString()), this);

		private void GameState_StateEntered(object sender, System.EventArgs e) => Entity.Publish(string.Format(EventTopics.GameStateEntered, Id.ToString()), this);

		private void GameStateBase_StateExit(object sender, System.EventArgs e) => Entity.Publish(string.Format(EventTopics.GameStateExit, Id.ToString()), this);

		private void GameState_StateExited(object sender, System.EventArgs e) => Entity.Publish(string.Format(EventTopics.GameStateExited, Id.ToString()), this);

		#endregion

		#region Methods

		public void SetEnterTransitionAuto(bool val) => SkipEnterTransition = !val;

		public void SetExitTransitionAuto(bool val) => SkipExitTransition = !val;

		#endregion
	}
}
