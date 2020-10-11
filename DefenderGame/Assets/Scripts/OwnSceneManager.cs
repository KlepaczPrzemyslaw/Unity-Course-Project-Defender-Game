using UnityEngine.SceneManagement;

public class OwnSceneManager
{
	public enum Scenes
	{
		GameScene,
		MainMenuScene
	}

	public static void Load(Scenes scene) =>
		SceneManager.LoadScene((int)scene);
}
