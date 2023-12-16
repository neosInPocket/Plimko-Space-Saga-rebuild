using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TutorialWindow : MonoBehaviour
{
	[SerializeField] private TMP_Text tutorialText;
	[SerializeField] private string[] texts;
	private Action OnEndAction;
	private int currentTextIndex;

	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();

		Touch.onFingerDown += OnFingerDown;
	}

	public void Show(Action endAction)
	{
		currentTextIndex = 1;
		gameObject.SetActive(true);
		OnEndAction = endAction;

		tutorialText.text = texts[0];
	}

	private void OnFingerDown(Finger finger)
	{
		tutorialText.text = texts[currentTextIndex];

		currentTextIndex++;
		if (currentTextIndex >= texts.Length)
		{
			OnEndAction();
			gameObject.SetActive(false);
		}
	}

	private void OnDisable()
	{
		Touch.onFingerDown -= OnFingerDown;
	}
}
