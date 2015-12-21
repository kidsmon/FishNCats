using UnityEngine;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;

public class RequestMessage
{
    public string Mtype { get; set; }
    public string FacebookID { get; set; }
}

public class LoginReqMessage
{
    public string Mtype { get; set; }
    public string FacebookID { get; set; }
    public string UserID { get; set; }
}