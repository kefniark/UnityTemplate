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
    public const string GameCharacterSpawn = "game.character.spawn";
}