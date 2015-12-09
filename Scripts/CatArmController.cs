using UnityEngine;
using System.Collections;

public class CatArmController : MonoBehaviour
{
    // === 내부 파라미터 ===================================
    GameObject[] Fishes;
    public GameObject[] foodObjectList;

    // === 코드(MonoBehaviour기본 기능 구현) ================

    // === 코드(기본 액션) =================================
    public void ActionFeed(int foodlevel, int amount)
    {
        Fishes = GameObject.FindGameObjectsWithTag("Fish");
        GameObject[] go = new GameObject[amount];
        Transform goFood = transform.Find("Muzzle");
        for (int i = 0; i < amount; i++)
        {
            Vector3 RandomPositon = new Vector3(goFood.position.x + Random.Range(-1.5f, +1.5f),
                goFood.position.y + Random.Range(-0.7f, 0.0f), goFood.position.z);
            go[i] = Instantiate(foodObjectList[foodlevel], RandomPositon,
                        Quaternion.identity) as GameObject;
        }
        for (int i = 0; i < Fishes.Length; i++)
        {
            Fishes[i].GetComponent<BaseFishController>().TargetFood[0] = go[i * 2];
            Fishes[i].GetComponent<BaseFishController>().TargetFood[1] = go[i * 2 + 1];
            Fishes[i].GetComponent<FishMain>().aiState = FISHAISTS.EATFOOD;
        }
    }

    // === 코드(지원 함수) ================================
    public static CatArmController GetController()
    {
        return GameObject.FindGameObjectWithTag("CatArm").GetComponent<CatArmController>();
    }
}