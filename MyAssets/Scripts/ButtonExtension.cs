using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public static class ButtonExtension
{
    public static void AddEventListener<T> (this Button button, T param, Action<T> OnClick)
    {
        button.onClick.AddListener(delegate ()
        {
            OnClick(param);
        });
    }

    public static void AddEventListener<T1, T2, T3>(this Button button, T1 param, T2 param2, T3 param3, Action<T1, T2, T3> OnClick)
    {
        button.onClick.AddListener(delegate ()
        {
            OnClick(param, param2, param3);
        });
    }

}
