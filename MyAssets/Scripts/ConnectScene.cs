using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;

public class ConnectScene : MonoBehaviour
{
    public static ConnectScene instance;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        if(FindObjectsOfType<ConnectScene>().Length > 1)
        {
            Destroy(gameObject);
        }

        if(instance == null)
        {
            instance = this;
        }
    }

    public void ActiveApp()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void UnActiveApp()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

   
}
