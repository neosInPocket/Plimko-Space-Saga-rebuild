using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseWindow : MonoBehaviour
{
	[SerializeField] private Image winImage;
	[SerializeField] private Image loseImage;
	[SerializeField] private TMP_Text resultText;
	[SerializeField] private TMP_Text coinsText;
	[SerializeField] private TMP_Text gemsText;
	[SerializeField] private Animator animator;
	[SerializeField] private LevelEngine levelEngine;

	public void ShowWindow(bool result, int gems = 0, int coins = 0)
	{
		gameObject.SetActive(true);

		gemsText.text = gems.ToString();
		coinsText.text = coins.ToString();

		resultText.text = result ? "YOU WIN" : "YOU  LOSE";
		winImage.enabled = result;
		loseImage.enabled = !result;
	}

	public void Hide()
	{
		animator.SetTrigger("hide");
	}

	public void HideAction()
	{
		levelEngine.Menu();
		gameObject.SetActive(false);
	}
}
