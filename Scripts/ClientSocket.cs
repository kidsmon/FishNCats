using UnityEngine;
using System.Collections;

public class ClientSocket : MonoBehaviour {

	
	private ClientSocket _instance;
	private ServerController sc;
	private ReadHandler rh;
	private WriteHandler wh;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public ClientSocket getInstance()
	{
		if(!_instance)
		{
			_instance = new ClientSocket();
			
		}

		return _instance;
	}

	public void initClientSocket()
	{
		sc = ServerController.getInstance();
		rh = ReadHandler.getInstance();
		wh = WriteHandler.getInstance();
	}

	public void sendtest(string writestring)
	{
		wh.sendstring(writestring);
	}

}
