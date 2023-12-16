using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlider : MonoBehaviour
{
	[SerializeField] private GoodsPanel goodsPanel;
	[SerializeField] private SavePropertiesController saveController;
	[SerializeField] private Image[] pieces;
	[SerializeField] private Image unavaliable;
	[SerializeField] private Button purchaseButton;
	[SerializeField] private SaveType fillType;
	[SerializeField] private SaveType coinType;
	[SerializeField] private float cost;

	private void Start()
	{
		Refresh();
	}

	public void Refresh()
	{
		DisableAll();
		int value = (int)saveController.GetPropertyValue(fillType, PropertyType.Int);
		if (value != 0)
		{
			pieces[value - 1].enabled = true;
		}

		bool enabledValue = (int)saveController.GetPropertyValue(coinType, PropertyType.Int) < cost || value >= 9;

		unavaliable.enabled = enabledValue;
		purchaseButton.gameObject.SetActive(!enabledValue);
	}

	public void Purchase()
	{
		var value = (int)saveController.GetPropertyValue(fillType, PropertyType.Int);
		var coins = (int)saveController.GetPropertyValue(coinType, PropertyType.Int);

		saveController.SetPropertyValue(fillType, PropertyType.Int, value + 1);
		saveController.SetPropertyValue(coinType, PropertyType.Int, coins - cost);
		Refresh();
		goodsPanel.Refresh();
	}

	private void DisableAll()
	{
		foreach (var piece in pieces)
		{
			piece.enabled = false;
		}
	}
}
