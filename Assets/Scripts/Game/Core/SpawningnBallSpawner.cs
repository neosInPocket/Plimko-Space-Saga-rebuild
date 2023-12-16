using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SpawningnBallSpawner : MonoBehaviour
{
	[SerializeField] private SpawningBall ballPrefab;
	[SerializeField] private float delay;
	[SerializeField] private Vector2 xBounds;
	[SerializeField] private float yBound;
	[SerializeField] private int poolSize;
	[SerializeField] private PieceColors pieceColors;
	[SerializeField] private SavePropertiesController saveController;

	public bool Enabled { get; set; }
	public bool isSpawning { get; set; }
	private List<SpawningBall> ballsPool;
	private Vector2 screenSize;

	private void Start()
	{
		screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
		ballsPool = new List<SpawningBall>();


		for (int i = 0; i < poolSize; i++)
		{
			var ball = Instantiate(ballPrefab, transform);
			ball.gameObject.SetActive(false);
			var gravityScalePoints = (int)saveController.GetPropertyValue(SaveType.FallSpeed, PropertyType.Int);
			ball.Rigid.gravityScale = (1 - (float)gravityScalePoints / 4) / 10 + 0.15f;
			ballsPool.Add(ball);
		}
	}

	private void Update()
	{
		if (!Enabled)
		{
			StopAllCoroutines();
			return;
		}

		if (isSpawning) return;

		StartCoroutine(SpawnCoroutine());
	}

	private IEnumerator SpawnCoroutine()
	{
		isSpawning = true;
		float randomX = Random.Range(2 * xBounds.x * screenSize.x - screenSize.x, 2 * xBounds.y * screenSize.x - screenSize.x);
		float y = 2 * screenSize.y * yBound - screenSize.y;
		Vector2 position = new Vector2(randomX, y);

		var inactiveBall = ballsPool.FirstOrDefault(x => !x.gameObject.activeSelf);
		if (inactiveBall == null)
		{
			var ball = Instantiate(ballPrefab, position, Quaternion.identity, transform);
			ball.CurrentColor = pieceColors.PickRandomColor();
			ballsPool.Add(ball);
			var gravityScalePoints = (int)saveController.GetPropertyValue(SaveType.FallSpeed, PropertyType.Int);
			ball.Rigid.gravityScale = (1 - (float)gravityScalePoints / 4) / 8 + 0.2f;
		}
		else
		{
			inactiveBall.gameObject.SetActive(true);
			inactiveBall.transform.position = position;
			inactiveBall.CurrentColor = pieceColors.PickRandomColor();
		}

		yield return new WaitForSeconds(delay);

		isSpawning = false;
	}

	public void Clear()
	{
		foreach (var ball in ballsPool)
		{
			ball.gameObject.SetActive(false);
		}
	}
}
