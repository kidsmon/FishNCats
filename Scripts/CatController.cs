using UnityEngine;
using System.Collections;

public class CatController : MonoBehaviour
{
	Animator CatAnimator;
	public void Awake()
	{
		CatAnimator = GetComponent<Animator>();
	}

	public void Start()
	{
		StartCoroutine("SetWalk");
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if(other.name == "Cage")
		{
			transform.Rotate(0.0f, 180.0f, 0.0f);
		}
	}

	public void Update()
	{
		AnimatorStateInfo info = CatAnimator.GetCurrentAnimatorStateInfo(0);
		if (info.fullPathHash.Equals(Animator.StringToHash("Base Layer.Cat_Walk")))
			transform.Translate(-0.5f * Time.deltaTime, 0.0f, 0.0f);
	}
	
	IEnumerator SetWalk()
	{
		while(true)
		{
			CatAnimator.SetTrigger("Walk");
			yield return new WaitForSeconds(11.0f);
		}
	}
}
