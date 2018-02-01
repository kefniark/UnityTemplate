namespace Scenes.Game.States
{
	public class GameStateTransition : GameState
	{
		private readonly GameStates next;

		public GameStateTransition(GameStates nextState)
		{
			next = nextState;
		}

		public void Finish() => StateMachine.ChangeState(next);
	}
}
