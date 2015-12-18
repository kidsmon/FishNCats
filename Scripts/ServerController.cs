using UnityEngine;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;

public class ServerController : MonoBehaviour
{

    private const string HostName = "52.69.229.4";
    private const int HostPort = 8107;

    private bool socketReady = false;
    // 소켓을 만들고 NetworkStream.GetStream을 통해 연결을 시작하면 true 아니면 false
    private TcpClient mySocket;
    //TcpClinet소켓, 동기 소켓이고 NetworkStream을 이용해 BeginRead 
    //함수에서 비동기 연결을 할 수 있다
    public NetworkStream NS;
    //NetworkStream객체를 이용해 Socket객체의 기능을 확대한다.

    private static ServerController _instace;
    //private static GameObject container;

    private static ReadHandler readHandlerInstance;
    public static ServerController getInstance()
    {
        if (_instace == null)
        {
            _instace = new ServerController();
        }
        return _instace;

    }

    private ServerController()
    {

    }
    public void initServerController()
    {

    }

    public bool setupServerController()
    {
        DontDestroyOnLoad(this);
        readHandlerInstance = ReadHandler.getInstance();
        DontDestroyOnLoad(readHandlerInstance);

        try
        {
            mySocket = new TcpClient(HostName, HostPort);
            if (mySocket.Connected)
            {
                NS = mySocket.GetStream();
                //NetworkStream 객체를 지정하여 데이터를 주고받는데 사용한다
                ReadHandler.getInstance().initReadHandler();
                //Debug.Log("Server Connection State:" + mySocket.Connected);
                socketReady = true;
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }

        return socketReady;
    }
    public void closeSocket()
    {
        if (!socketReady)
        {
            mySocket.Close();
            socketReady = false;
        }
    }
    /*
public void maintainConnection()
{
    if (!NS.CanRead)
        getInstance();
} 
    */
    public void Terminate()
    {
        //   WriteHanler.getInstance().sendConnectionCloseRequest();
        NS.Close();
        closeSocket();
    }
}
