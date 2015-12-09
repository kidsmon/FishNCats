using UnityEngine;
using System.Collections;

public class HorizontalFishCollider : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log("Fish OnTriggerEnter2D : " + other.name);
		if (other.name == "VerticalCage")
		{
			GetComponentInParent<BaseFishController>().SpeedTurn();
		}
		else if (other.name == "HorizontalCage")
		{
			if (GetComponentInParent<HorizontalFishMain>().aiState == FISHAISTS.FORWARD)
			{
				transform.parent.eulerAngles = new Vector3(transform.eulerAngles.x,
					transform.eulerAngles.y, 360.0f - transform.parent.eulerAngles.z);
			}
			else if (GetComponentInParent<HorizontalFishMain>().aiState == FISHAISTS.SPIRALMOVE)
			{
				transform.parent.eulerAngles = new Vector3(transform.eulerAngles.x,
					transform.eulerAngles.y, 360.0f - transform.parent.eulerAngles.z);
				GetComponentInParent<FishMain>().RandomAngle = -GetComponentInParent<FishMain>().RandomAngle;
			}
		}
	}
	
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Food")
		{
			if (other.gameObject == GetComponentInParent<BaseFishController>().TargetFood[0])
			{
				Destroy(other.gameObject);
				if (gameObject.GetComponent<HorizontalFishMain>())
				{
					GetComponentInParent<BaseFishController>().ActionForward(0.0f);
				}
				else
				{
					GetComponentInParent<BaseFishController>().ActionMove(Vector2.zero);
				}
				GetComponentInParent<Animator>().SetTrigger("Eat");
			}
			else if (other.gameObject == GetComponentInParent<BaseFishController>().TargetFood[1]
				&& !GetComponentInParent<BaseFishController>().TargetFood[0])
			{
				Destroy(other.gameObject);
				if (gameObject.GetComponent<HorizontalFishMain>())
				{
					GetComponentInParent<BaseFishController>().ActionForward(0.0f);
				}
				else
				{
					GetComponentInParent<BaseFishController>().ActionMove(Vector2.zero);
				}
				GetComponentInParent<Animator>().SetTrigger("Eat");
			}
		}
	}
}