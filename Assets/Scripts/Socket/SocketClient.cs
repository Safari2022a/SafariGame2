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
    GameObject _gameController;

    void Start()
    {
        _actions = new Queue<Action>();
        _gameController = GameObject.FindWithTag("GameController");
        // websocket
        _webSocket = new WebSocket($"ws://{Settings.Host}:{Settings.Port}/");
        _webSocket.OnOpen += (sender, e) => Debug.Log("WebSocket Open");
        _webSocket.OnError += (sender, e) => Debug.Log("WebSocket Error Message: " + e.Message);
        _webSocket.OnClose += (sender, e) => Debug.Log("WebSocket Close");
        
        _webSocket.OnMessage += (sender, e) => {
            // print("Message Recieved");
            SocketData data = JsonUtility.FromJson<SocketData>(e.Data);
            if (data.type.Equals("UpdT")) {
                _actions.Enqueue(() => {
                    DataTransform dt = JsonUtility.FromJson<DataTransform>(data.content);
                    _gameController.GetComponent<GameController>().UpdateOtherUserT(dt.userID, dt.transformAry);
                });
            } else if (data.type.Equals("CreateUser")) {
                _actions.Enqueue(() => {
                    DataCreateUser dcu = JsonUtility.FromJson<DataCreateUser>(data.content);
                    _gameController.GetComponent<GameController>().CreateOtherUser(dcu.userID);
                });
            } else if (data.type.Equals("ActiveUsers")) {
                _actions.Enqueue(() => {
                    DataActiveUsers dau = JsonUtility.FromJson<DataActiveUsers>(data.content);
                    _gameController.GetComponent<GameController>().UserID = dau.userID;
                    foreach (DataCreateUser dcu in dau.activeUsers) {
                        _gameController.GetComponent<GameController>().CreateOtherUser(dcu.userID);
                    }

                    DataOKUser dou = new DataOKUser(dau.userID);
                    SocketData _data = new SocketData("OKUser", JsonUtility.ToJson(dou));
                    _webSocket.Send(JsonUtility.ToJson(_data));
                });
            } else if (data.type.Equals("RemoveUser")) {
                _actions.Enqueue(() => {
                    DataRemoveUser dru = JsonUtility.FromJson<DataRemoveUser>(data.content);
                    _gameController.GetComponent<GameController>().RemoveOtherUser(dru.userID);
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
            _actions.Dequeue()();
        }
    }

    private void OnDestroy()
    {
        print("destroy");
        // DataRemoveUser dru = new DataRemoveUser()
        // _webSocket.Send();
        _webSocket.Close();
        _webSocket = null;
    }

    public void Send(string data) {
        _webSocket.Send(data);
    }
}
