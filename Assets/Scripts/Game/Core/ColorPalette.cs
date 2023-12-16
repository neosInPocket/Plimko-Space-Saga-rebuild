using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ColorPalette : MonoBehaviour
{
	[SerializeField] private PieceColors pieceColors;
	[SerializeField] private TemporaryColorViewer colorViewer;
	[SerializeField] private PieceColoring pieceColoring;
	[SerializeField] private List<ColorButton> buttons;
	private List<Color> allColors => pieceColors.Colors;
	private List<Color> usedColors;

	private void Start()
	{
		usedColors = new List<Color>();

		for (int i = 0; i < 3; i++)
		{
			buttons[i].OnClicked += OnButtonClicked;
			buttons[i].CurrentButtonColor = pieceColors.Colors[i];
		}
	}

	private void OnButtonClicked(ColorButton button)
	{
		if (usedColors.Contains(button.CurrentButtonColor))
		{
			colorViewer.Color = button.CurrentButtonColor;
			return;
		}

		pieceColoring.CurrentColor = button.CurrentButtonColor;
		colorViewer.Color = button.CurrentButtonColor;
	}

	public void DisableButton(Color color)
	{
		var button = buttons.FirstOrDefault(x => x.CurrentButtonColor == color);
		button.SilenceImage.gameObject.SetActive(true);
		usedColors.Add(button.CurrentButtonColor);
	}

	public void RemoveColor(Color color)
	{
		usedColors.Remove(color);
		pieceColoring.UsedColors.Remove(color);
		var button = buttons.FirstOrDefault(x => x.CurrentButtonColor == color);
		button.SilenceImage.gameObject.SetActive(false);
	}

	public void Restart()
	{
		usedColors.Clear();
		pieceColoring.UsedColors.Clear();
	}

	public void Clear()
	{
		usedColors.Clear();
		foreach (var button in buttons)
		{
			button.SilenceImage.gameObject.SetActive(false);
		}
	}
}
