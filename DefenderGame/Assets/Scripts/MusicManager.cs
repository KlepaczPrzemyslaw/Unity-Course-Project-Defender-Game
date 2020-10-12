using UnityEngine;

public class MusicManager : MonoBehaviour
{
	public static int Volume { get; protected set; } = 5;

	public static MusicManager Instance;

	private AudioSource audioSource;
	private const string VolumeKey = "MusicVolume";

	void Awake()
	{
		Instance = this;

		if (PlayerPrefs.HasKey(VolumeKey))
		{
			Volume = PlayerPrefs.GetInt(VolumeKey);
		}
		else
		{
			PlayerPrefs.SetInt(VolumeKey, Volume);
			PlayerPrefs.Save();
		}

		audioSource = GetComponent<AudioSource>();
		audioSource.volume = Volume / 10f;
	}

	public void ChangeVolumeBy(int by)
	{
		Volume += by;
		Volume = Mathf.Clamp(Volume, 0, 10);
		audioSource.volume = Volume / 10f;
		PlayerPrefs.SetInt(VolumeKey, Volume);
		PlayerPrefs.Save();
	}
}
