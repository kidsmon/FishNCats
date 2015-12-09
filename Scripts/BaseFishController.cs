using UnityEngine;
using System.Collections;

public class BaseFishController : MonoBehaviour
{
	// === 외부 파라미터(Inspector 표시) ===================
	[Range(0.0f, 5.0f)] public float Movspeed = 3.51f;

	// === 외부 파라미터 ==================================
	[System.NonSerialized] public int          grade;
	[System.NonSerialized] public int          age;
	[System.NonSerialized] public int          price;
	[System.NonSerialized] public bool         Right  = true;
	[System.NonSerialized] public bool         Starve = false;
	[System.NonSerialized] public bool         Diease = false;
	[System.NonSerialized] public bool         Die    = false;
	[System.NonSerialized] public bool         Mate   = true;
	[System.NonSerialized] public int          dir    = 1;
	[System.NonSerialized] public GameObject[] TargetFood;

	// === 애니메이션 해시 이름
	public readonly static int ANISTS_Move  = Animator.StringToHash("Base Layer.Fish_Test_Move");

	// === 캐시 =========================================
	[System.NonSerialized] public Animator animator;


	// === 내부 파라미터 =================================
	

	// === 코드 (Monobehaviour기본 기능 구현) =============
	public virtual void Awake ()
	{
		animator = GetComponent<Animator>();
		TargetFood = new GameObject[2];
	}

	// === 코드 (기본 액션) ==============================
	public void ActionForward(float Movspeed)
	{
		//Debug.Log("Move, transform.localposition : " + string.Format("{0}", transform.localPosition.x));
		transform.Translate(Movspeed * Time.deltaTime, 0.0f, 0.0f);
	}

	public void ActionMove(Vector2 MovSpeed)
	{
		
		transform.Translate((Vector3)MovSpeed * Time.deltaTime);
	}

	public bool ActionMoveToNear(GameObject go, float near, float Movspeed)
	{
		if(Vector3.Distance(transform.position, go.transform.position) > near)
		{
			ActionForward(Movspeed);
			return true;
		}
		return false;
	}

	public void ActionMoveToFood(int targetNum)
	{
		Vector3 relativePos = TargetFood[targetNum].transform.position - transform.position;
		relativePos = relativePos.normalized;

		transform.position += relativePos * Time.deltaTime * Movspeed;
		
		if (GetComponent<HorizontalFishMain>())
		{
			transform.right = relativePos;
			if (TargetFood[targetNum].transform.position.x < transform.position.x)
			{
				transform.Rotate(180.0f, 0.0f, 0.0f);
				
			}
		}
		else
		{
			if (TargetFood[targetNum].transform.position.x < transform.position.x &&
				transform.forward == Vector3.forward)
				transform.Rotate(0.0f, 180.0f, 0.0f);
			else if (TargetFood[targetNum].transform.position.x > transform.position.x &&
				transform.forward == -Vector3.forward)
				transform.Rotate(0.0f, 180.0f, 0.0f);
		}
	}

	public bool ActionMoveToPile(GameObject go, float near)
	{
		if (Vector3.Distance(transform.position, go.transform.position) > near)
		{
			
			return true;
		}
		return false;
	}

	public void ActionTurn()
	{
		if (dir == 1)
		{// Turn Left
			transform.Rotate(0.0f, -180.0f * 5.0f * Time.deltaTime, 0.0f);
			if (transform.localEulerAngles.y == 180.0f)
				dir = -1;
		}
		else if (dir == -1)
		{// Trun Right
			transform.Rotate(0.0f, +180.0f * 5.0f * Time.deltaTime, 0.0f);
			if (transform.localEulerAngles.y == 180.0f)
				dir = 1;
		}
	}

	public void SpeedTurn()
	{
		
		//Debug.Log("Turn(1) =>> dir = " + string.Format("{0}", dir));
		if (dir == 1)
		{
			transform.Rotate(0.0f, +180.0f, 0.0f);
			dir = -1;
		}
		else if( dir == -1)
		{
			transform.Rotate(0.0f, +180.0f, 0.0f);
			dir = 1;
		}
		//Debug.Log("Turn(2) =>> dir = " + string.Format("{0}", dir));
	}

	public void ActionChagneAngle(float angle)
	{

		transform.Rotate(0.0f, 0.0f, angle);
		Vector3 ang = transform.eulerAngles;// 회전값을 읽어오고
		if (ang.z > 180)
			ang.z -= 360;  // 180이상의 수는 음수로 표현.
		ang.z = Mathf.Clamp(ang.z, -30, 30);// -회전각 범위 제한
		transform.eulerAngles = ang; //-새로운 각도를 회전각으로 설정
		//Debug.Log(string.Format(">>> transform.localEulerAngles.z = {0}", transform.localEulerAngles.z));
		//Debug.Log(string.Format(">>> transform.EulerAngles.z = {0}", transform.eulerAngles.z));
	}
}