using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
	public void StartGame() =>
		OwnSceneManager.Load(OwnSceneManager.Scenes.GameScene);

	public void Exit() =>
		Application.Quit();
}
