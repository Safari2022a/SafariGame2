// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System;
// using System.Net.Sockets;
// using System.Text;


// public class SocketTest : MonoBehaviour
// {
//     public string m_ipAddress = "127.0.0.1";
//     public int    m_port      = 6000;
    
//     private TcpClient     m_tcpClient;
//     private NetworkStream m_networkStream;
//     private bool          m_isConnection;

//     private string m_message = "connectionTest";

//     void Start()
//     {
//         try
//         {
//             // 指定された IP アドレスとポートでサーバに接続します
//             m_tcpClient     = new TcpClient( m_ipAddress, m_port );
//             m_networkStream = m_tcpClient.GetStream();
//             m_isConnection  = true;

//             Debug.LogFormat( "接続成功" );
//         }
//         catch ( SocketException )
//         {
//             // サーバが起動しておらず接続に失敗した場合はここに来ます
//             Debug.LogError( "接続失敗" );
//         }
//     }


//     void Update()
//     {
//         if (Time.frameCount % 60 == 0) {
//              try
//             {
//                 // サーバに文字列を送信します
//                 var buffer = Encoding.UTF8.GetBytes( m_message );
//                 m_networkStream.Write( buffer, 0, buffer.Length );

//                 Debug.LogFormat( "送信成功：{0}", m_message );
//             }
//             catch ( Exception )
//             {
//                 // サーバが起動しておらず送信に失敗した場合はここに来ます
//                 // SocketException 型だと例外のキャッチができないようなので
//                 // Exception 型で例外をキャッチしています
//                 Debug.LogError( "送信失敗" );
//             }
//         }
//     }
// }
