using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Net.Sockets;
using WebSocketSharp;

public class SocketClient : MonoBehaviour
{
    WebSocket _webSocket;
    Queue<Action> _actions;

    void Start()
    {
        _actions = new Queue<Action>();
        // websocket
        _webSocket = new WebSocket($"ws://{Settings.Host}:{Settings.Port}/");
        _webSocket.OnOpen += (sender, e) => Debug.Log("WebSocket Open");
        _webSocket.OnError += (sender, e) => Debug.Log("WebSocket Error Message: " + e.Message);
        _webSocket.OnClose += (sender, e) => Debug.Log("WebSocket Close");
        
        _webSocket.OnMessage += (sender, e) => {
            print("Message Recieved");
            print(e.Data);
            SocketData data = JsonUtility.FromJson<SocketData>(e.Data);
            if (data.type.Equals("CreateUser")) {
                DataCreateUser dcu = JsonUtility.FromJson<DataCreateUser>(data.content);
                _actions.Enqueue(() => {
                    GameObject.FindWithTag("GameController").GetComponent<GameController>().CreateOtherUser(dcu.userID);
                });
            }
        };
        _webSocket.Connect();
    }

    void Update()
    {
        // if (Input.GetKeyUp("s"))
        // {
        //     Debug.Log("send!!");
        //     _webSocket.Send("TestMessage???");
        // }

        if (_actions.Count > 0) {
            print("cnt > 0");
            _actions.Dequeue()();
        }
    }

    private void OnDestroy()
    {
        print("destroy");
        _webSocket.Close();
        _webSocket = null;
    }

    public void Send(string data) {
        print(_webSocket);
        _webSocket.Send(data);
    }
}
