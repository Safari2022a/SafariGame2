// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;
// using UnityEngine.InputSystem.Layouts;

// public class KeyBoadTest : MonoBehaviour
// {
//     void Start()
//     {
//         // Add a device such that matching process is employed:
//         InputSystem.AddDevice(new InputDeviceDescription
//         {
//             interfaceName = "Karabiner",
//             deviceClass = "Keyboard",
//             product = "USB Keyboard",
//             capabilities = @"
//                 {
//                     ""vendorId"" : 0x1C4F,
//                     ""productId"" : 0x15
//                 }
//             "
//         });

//         print(InputSystem.devices.Count);
//         foreach(var device in InputSystem.devices) {
//             print("deviceDesc:");
//             print(device);
//             print(device.description);
//             print(device.description.interfaceName);
//             print(device.description.product);
//             print(device.description.deviceClass);
//             print(device.description.serial);
//             print("---");
//         }
//         // InputSystem.onDeviceChange +=
//         // (device, change) =>
//         // {
//         //     switch (change)
//         //     {
//         //         case InputDeviceChange.Added:
//         //             Debug.Log($"Device {device} was added");
//         //             break;
//         //         case InputDeviceChange.Removed:
//         //             Debug.Log($"Device {device} was removed");
//         //             break;
//         //     }
//         // };

//         print(Keyboard.current.description.interfaceName);
//         print(Keyboard.current.description.deviceClass);

//     }

//     void OnEnable() {
//         // Keyboard.current.onTextInput += (char c) => {
//         //     print($"input: {c}");
//         // };
//     }

//     void Update()
//     {
//         // if (Keyboard.current.aKey.wasPressedThisFrame) {
//         //     print(InputSystem.devices.Count);
//         // }
        
//         // Keyboard t = (Keyboard)InputSystem.devices[1];
//         // if (t.kKey.wasPressedThisFrame) {
//         //     print("k!!!");
//         //     print(InputSystem.devices.Count);
//         // }
//         for (int i = 0; i < InputSystem.devices.Count; i++) {
//             Keyboard kb = (Keyboard)InputSystem.devices[i];
//             if (kb.aKey.wasPressedThisFrame) {
//                 // print("a");
//                 print(i);
//                 // print(kb.description.product);
//                 // print(kb.description.interfaceName);
//             }
//         }
//     }
// }
