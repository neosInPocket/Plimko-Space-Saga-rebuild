using UnityEngine;

public class GameStarter : MonoBehaviour
{
	[SerializeField] private Animator animator;
	[SerializeField] private LevelEngine levelEngine;
	[SerializeField] private SavePropertiesController saveController;
	[SerializeField] private GameObject menuObjects;
	[SerializeField] private GoodsPanel goodsPanel;
	[SerializeField] private UpgradesController upgradesController;
	[SerializeField] private LevelChooser levelChooser;

	public void FadeGame()
	{
		animator.SetTrigger("start");
	}

	public void StartLevelEngine()
	{
		levelEngine.gameObject.SetActive(true);
		levelEngine.StartLevel(saveController);
		menuObjects.SetActive(false);
	}

	public void Menu()
	{
		menuObjects.SetActive(true);
		animator.SetTrigger("menu");
		goodsPanel.Refresh();
		upgradesController.Refresh();
		levelChooser.Refresh();
	}
}
