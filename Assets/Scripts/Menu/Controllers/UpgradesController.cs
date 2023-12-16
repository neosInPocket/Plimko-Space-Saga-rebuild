using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesController : MonoBehaviour
{
	[SerializeField] private List<UpgradeSlider> upgrades;

	private void Start()
	{
		Refresh();
	}

	public void Refresh()
	{
		foreach (var upgrade in upgrades)
		{
			upgrade.Refresh();
		}
	}
}
