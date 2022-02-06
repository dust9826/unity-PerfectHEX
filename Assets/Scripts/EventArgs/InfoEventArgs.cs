using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoEventArgs<T> : EventArgs
{
    public T info;

    public InfoEventArgs()
    {
        info = default(T);
        Debug.Log(info.ToString());
    }

    public InfoEventArgs(T info)
    {
        this.info = info;
    }
}
