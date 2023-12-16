using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelChooser : MonoBehaviour
{
	[SerializeField] private LevelButton prefab;
	[SerializeField] private int levelCount;
	[SerializeField] private Transform spawnContainer;
	[SerializeField] private SavePropertiesController saveController;
	[SerializeField] private Button playButton;
	[SerializeField] private LevelEngine levelEngine;
	[SerializeField] private GameStarter gameStarter;
	private List<LevelButton> buttons;
	private int currentSelectedLevel;

	private void Start()
	{
		buttons = new List<LevelButton>(levelCount);

		for (int i = 0; i < levelCount; i++)
		{
			var button = Instantiate(prefab, spawnContainer);
			button.Level = i;

			if ((int)saveController.GetPropertyValue(SaveType.LevelsPassed, PropertyType.Int) >= i)
			{
				button.Interactable = true;
			}
			else
			{
				button.Interactable = false;
			}

			buttons.Add(button);
			button.OnClicked += OnButtonClick;
		}

		playButton.interactable = false;
		playButton.onClick.AddListener(StartGame);
	}

	private void OnButtonClick(LevelButton button)
	{
		playButton.interactable = true;

		foreach (var levelButton in buttons)
		{
			levelButton.Selected = false;
		}

		button.Selected = true;
		currentSelectedLevel = buttons.IndexOf(button);
	}

	private void StartGame()
	{
		levelEngine.CurrentLevel = currentSelectedLevel + 1;
		gameStarter.FadeGame();
	}

	public void Refresh()
	{
		for (int i = 0; i < levelCount; i++)
		{
			if ((int)saveController.GetPropertyValue(SaveType.LevelsPassed, PropertyType.Int) >= i)
			{
				buttons[i].Interactable = true;
			}
			else
			{
				buttons[i].Interactable = false;
			}
		}
	}
}
