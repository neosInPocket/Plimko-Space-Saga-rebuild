using System.Collections;
using UnityEngine;

public class MaskableTransitionController : MonoBehaviour
{
	[SerializeField] private MaskableTransition menuTransition;
	[SerializeField] private MaskableTransition storeTransition;
	[SerializeField] private MaskableTransition optionsTransition;
	private MaskableTransition currentScreen;

	private void Start()
	{
		currentScreen = menuTransition;
	}

	public void MenuToStore(MaskableTransition target)
	{
		currentScreen.PlayTransition(true);
		target.PlayTransition(false);
		currentScreen = target;
	}
}
