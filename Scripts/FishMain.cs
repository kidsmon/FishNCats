using UnityEngine;
using System.Collections;

public enum FISHAISTS // 물고기 AI 상태
{
    ACTIONSELECT,       // 액션 선택(사고)
    MOVE,               // 일정 시간 동안 이동한다.
    FORWARD,            // 일정 시간동안 앞으로 이동한다.
    CHANGEANGLE,        // 각도를 바꾼다.
    SPIRALMOVE,         // 각도 회전과 이동을 동시에 한다.
    TRUN,               // 방향을 전환한다.
    WAIT,               // 일정 시간 (멈춰서) 기다린다.
    EATFOOD             // 먹이를 먹는다.
}

public class FishMain : MonoBehaviour
{
    // === 외부 파라미터(Inspector 표시) ===================

    // === 외부 파라미터 ==================================
    [System.NonSerialized] public FISHAISTS aiState     = FISHAISTS.ACTIONSELECT;
    [System.NonSerialized] public bool eating           = false;
    [System.NonSerialized] public Vector2 RandomSpeedXY = Vector2.zero;
    [System.NonSerialized] public float RandomAngle     = 0.0f;
    [System.NonSerialized] public GameObject FishPile;

    //  === 캐시 =========================================
    protected BaseFishController fishCtrl;

    // === 내부 파라미터 =================================
    float aiActionTimeLength        = 0.0f;
    float aiActionTimeStart         = 0.0f;
    protected float RandomTime      = 0.0f;
    protected float RandomSpeed     = 0.0f;

    // === 코드（Monobehaviour기본 기능 구현） ============
    public virtual void Awake()
    {
        fishCtrl = GetComponent<BaseFishController>();
        FishPile = GameObject.FindWithTag("FishPile");
    }

    public virtual void Update()
    {
        //Debug.Log(">>>> Time.Time = " + string.Format("{0}", Time.time)
        //    + "\n>>>> Time.FixedTime = "+ string.Format("{0}", Time.fixedTime));
        if (StartFishCommonWork())
        {
            UpdateAI();
            FinishFishCommonWork();
        }
    }

    // === 코드（기본 AI 동작 처리） ===========================


    public virtual void UpdateAI()
    {
    }

    public bool StartFishCommonWork()
    {
        return true;
    }

    public void FinishFishCommonWork()
    {
        // 액션 한계 시간 검사
        if (!eating)
        {
            float time = Time.time - aiActionTimeStart;
            if (time > aiActionTimeLength)
            {
                aiState = FISHAISTS.ACTIONSELECT;
            }
        }
    }

    // === 코드 (AI 스크립트 지원 함수) ========================
    public float SetRandomTime()
    {
        RandomTime = Random.Range(3.0f, 5.0f);
        return RandomTime;
    }

    public int SelectRandomAIState()
    {
        return Random.Range(0, 100 + 1);
    }

    public void SetAIState(FISHAISTS sts, float time)
    { // 기본적으로 상태와 지속시간만 설정
        aiState = sts;
        aiActionTimeStart = Time.time;
        aiActionTimeLength = time;
    }

    public void SetAIState(FISHAISTS sts, float time, Vector2 Optional)
    { // 상태와 지속시간 및 Vector2형 랜덤값 2개 설정
        aiState = sts;
        aiActionTimeStart = Time.time;
        aiActionTimeLength = time;
        if(sts == FISHAISTS.MOVE)
        {
            RandomSpeedXY = Optional;
        }
    }

    public void SetAIState(FISHAISTS sts, float time, float Optional)
    { // 상태와 지속시간 및 랜덤값 1개 설정
        aiState = sts;
        aiActionTimeStart = Time.time;
        aiActionTimeLength = time;
        if (sts == FISHAISTS.CHANGEANGLE)
        {
            RandomAngle = Optional;
        }
        else if (sts == FISHAISTS.FORWARD)
        {
            RandomSpeed = Optional;
        }
    }

    public void SetAIState(FISHAISTS sts, float time, float Optional1, float Optional2)
    { // 상태와 지속시간 및 랜덤값 2개 설정
        aiState = sts;
        aiActionTimeStart = Time.time;
        aiActionTimeLength = time;
        if (sts == FISHAISTS.SPIRALMOVE)
        {
            RandomSpeed = Optional1;
            RandomAngle = Optional2;
        }
    }
}