using UnityEngine;
using UnityEngine.UI;

public class TemporaryColorViewer : MonoBehaviour
{
	[SerializeField] private Image temp;
	
	public Color Color
	{
		get => temp.color;
		set => temp.color = value;
	}
}
