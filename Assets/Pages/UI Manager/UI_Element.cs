using UnityEngine;

public class UI_Element : EventEmitterComponent
{
    /// <summary>
    /// Called when the page is initialized
    /// </summary>
    public virtual void init() { 
    this.emit("init");
    }

    /// <summary>
    /// Called when the page is opened
    /// </summary>
    public virtual void onOpen() {
     this.emit("onOpen");
    }

    /// <summary>
    /// Called when the page is closed
    /// </summary>
    public virtual void onClose() {
    this.emit("onClose");
    }
    
}