using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;


public class Piece : MonoBehaviour
{
	[SerializeField] private CircleCollider2D circleCollider;
	[SerializeField] private Rigidbody2D rigidBody;
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private CircleCollider2D magnetZone;
	[SerializeField] private GameObject particles;
	public float magnetStrength { get; set; }

	public bool Enabled
	{
		get => isEnabled;
		set
		{
			if (value)
			{
				particles.SetActive(true);
			}
			isEnabled = value;
		}
	}

	private bool isEnabled;

	private List<SpawningBall> targetBalls;
	private Color currentAttractedColor
	{
		get => attractedColor;
		set
		{
			attractedColor = value;
			spriteRenderer.color = value;
		}
	}

	private Color attractedColor;

	private void Start()
	{
		targetBalls = new List<SpawningBall>();
	}

	public void Enable(float value)
	{
		magnetStrength = value;
	}

	public void ToggleMagnet(Color color)
	{
		if (!Enabled)
		{
			Enabled = true;
			currentAttractedColor = color;
		}
		else
		{
			if (currentAttractedColor == color)
			{
				Enabled = false;
				currentAttractedColor = Color.white;
				particles.SetActive(false);
			}
		}
	}

	public void Disable()
	{
		Enabled = false;
		currentAttractedColor = Color.white;
		particles.SetActive(false);
	}

	public System.Action<Color> ColorUsed;

	private void Update()
	{
		if (targetBalls.Count == 0) return;

		foreach (var ball in targetBalls)
		{
			if (!ball.IsAttracting)
			{
				if (currentAttractedColor != ball.CurrentColor) continue;
			}

			var difference = -ball.transform.position + transform.position;
			var direction = difference.normalized;
			var distance = difference.magnitude;
			var traveled = distance / magnetZone.radius;

			ball.Rigid.velocity = Vector2.Lerp(ball.Rigid.velocity, direction * magnetStrength, 1 - traveled);
		}
	}

	private void OnTriggerStay2D(Collider2D collider)
	{
		if (collider.TryGetComponent<SpawningBall>(out SpawningBall ball))
		{
			if (targetBalls.Contains(ball) || ball.IsAttracting) return;

			if (Enabled)
			{
				if (currentAttractedColor != ball.CurrentColor)
				{
					targetBalls.Add(ball);
					return;
				}

				targetBalls.Add(ball);
				ball.IsAttracting = true;
				Enabled = false;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.TryGetComponent<SpawningBall>(out SpawningBall ball))
		{
			if (targetBalls.Contains(ball))
			{
				targetBalls.Remove(ball);
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.TryGetComponent<SpawningBall>(out SpawningBall ball))
		{
			if (currentAttractedColor == ball.CurrentColor)
			{
				particles.gameObject.SetActive(false);
				currentAttractedColor = Color.white;
				ColorUsed?.Invoke(ball.CurrentColor);
			}
		}
	}
}
