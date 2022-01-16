using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControllerShop : MonoBehaviour
{
    public static ControllerShop instance;

    public Button btnCloseShop, btnBack;

    public GameObject screenShop, screenDetail, screenGioHang, screenDatHang;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        screenShop.SetActive(true);
        screenDetail.SetActive(false);
        screenGioHang.SetActive(false);

        btnCloseShop.onClick.AddListener(CloseScreenShop);
        btnBack.onClick.AddListener(BackToScreenShop);
    }

    void CloseScreenShop()
    {
        screenShop.SetActive(false);
        screenGioHang.SetActive(true);
    }

    void BackToScreenShop()
    {
        screenDetail.SetActive(false);
        screenShop.SetActive(true);
    }

}
