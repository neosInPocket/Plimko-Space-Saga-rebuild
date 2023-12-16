using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoodsPanel : MonoBehaviour
{
	[SerializeField] private SavePropertiesController propertiesController;
	[SerializeField] private TMP_Text gemText;
	[SerializeField] private TMP_Text coinsText;

	private void Start()
	{
		Refresh();
	}

	public void Refresh()
	{
		gemText.text = propertiesController.GetPropertyValue(SaveType.Gems, PropertyType.Int).ToString();
		coinsText.text = propertiesController.GetPropertyValue(SaveType.Coins, PropertyType.Int).ToString();
	}
}
