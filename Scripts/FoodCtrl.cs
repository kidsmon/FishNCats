using UnityEngine;
using System.Collections;

public class FoodCtrl : MonoBehaviour
{
	public float speed;

	void Update ()
	{
		transform.Translate(0.0f, speed * Time.deltaTime, 0.0f);
	}
}
