using UnityEngine;

public class LevelEngine : MonoBehaviour
{
	[SerializeField] private SpawningnBallSpawner ballSpawner;
	[SerializeField] private TutorialWindow tutorialWindow;
	[SerializeField] private SavePropertiesController saveController;
	[SerializeField] private PopupScreenController popupScreenController;
	[SerializeField] private PieceColoring pieceColoring;
	[SerializeField] private MainObjectSpawner mainObjectSpawner;
	[SerializeField] private WinLoseWindow winLose;
	[SerializeField] private AnimationCurve targetBallsCurve;
	[SerializeField] private AnimationCurve coinsCurve;
	[SerializeField] private GameStarter gameStarter;
	public int CurrentLevel { get; set; }

	public void StartLevel(SavePropertiesController controller)
	{
		saveController = controller;
		mainObjectSpawner.SetZones((int)targetBallsCurve.Evaluate(CurrentLevel), OnCompleted);
		mainObjectSpawner.Clear();

		bool tutorialPassed = (bool)saveController.GetPropertyValue(SaveType.TutorialCompleted, PropertyType.Bool);

		if (!tutorialPassed)
		{
			tutorialWindow.Show(OnTutorialEnd);
			saveController.SetPropertyValue(SaveType.TutorialCompleted, PropertyType.Bool, true);
		}
		else
		{
			OnTutorialEnd();
		}
	}

	private void OnTutorialEnd()
	{
		string[] texts = { $"LEVEL {CurrentLevel}", "3", "2", "1", "GO!" };
		popupScreenController.Show(OnPopupEnd, texts);
	}

	private void OnPopupEnd()
	{
		ballSpawner.Enabled = true;
		pieceColoring.Enabled = true;
		ballSpawner.isSpawning = false;
	}

	private void OnCompleted()
	{
		winLose.ShowWindow(true, 1, (int)coinsCurve.Evaluate(CurrentLevel));
		ballSpawner.Enabled = false;
		pieceColoring.Enabled = false;
		ballSpawner.Clear();
		pieceColoring.Disable();
		mainObjectSpawner.Clear();

		int levelPassed = (int)saveController.GetPropertyValue(SaveType.LevelsPassed, PropertyType.Int);
		int coins = (int)saveController.GetPropertyValue(SaveType.Coins, PropertyType.Int);
		int gems = (int)saveController.GetPropertyValue(SaveType.Gems, PropertyType.Int);

		if (CurrentLevel == levelPassed + 1)
		{
			saveController.SetPropertyValue(SaveType.LevelsPassed, PropertyType.Int, levelPassed + 1);
		}

		saveController.SetPropertyValue(SaveType.Coins, PropertyType.Int, coins + (int)coinsCurve.Evaluate(CurrentLevel));
		saveController.SetPropertyValue(SaveType.Gems, PropertyType.Int, gems + 1);
	}

	public void Menu()
	{
		gameStarter.Menu();
		gameObject.SetActive(false);
	}
}
