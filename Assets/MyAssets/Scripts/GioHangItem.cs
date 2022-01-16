using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GioHangItem : MonoBehaviour
{
    public string nameItem;
    public Image imageItem;
    public float price;
    public int soLuong;

    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txtPrice;
    public TextMeshProUGUI txtSoLong;
    public Button btnSub, btnCross, btnDelete;


    private void Start()
    {
        SetTextPrice();
        SetTextSoLuong();
        SetTextName();
        btnCross.onClick.AddListener(ButtonCross);
        btnSub.onClick.AddListener(ButtonSub);
        btnDelete.onClick.AddListener(ButtonDelete);
    }

    public void SetTextPrice()
    {
        txtPrice.text = "Giá: " + price.ToString().Insert((price.ToString().Length) - 3, ".") + " VNĐ"; 
    }

    public void SetTextSoLuong()
    {
        txtSoLong.text = "So luong: x" + soLuong.ToString();
    }

    public void SetTextName()
    {
        txtName.text = nameItem.ToString();
    }

    public void ButtonCross()
    {
        if(soLuong == 0)
        {
            btnSub.interactable = true;
        }
        soLuong++;
        SetTextSoLuong();
        GioHang.instance.ChangeTongTien(price);
    }
    public void ButtonSub()
    {
        if (soLuong > 0)
        {
            soLuong--;
            SetTextSoLuong();
            GioHang.instance.ChangeTongTien(-1.0f * price);
            if (soLuong == 0)
            {
                btnSub.interactable = false;
            }
        }
    }
    public void ButtonDelete()
    {
        GioHang.instance.listCurrentItemInGioHang.Remove(this);
        Destroy(gameObject);
        GioHang.instance.ChangeTongTien(-1.0f * price * soLuong);
    }
}
