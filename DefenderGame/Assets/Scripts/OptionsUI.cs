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

	[Header("Menu")]
	[SerializeField]
	private Button mainMenuBtn = null;

	void Awake()
	{
		moreSoundBtn.onClick.AddListener(IncSound);
		lessSoundBtn.onClick.AddListener(DecSound);

		moreMusicBtn.onClick.AddListener(IncMusic);
		lessMusicBtn.onClick.AddListener(DecMusic);
	}

	void OnDestroy()
	{
		moreSoundBtn.onClick.RemoveAllListeners();
		lessSoundBtn.onClick.RemoveAllListeners();
		moreMusicBtn.onClick.RemoveAllListeners();
		lessMusicBtn.onClick.RemoveAllListeners();
		mainMenuBtn.onClick.RemoveAllListeners();
	}

	void Update()
	{
		soundText.SetText(SoundManager.Volume.ToString());
		volumeText.SetText(MusicManager.Volume.ToString());
	}

	private void IncSound() => SoundManager.Instance.ChangeVolumeBy(1);

	private void DecSound() => SoundManager.Instance.ChangeVolumeBy(-1);

	private void IncMusic() => MusicManager.Instance.ChangeVolumeBy(1);

	private void DecMusic() => MusicManager.Instance.ChangeVolumeBy(-1);

	private void MyMenu()
	{

	}
}
