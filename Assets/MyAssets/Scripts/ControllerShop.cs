using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControllerShop : MonoBehaviour
{
    public static ControllerShop instance;

    public Button btnCloseShop, btnBack;

    public GameObject screenShop, screenDetail;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        btnCloseShop.onClick.AddListener(CloseScreenShop);
        btnBack.onClick.AddListener(BackToScreenShop);
    }

    void CloseScreenShop()
    {
        screenShop.SetActive(false);
    }

    void BackToScreenShop()
    {
        screenDetail.SetActive(false);
        screenShop.SetActive(true);
    }

}
