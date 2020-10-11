using UnityEngine;

public class MusicManager : MonoBehaviour
{
	public static int Volume { get; protected set; } = 5;

	public static MusicManager Instance;

	private AudioSource audioSource;

	void Awake()
	{
		Instance = this;
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = Volume / 10f;
	}

	public void ChangeVolumeBy(int by)
	{
		Volume += by;
		Volume = Mathf.Clamp(Volume, 0, 10);
		audioSource.volume = Volume / 10f;
	}
}
