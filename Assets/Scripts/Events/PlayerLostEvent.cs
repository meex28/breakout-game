using System;

public class PlayerLostEvent : EventArgs {
    public string message;
    
    public PlayerLostEvent(string msg) {
        message = msg;
    }
}
