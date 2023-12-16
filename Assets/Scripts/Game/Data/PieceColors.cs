using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Piece colors")]
public class PieceColors : ScriptableObject
{
	[SerializeField] private List<Color> colors;
	public List<Color> Colors => colors;
	public Color DefaultColor => colors[0];
	
	public Color PickRandomColor()
	{
		return colors[Random.Range(0, colors.Count)];
	}
}
