using System;
using System.Collections;
using UnityEngine;

public class SpawningBall : MonoBehaviour
{
	[SerializeField] private CircleCollider2D circleCollider2D;
	[SerializeField] private Rigidbody2D rigid2D;
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private GameObject spawnEffect;
	public Rigidbody2D Rigid => rigid2D;
	public bool IsAttracting { get; set; }

	public Color CurrentColor
	{
		get => currentColor;
		set
		{
			currentColor = value;
			spriteRenderer.color = value;
		}
	}

	private Color currentColor;

	private void Start()
	{

	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<DeathTrigger>(out DeathTrigger deathTrigger))
		{
			gameObject.SetActive(false);
		}
	}

	private void OnEnable()
	{
		StartCoroutine(SpawnEffect());
		IsAttracting = false;
	}

	private IEnumerator SpawnEffect()
	{
		spawnEffect.gameObject.SetActive(true);
		yield return new WaitForSeconds(1);
		spawnEffect.gameObject.SetActive(false);
	}
}
