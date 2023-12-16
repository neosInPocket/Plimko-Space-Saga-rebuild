using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
	[SerializeField] private TMP_Text levelText;
	[SerializeField] private Image checkmark;
	[SerializeField] private Button button;

	private int level;
	public int Level
	{
		get => level;
		set
		{
			level = value;
			levelText.text = (value + 1).ToString();
		}
	}

	public bool Interactable
	{
		get => button.interactable;
		set => button.interactable = value;
	}

	public bool Selected
	{
		get => checkmark.enabled;
		set
		{
			checkmark.gameObject.SetActive(value);
			levelText.gameObject.SetActive(!value);
		}
	}

	public Button Button => button;
	public Action<LevelButton> OnClicked;

	private void Start()
	{
		button.onClick.AddListener(ButtonClick);
	}

	private void ButtonClick()
	{
		OnClicked?.Invoke(this);
	}
}
