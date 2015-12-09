using UnityEngine;
using System.Collections;

public class VerticalFishCollider : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Fish OnTriggerEnter2D : " + other.name);
        if (other.name == "VerticalCage")
        {
            //Debug.Log("Turn(Cage)" + string.Format("{0}", Time.time) + " =>> dir = " + string.Format("{0}", GetComponentInParent<BaseFishController>().dir));
            GetComponentInParent<BaseFishController>().SpeedTurn();
        }
        if (other.name == "HorizontalCage")
        {
            GetComponentInParent<FishMain>().RandomSpeedXY.y = -GetComponentInParent<FishMain>().RandomSpeedXY.y;
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