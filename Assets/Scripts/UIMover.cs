using UnityEngine;
using System.Collections;

public class UIMover : MonoBehaviour {
	public Direction direction;
	public float speed = 2f;
	public float distance = 4000;
	public bool shown = false;
	public Vector3 startPos, newPos;
	private RectTransform rect;

	// Use this for initialization
	void Start () {
		rect = GetComponent<RectTransform> ();

		startPos = rect.anchoredPosition;
		if(direction == Direction.Down) {
			newPos = startPos + Vector3.down * distance;
		}
		if(direction == Direction.Up) {
			newPos = startPos + Vector3.up * distance;
		}
		if(direction == Direction.Left) {
			newPos = startPos + Vector3.left * distance;
		}
		if(direction == Direction.Right) {
			newPos = startPos + Vector3.right * distance;
		}
		if(!shown)
			rect.anchoredPosition = newPos;
	}
	
	// Update is called once per frame
	void Update () {
		if(shown) {
			rect.anchoredPosition = Vector3.Lerp(rect.anchoredPosition, startPos, speed * Time.deltaTime);
		} else {
			rect.anchoredPosition = Vector3.Lerp(rect.anchoredPosition, newPos, speed * Time.deltaTime);
		}
	}

	public enum Direction {
		Up,
		Down,
		Left,
		Right
	}
}
