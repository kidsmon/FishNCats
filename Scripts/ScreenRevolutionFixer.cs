using UnityEngine;
using System.Collections;

public class ScreenRevolutionFixer : MonoBehaviour
{

	void Awake()
	{
		Screen.SetResolution(1920, 1080, true);
	}
}
