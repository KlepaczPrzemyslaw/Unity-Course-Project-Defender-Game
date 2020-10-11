using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
	public static GameOverUI Instance { get; protected set; }

	[SerializeField]
	private Button retry = null;

	[SerializeField]
	private Button exit = null;

	[SerializeField]
	private TextMeshProUGUI summaryText = null;

	void Awake()
	{
		Instance = this;
		Hide();

		retry.onClick.AddListener(RetryGame);
		exit.onClick.AddListener(ExitGame);
	}

	void OnDestroy()
	{
		retry.onClick.RemoveAllListeners();
		exit.onClick.RemoveAllListeners();
	}

	public void Show()
	{
		summaryText.SetText(
			$"You survived {EnemyWaveManager.Instance.GetWaveNumber()} Waves!");
		gameObject.SetActive(true);
	}

	private void RetryGame() => OwnSceneManager.Load(OwnSceneManager.Scenes.GameScene);

	private void ExitGame() => OwnSceneManager.Load(OwnSceneManager.Scenes.MainMenuScene);

	private void Hide() => gameObject.SetActive(false);
}
