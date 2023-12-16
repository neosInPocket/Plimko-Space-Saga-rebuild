using System;
using TMPro;
using UnityEngine;

public class PopupScreenController : MonoBehaviour
{
	[SerializeField] private TMP_Text popupText;
	[SerializeField] private Animator animator;
	private Action EndAction;
	private int currentTextIndex;
	private string[] currentTexts;

	public void Show(Action onEndAction, string[] texts)
	{
		gameObject.SetActive(true);
		EndAction = onEndAction;
		popupText.text = texts[0];
		currentTextIndex = 0;
		currentTexts = texts;
	}

	public void NextText()
	{
		currentTextIndex++;
		if (currentTextIndex >= currentTexts.Length)
		{
			End();
			return;
		}

		animator.SetTrigger("next");
		popupText.text = currentTexts[currentTextIndex];
	}

	public void End()
	{
		EndAction();
		gameObject.SetActive(false);
	}
}
