using UnityEngine;
using UnityEngine.UI;

public class PieceSlider : MonoBehaviour
{
	[SerializeField] private SavePropertiesController saveController;
	[SerializeField] private Image[] pieces;
	[SerializeField] private SaveType fillType;
	[SerializeField] private MusicController musicController;
	private int currentState;

	private void Start()
	{
		SetValue((float)saveController.GetPropertyValue(fillType, PropertyType.Float));
	}

	public void SetValue(float value)
	{
		int fillValue = (int)(value * pieces.Length);
		musicController.SetMusicVolumeValue(value);
		saveController.SetPropertyValue(fillType, PropertyType.Float, value);

		foreach (var piece in pieces)
		{
			piece.enabled = false;
		}

		if (fillValue > 0)
		{
			pieces[fillValue - 1].enabled = true;
		}

		currentState = fillValue;
	}

	public void IncreaseFillValue(int value)
	{
		int newValue = currentState + value;

		if (newValue > pieces.Length || newValue < 0)
		{
			return;
		}

		SetValue((float)newValue / (float)pieces.Length);
	}
}
