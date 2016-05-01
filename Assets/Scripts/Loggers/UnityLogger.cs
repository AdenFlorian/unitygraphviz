using System;
using UnityEngine;

public class UnityLogger : Logger {
    public void Log(string message) {
        Debug.Log(message);
    }
}
