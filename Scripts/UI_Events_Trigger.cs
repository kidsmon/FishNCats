using UnityEngine;
using System.Collections;


public class UI_Events_Trigger : MonoBehaviour
{
	

	// === 외부 파라미터(Inspector 표시) ===================


	// === 캐쉬 =========================================


	// === 내부 파라미터 ==================================
	GameObject CatArm;
	GameObject Panel;
	
	// === 유니티 기본 지원 함수 ===========================
	public void Awake()
	{
		CatArm = GameObject.FindGameObjectWithTag("CatArm");
		
	}
	// === 그외 함수 들 ==================================
	public void ClickSceneLoad(string SceneName)
	{
		Debug.Log("Clicked");
		StartCoroutine(LevelMove(SceneName));
	}
	
	public void ClickFood(int foodlevel)
	{
		Panel = GameObject.Find("Panel");
		int amount = 0;
		CatArm.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		GameObject[] Fishes = GameObject.FindGameObjectsWithTag("Fish");
		for(int i = 0; i < Fishes.Length; i++)
		{
			//Debug.Log("Fishes[i].name :" + Fishes[i].name);
			if (Fishes[i].activeSelf)
			{
				amount++;
			}
		}
		
		switch (foodlevel)
		{
			case 0:
				StartCoroutine(WaitAndFeed(foodlevel, amount));
				break;
			case 1:
				StartCoroutine(WaitAndFeed(foodlevel, amount));
				break;
			case 2:
				StartCoroutine(WaitAndFeed(foodlevel, amount));
				break;
		}
		StartCoroutine(WaitAndChangeColor());
		StartCoroutine(WaitAndSetActive());
	}

	IEnumerator LevelMove(string SceneName)
	{ // Scene전환용 버튼
		yield return new WaitForSeconds(1.0f);
		Application.LoadLevel(SceneName);
	}

	IEnumerator WaitAndFeed(int foodlevel, int amount)
	{
		yield return new WaitForSeconds(0.7f);
		CatArm.GetComponent<Animator>().SetTrigger("Feed");
		CatArm.GetComponent<CatArmController>().ActionFeed(foodlevel, amount * 2);
	}

	IEnumerator WaitAndChangeColor()
	{
		yield return new WaitForSeconds(2.0f);
		CatArm.GetComponent< SpriteRenderer >().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
	}

	IEnumerator WaitAndSetActive()
	{
		yield return new WaitForSeconds(0.43f);
		Panel.SetActive(false);
	}
}