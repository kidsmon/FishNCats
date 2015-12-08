using UnityEngine;
using System.Collections;

public class Cage_Collider : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Destroy(other.gameObject);
        }
    }
}