using UnityEngine;
using System.Collections;

public class MossController : MonoBehaviour
{
    // === 외부 파라미터(Inspector 표시) ===================
    public GameObject Mosses;
    public GameObject[] Moss;

    // === 캐쉬 =========================================


    // === 내부 파라미터 ==================================
    [System.NonSerialized] public Transform[] MossPositions;

    // === 유니티 기본 지원 함수 ===========================
    void Start()
    {
        MossPositions = new Transform[12];
        for (int i = 0; i < 11; i++)
        {
            MossPositions[i].position = new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(-4.0f, 4.0f));
        }
    }

    // === 그외 함수 들 ==================================
}