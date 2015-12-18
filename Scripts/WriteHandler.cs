using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

class WriteHandler : MonoBehaviour
{
    
    private static WriteHandler _instance;
    private static GameObject container;

    public static WriteHandler getInstance()
    {
        if (_instance == null)
        {
            container = new GameObject("WriteHandler");
            _instance = new WriteHandler();
        }
        return _instance;
    }

    private WriteHandler()
    {

    }

    public void sendData(string MessageJSON)
    {
        byte[] sendByte = Encoding.ASCII.GetBytes(MessageJSON);
        ServerController.getInstance().NS.Write(sendByte, 0, sendByte.Length);
        //Console.WriteLine("sendData");
    }

    public void sendstring(string sendS)
    {
        //string 테스트 전송
        byte[] sendByte = Encoding.ASCII.GetBytes(sendS);
        ServerController.getInstance().NS.Write(sendByte, 0, sendByte.Length);
        //Console.WriteLine("sendstring");
        Debug.Log("testtring");
    }

}

