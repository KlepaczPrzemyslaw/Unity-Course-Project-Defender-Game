using UnityEngine.SceneManagement;

public class OwnSceneManager
{
	public enum Scenes
	{
		MainMenuScene,
		GameScene
	}

	public static void Load(Scenes scene) =>
		SceneManager.LoadScene((int)scene);
}
