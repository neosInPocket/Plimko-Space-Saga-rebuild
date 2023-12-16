using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
	[SerializeField] private Camera targetCamera;
	private Vector2 innitialPoint;
	private float initialZoomValue;

	public void ZoomToPoint(Vector2 point, float size, float zoomTime)
	{
		initialZoomValue = targetCamera.orthographicSize;
		innitialPoint = targetCamera.transform.position;
		
		Vector3 targetPos = new Vector3(point.x, point.y, targetCamera.transform.position.z);
		StopAllCoroutines();
		StartCoroutine(ZoomCoroutine(targetPos, size, zoomTime));
	}

	private IEnumerator ZoomCoroutine(Vector3 targetPos, float size, float zoomTime)
	{
		float startTime = Time.time;
		float startSize = targetCamera.orthographicSize;
		while (Time.time - startTime < zoomTime)
		{
			float t = (Time.time - startTime) / zoomTime;
			targetCamera.transform.position = Vector3.Lerp(targetCamera.transform.position, targetPos, t);
			targetCamera.orthographicSize = Mathf.Lerp(startSize, size, t);
			yield return null;
		}
		targetCamera.transform.position = targetPos;
		targetCamera.orthographicSize = size;
	}
	
	public void ReturnToInitialState(float time)
	{
		StopAllCoroutines();
		
		ZoomToPoint(innitialPoint, initialZoomValue, time);
	}
}
