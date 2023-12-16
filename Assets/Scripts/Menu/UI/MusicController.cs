using UnityEngine;

public class MusicController : MonoBehaviour
{
	[SerializeField] private AudioSource musicSource;
	[SerializeField] private SavePropertiesController saveProperties;
	[SerializeField] private SaveType musicType;

	private void Start()
	{
		if (musicSource != null)
		{
			musicSource.loop = true;
		}

		SetMusicVolumeValue((float)saveProperties.GetPropertyValue(musicType, PropertyType.Float));
	}

	public void SetMusicVolumeValue(float value)
	{
		if (musicSource != null)
		{
			musicSource.volume = value;
		}

	}
}
