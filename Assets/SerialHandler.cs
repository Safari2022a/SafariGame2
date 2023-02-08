using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;
using System.IO.Ports;
using UnityEngine;
using UniRx;

public class SerialHandler : MonoBehaviour {
    // [SerializeField] string portName = "/dev/tty.usbserial-95D2CE256D";
    [SerializeField] string portName = "/dev/tty.M5StickSafari";
    [SerializeField] int bauRate = 9600;

    SerialPort _serial;
    bool _isLoop = true;

    void Start () 
    {
        _serial = new SerialPort (portName, bauRate, Parity.None, 8, StopBits.One);

        try
        {
            _serial.Open();
            Scheduler.ThreadPool.Schedule (() => Read ()).AddTo(this);
            print($"Serial opened. Port name is {portName}");
        } 
        catch(Exception e)
        {
            Debug.Log ("can not open _serial port");
        }
    }

    void Update() {
        if (Time.frameCount % 120 == 0) {
            Write("Happy");
        } else if (Time.frameCount % 60 == 0) {
            Write("Hate");
        }
    }
	
    public void Read() {
        while (_isLoop)
        {
            string message = _serial.ReadLine();
            print(message);
        }
    }

    public void Write(string s) {
        if (_serial.IsOpen) {
            _serial.Write($"{s}\n");
            // print($"write: {s}");
        }
    }

    void OnDestroy()
    {
        _isLoop = false;
        _serial.Close ();
    }
}
