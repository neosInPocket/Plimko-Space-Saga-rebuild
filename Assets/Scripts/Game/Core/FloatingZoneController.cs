using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingZoneController : MonoBehaviour
{
	[SerializeField] private SpriteRenderer zoneRenderer;
	[SerializeField] private ParticleSystem pSystem;
	[SerializeField] private float alpha;
	[SerializeField] private Canvas innerCanvas;
	[SerializeField] private TMP_Text counterText;
	public Canvas InnerCanvas => innerCanvas;


	public Color CurrentColor
	{
		get => currentColor;
		set
		{
			var color = value;
			color.a = alpha;
			zoneRenderer.color = color;
			var main = pSystem.main;
			main.startColor = value;
			currentColor = value;
		}
	}

	public Vector2 Size
	{
		get => size;
		set
		{
			size = value;
			var shape = pSystem.shape;
			shape.scale = value;
			zoneRenderer.size = value;
			var rectTransform = innerCanvas.GetComponent<RectTransform>();
			rectTransform.sizeDelta = value;
			innerCanvas.transform.localPosition = Vector2.zero;
		}
	}

	private Vector2 size;

	public int TargetBallCount { get; set; }
	public int currentBallCount
	{
		get
		{
			if (targetBalls == null || targetBalls.Count == 0)
			{
				return 0;
			}
			else
			{
				return targetBalls.Count;
			}
		}
	}

	private Color currentColor;
	private List<SpawningBall> targetBalls;

	private void Start()
	{
		targetBalls = new List<SpawningBall>();
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<SpawningBall>(out SpawningBall ball))
		{
			if (ball.CurrentColor == CurrentColor && currentBallCount < TargetBallCount)
			{
				targetBalls.Add(ball);
				RefreshText();
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.TryGetComponent<SpawningBall>(out SpawningBall ball))
		{
			if (ball.CurrentColor == CurrentColor || targetBalls.Contains(ball))
			{
				targetBalls.Remove(ball);
				RefreshText();
			}
		}
	}

	public void RefreshText()
	{
		counterText.text = $"{currentBallCount}/{TargetBallCount}";
	}
}
