using UnityEngine;
using System.Collections;

public class InGameData : MonoBehaviour {

    private static string UserID;
    private static string FacebookID;
    private static InGameData _instance;
    private static GameObject container;

    public static InGameData getInstance()
    {
        if (_instance == null)
        {
            container = new GameObject("InGameData");
            _instance = new InGameData();
        }
        return _instance;
    }

    public void inputData(CLoginInfo CL)
    {
        UserID = CL.UserID;
        Debug.Log(UserID);
    }
}
