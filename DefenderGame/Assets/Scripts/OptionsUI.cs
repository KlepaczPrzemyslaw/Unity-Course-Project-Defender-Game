using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
	[Header("Sound")]
	[SerializeField]
	private Button moreSoundBtn = null;

	[SerializeField]
	private Button lessSoundBtn = null;

	[SerializeField]
	private TextMeshProUGUI soundText = null;

	[Header("Music")]
	[SerializeField]
	private Button moreMusicBtn = null;

	[SerializeField]
	private Button lessMusicBtn = null;

	[SerializeField]
	private TextMeshProUGUI volumeText = null;

	[Header("ES")]
	[SerializeField]
	private Toggle edgeScrolling = null;

	[Header("Menu")]
	[SerializeField]
	private Button mainMenuBtn = null;

	void Awake()
	{
		moreSoundBtn.onClick.AddListener(IncSound);
		lessSoundBtn.onClick.AddListener(DecSound);

		moreMusicBtn.onClick.AddListener(IncMusic);
		lessMusicBtn.onClick.AddListener(DecMusic);

		edgeScrolling.onValueChanged.AddListener(ChangeEdgeScrolling);

		mainMenuBtn.onClick.AddListener(MyMenu);

		gameObject.SetActive(false);
	}

	void Start() => edgeScrolling.isOn = CameraHandler.IsEdgeScrolling;

	void OnDestroy()
	{
		moreSoundBtn.onClick.RemoveAllListeners();
		lessSoundBtn.onClick.RemoveAllListeners();
		moreMusicBtn.onClick.RemoveAllListeners();
		lessMusicBtn.onClick.RemoveAllListeners();
		edgeScrolling.onValueChanged.RemoveAllListeners();
		mainMenuBtn.onClick.RemoveAllListeners();
	}

	void Update()
	{
		soundText.SetText(SoundManager.Volume.ToString());
		volumeText.SetText(MusicManager.Volume.ToString());
	}

	public void ToggleOptions()
	{
		gameObject.SetActive(!gameObject.activeSelf);

		if (gameObject.activeSelf)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
	}

	private void IncSound() =>
		SoundManager.Instance.ChangeVolumeBy(1);

	private void DecSound() =>
		SoundManager.Instance.ChangeVolumeBy(-1);

	private void IncMusic() =>
		MusicManager.Instance.ChangeVolumeBy(1);

	private void DecMusic() =>
		MusicManager.Instance.ChangeVolumeBy(-1);

	private void ChangeEdgeScrolling(bool value) =>
		CameraHandler.SetEdgeScrolling(value);

	private void MyMenu()
	{
		Time.timeScale = 1f;
		OwnSceneManager.Load(OwnSceneManager.Scenes.MainMenuScene);
	}
}
