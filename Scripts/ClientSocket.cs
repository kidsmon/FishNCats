using UnityEngine;
using System.Collections;

public class ClientSocket : MonoBehaviour {

	
	public ClientSocket _instance;
	public ServerController sc;
	public ReadHandler rh;
	public WriteHandler wh;



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
	   
	}

}
