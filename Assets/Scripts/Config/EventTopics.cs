namespace Config {
	public static class EventTopics
	{
		// Scene
		public const string SceneBase = "scene.*";
		public const string SceneAwake = "scene.awake";
		public const string SceneChange = "scene.change";

		// Scene : Additive
		public const string SceneLoadAdditive = "scene.additive.loading";
		public const string SceneLoadAdditiveAwake = "scene.additive.awake";
		public const string SceneLoadAdditiveLoaded = "scene.additive.loaded";

		// Scene : Loading Screen
		public const string SceneLoading = "scene.loading";
		public const string SceneLoaded = "scene.loaded";

		// Game

		// States
		public const string GameStateBase = "game.states.*";
		public const string GameStatesChanged = "game.states.changed";
		public const string GameStateEnter = "game.states.{0}.enter";
		public const string GameStateEntered = "game.states.{0}.entered";
		public const string GameStateExit = "game.states.{0}.exit";
		public const string GameStateExited = "game.states.{0}.exited";

		// Player
		public const string GamePlayerBase = "game.player.*";
		public const string GamePlayerCreated = "game.player.created";
		public const string GamePlayerUpdated = "game.player.updated";

		// Characters
		public const string GameCharacterBase = "game.character.*";
		public const string GameCharacterSpawn = "game.character.spawn";
		public const string GameCharacterDeath = "game.character.death";

		// Objectives
		public const string GameObjectiveBase = "game.objective.*";
		public const string GameObjectiveCreated = "game.objective.created";
		public const string GameObjectiveDestroyed = "game.objective.destroyed";
		public const string GameObjectiveUpdated = "game.objective.updated";
		public const string GameObjectiveComplete = "game.objective.complete";
		public const string GameObjectiveFail = "game.objective.fail";
	}
}
