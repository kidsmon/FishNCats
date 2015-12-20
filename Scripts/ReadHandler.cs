using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using LitJson;

public class ReadHandler : MonoBehaviour {

    private static ReadHandler _instance;
    private static GameObject container;
    private const int READ_BUFFER_SIZE = 1024;
    private byte[] readBuffer = new byte[READ_BUFFER_SIZE];
    private byte[] saveBytes = new byte[READ_BUFFER_SIZE];

    private List<int> offList = new List<int>();
    private List<int> lenList = new List<int>();
    private int saveOff = 0;
    private int readLen = 0;
    private bool clearFlag = true;
    public delegate void DelegateReceiver(byte[] data);
    public DelegateReceiver receiver;



    public static ReadHandler getInstance()
    {
        if (!_instance)
        {
            container = new GameObject("ReadHandler");
            _instance = container.AddComponent<ReadHandler>() ;
        }

        return _instance;
    }

    private ReadHandler()
    {
   
    }


    public void initReadHandler()
    {   
        //!!
        //데이터 관리 인스턴스 생성필요
        ServerController.getInstance().NS.BeginRead(readBuffer, 0,
            READ_BUFFER_SIZE, new AsyncCallback(RHandlerCallbackfunc), null);

        receiver = new DelegateReceiver(mainReceiver);
    }

    public void RHandlerCallbackfunc(IAsyncResult ar)
    {
        int BytesRead; //읽은 바이트 수로 초기화
        try
        {
            BytesRead = ServerController.getInstance().NS.EndRead(ar);
            //EndRead 메서드는 읽은 바이트 수를 반환하기 위해 호출
            //데이터를 사용할 수 이 있을 때까지 차단된다.
            if (BytesRead < 1)
            {
                //disconnected
                Debug.Log("Disconnected");
                return;
            }
            parser(readBuffer, BytesRead);

            for (int i = 0; i < BytesRead; i++)
                readBuffer[i] = 0;
            //데이터를 다른곳에서 이용한후 버퍼 초기화
            ServerController.getInstance().NS.BeginRead(readBuffer, 0,
                READ_BUFFER_SIZE, new AsyncCallback(RHandlerCallbackfunc), null);

        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }
    }

    private void mainReceiver(byte[] input)
    {

        //HandlingMessage test = null;
        Console.WriteLine("mainReceiver");
        PreHandlingMessage PreHM = null;
        using (MemoryStream MS = new MemoryStream(input))
        {
            var SR = new StreamReader(MS);
            var tempstr = SR.ReadToEnd();
            Console.WriteLine(tempstr);

            PreHM = JsonMapper.ToObject<PreHandlingMessage>(tempstr);
        }


        //Console.WriteLine(Encoding.Default.GetString(input));

        //Console.WriteLine(PreHM.Mtype);
        //Console.WriteLine(PreHM.DataJSON);
        //처음에 DataJSON을 스트링으로 저장하고 Mtype에 따라 다른 작업을 수행

        switch (PreHM.Mtype)
        {
            case "JoinIDWrite":
                CLoginInfo LoginInfod = JsonMapper.ToObject<CLoginInfo>(PreHM.DataJSON);
                Console.WriteLine(LoginInfod.UserID);

                break;
            case "LoginInfoRead":
                if (PreHM.DataJSON == null)
                {
                    CLoginInfo LoginInfo = JsonMapper.ToObject<CLoginInfo>(PreHM.DataJSON);
                    Debug.Log("not joined");
                }
                else
                {
                    Debug.Log(PreHM.DataJSON);
                    CLoginInfo LoginInfo = JsonMapper.ToObject<CLoginInfo>(PreHM.DataJSON);
                
                }
                
                break;
                /*
            case "";
                var jss = new JavaScriptSerializer();
                                  = JsonMapper.ToObject<>(PreHM.DataJSON);

                break;

            case "";
                var jss = new JavaScriptSerializer();
                                 = JsonMapper.ToObject<>(PreHM.DataJSON);

                break;

            case "";
                var jss = new JavaScriptSerializer();
                                  = JsonMapper.ToObject<>(PreHM.DataJSON);

                break;

            case "";
                var jss = new JavaScriptSerializer();
                                  = JsonMapper.ToObject<>(PreHM.DataJSON);


                break;

            case "";
                var jss = new JavaScriptSerializer();
                                  = JsonMapper.ToObject<>(PreHM.DataJSON);


                break;

            case "";
                var jss = new JavaScriptSerializer();
                                 = JsonMapper.ToObject<>(PreHM.DataJSON);


                break;
                */

        }



        //접속 결과
        //서버로 부터 받은 게임정보 읽기
        //변경된 내용 저장 결과
        //종료시 정보 저장 결과


    }


    public int pre_parser(byte[] data, int ByteRead, byte[] handledbyte)
    {
        //byte 0 제거
        Console.WriteLine("pre_parser");

        int i = 0;
        while (true)
        {
            if (data[i] != 0)
            {
                handledbyte[i] = data[i];
                i++;
            }
            else if (data[i] == 0)
            {

                break;
            }
        }

        return i;
    }

    public void parser(byte[] data, int ByteRead)
    {
        Console.WriteLine("parser");
        //처리할 메시지인지(Mtype을 가지는 메시지)
        //바로 출력할 메시지인지(단순 string) 를 판단해서
        //구분이 필요


        //배열로 들어오는거에 대해 체크 
        //배열에 대한 체크가 특별히 필요하지 않으면 pre_parser랑 합치기
        byte[] handledbyte = new byte[ByteRead];
        int ind = pre_parser(data, ByteRead, handledbyte);
        receiver(handledbyte);
        //Debug.Log(Encoding.Default.GetString(handledbyte));
    }
    


}