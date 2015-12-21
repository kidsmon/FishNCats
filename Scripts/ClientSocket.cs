using UnityEngine;
using System.Collections;

public class ClientSocket : MonoBehaviour
{
	private static GameObject container;
	private static ClientSocket _instance;
	private static ServerController scinstance;
	private static ReadHandler rhinstance;
	private static WriteHandler whinstance;
	private static InGameData IGinstance;



	public static ClientSocket getInstance()
	{
		if(!_instance)
		{
			container = new GameObject("ClientSocket");
			_instance = container.AddComponent<ClientSocket>();

		}

		return _instance;
	}

	public void initClientSocket()
	{
		scinstance = ServerController.getInstance();
		DontDestroyOnLoad(scinstance);
		rhinstance = ReadHandler.getInstance();
		DontDestroyOnLoad(rhinstance);
		whinstance = WriteHandler.getInstance();
		DontDestroyOnLoad(whinstance);
		IGinstance = InGameData.getInstance();
		DontDestroyOnLoad(IGinstance);
	}

	public void sendData(RequestMessage Message)
	{
		Debug.Log(Message.Mtype);
		Debug.Log(Message.FacebookID);
		whinstance.sendData(Message);
	}
	public void sendData(LoginReqMessage Message)
	{
		Debug.Log(Message.Mtype);
		Debug.Log(Message.FacebookID);
		whinstance.sendData(Message);
	}
	public void inputData(CLoginInfo CL)
	{
		IGinstance.inputData(CL);
	}
}
