using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MaskableTransition : MaskableGraphic
{
	[SerializeField] private float transitionSpeed;
	[SerializeField] private Color maskColor;
	[SerializeField] private float maskTopEdge;
	[SerializeField] private float maskBottomEdge;
	[SerializeField] private float speedThreshold;
	[SerializeField] private CanvasGroup canvasGroup;
	[SerializeField] private bool enabledByDefault;

	private Vector2 screenSize;
	private UIVertex[] vertices;
	private float currentX;
	private bool isPlaying;
	public bool IsPlaying => isPlaying;
	public float CurrentX => currentX;

	protected override void Start()
	{
		if (!Application.isPlaying) return;

		base.Start();
		screenSize = new Vector2(Screen.width, Screen.height);

		if (enabledByDefault)
		{
			canvasGroup.blocksRaycasts = true;
			Vector2 a = new Vector2(-screenSize.x, -screenSize.y);
			Vector2 b = new Vector2(-screenSize.x, screenSize.y);
			Vector2 d = new Vector2(screenSize.x, -screenSize.y);
			Vector2 c = new Vector2(screenSize.x, screenSize.y);

			vertices = new UIVertex[]
			{
				new UIVertex { position = a, color = maskColor },
				new UIVertex { position = b, color = maskColor },
				new UIVertex { position = c, color = maskColor },
				new UIVertex { position = d, color = maskColor }
			};

			SetVerticesDirty();
		}
		else
		{
			Vector2 a = Vector2.zero;
			Vector2 b = Vector2.zero;
			Vector2 c = Vector2.zero;
			Vector2 d = Vector2.zero;

			vertices = new UIVertex[]
			{
				new UIVertex { position = a, color = maskColor },
				new UIVertex { position = b, color = maskColor },
				new UIVertex { position = c, color = maskColor },
				new UIVertex { position = d, color = maskColor }
			};

			SetVerticesDirty();

			canvasGroup.blocksRaycasts = false;
		}
	}

	public void PlayTransition(bool reverse)
	{
		StopAllCoroutines();
		StartCoroutine(Transition(reverse));
	}

	private IEnumerator Transition(bool reverse)
	{
		if (reverse)
		{
			canvasGroup.blocksRaycasts = false;
			Vector2 d = new Vector2(3 * screenSize.x, -screenSize.y);
			Vector2 c = new Vector2(3 * screenSize.x, screenSize.y);
			Vector2 a = new Vector2(2 * screenSize.x * maskBottomEdge - 3 * screenSize.x, -screenSize.y);
			Vector2 b = new Vector2(2 * screenSize.x * maskTopEdge - 3 * screenSize.x, screenSize.y);

			vertices = new UIVertex[]
			{
				new UIVertex { position = a, color = maskColor },
				new UIVertex { position = b, color = maskColor },
				new UIVertex { position = c, color = maskColor },
				new UIVertex { position = d, color = maskColor }
			};
		}
		else
		{
			canvasGroup.blocksRaycasts = true;
			Vector2 a = new Vector2(-3 * screenSize.x, -screenSize.y);
			Vector2 b = new Vector2(-3 * screenSize.x, screenSize.y);
			Vector2 d = new Vector2(2 * screenSize.x * maskBottomEdge - 3 * screenSize.x, -screenSize.y);
			Vector2 c = new Vector2(2 * screenSize.x * maskTopEdge - 3 * screenSize.x, screenSize.y);

			vertices = new UIVertex[]
			{
				new UIVertex { position = a, color = maskColor },
				new UIVertex { position = b, color = maskColor },
				new UIVertex { position = c, color = maskColor },
				new UIVertex { position = d, color = maskColor }
			};
		}

		isPlaying = true;

		if (reverse)
		{
			float distance = Mathf.Abs(vertices[0].position.x - screenSize.x);
			float magnitude = distance;

			while (vertices[0].position.x < 3 * screenSize.x / 2)
			{
				currentX = vertices[0].position.x;
				vertices[0].position.x += transitionSpeed * Time.deltaTime * (distance + speedThreshold) / magnitude;
				vertices[1].position.x += transitionSpeed * Time.deltaTime * (distance + speedThreshold) / magnitude;
				distance = Mathf.Abs(vertices[0].position.x - screenSize.x);
				yield return new WaitForEndOfFrame();
			}

			vertices[0].position.x = screenSize.x;
		}
		else
		{
			float distance = Mathf.Abs(vertices[3].position.x - screenSize.x);
			float magnitude = distance;

			while (vertices[3].position.x < 3 * screenSize.x / 2)
			{
				currentX = vertices[3].position.x;
				vertices[2].position.x += transitionSpeed * Time.deltaTime * (distance + speedThreshold) / magnitude;
				vertices[3].position.x += transitionSpeed * Time.deltaTime * (distance + speedThreshold) / magnitude;
				distance = Mathf.Abs(vertices[3].position.x - screenSize.x);
				yield return new WaitForEndOfFrame();
			}

			vertices[3].position.x = screenSize.x;
		}
		isPlaying = false;
	}


	protected override void OnPopulateMesh(VertexHelper vh)
	{
		if (vertices == null || vertices.Length == 0) return;

		vh.Clear();

		if (!Application.isPlaying) return;
		vh.AddUIVertexQuad(vertices);
	}

	private void Update()
	{
		if (!Application.isPlaying) return;

		if (!isPlaying) return;
		SetVerticesDirty();
	}
}
