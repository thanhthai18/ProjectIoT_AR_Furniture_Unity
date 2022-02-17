using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GioHang : MonoBehaviour
{
    public static GioHang instance;

    public GioHangItem gioHangItemPrefab;
    public Transform gioHangScrollViewContent;
    public List<GioHangItem> listCurrentItemInGioHang = new List<GioHangItem>();
    public int indexCheckName;
    public TextMeshProUGUI txtTongTien;
    private float tongTien;



    public float TongTien
    {
        get => tongTien;
        set
        {
            if (tongTien >= 0)
            {
                tongTien = value;
                if (AppController.instance.btnDatHang.interactable == false)
                {
                    AppController.instance.btnDatHang.interactable = true;
                }
                else if (tongTien == 0)
                {
                    AppController.instance.btnDatHang.interactable = false;
                }
            }
            else
            {
                tongTien = 0;
            }
            SetTextTongTien();
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (listCurrentItemInGioHang.Count == 0)
        {
            AppController.instance.btnHuyDonHang.interactable = false;
            AppController.instance.btnDatHang.interactable = false;
        }
    }

    public void HuyDonHang()
    {
        for (int i = 0; i < listCurrentItemInGioHang.Count; i++)
        {
            Destroy(listCurrentItemInGioHang[i].gameObject);
        }
        TongTien = 0;
        listCurrentItemInGioHang.Clear();
        AppController.instance.btnHuyDonHang.interactable = false;
    }



    public void ChangeTongTien(float value)
    {
        TongTien += value;
    }

    public void SetTextTongTien()
    {
        if (TongTien.ToString().Length > 3)
        {
            txtTongTien.text = "Tong tien: " + TongTien.ToString().Insert((TongTien.ToString().Length) - 3, ".") + " VNĐ";
        }
        else
        {
            txtTongTien.text = "Tong tien: " + TongTien.ToString() + " VNĐ";
        }
    }


    public void AddItem(string name, Sprite img, float price)
    {
        if (!AppController.instance.btnHuyDonHang.interactable)
        {
            AppController.instance.btnHuyDonHang.interactable = true;
        }
        TongTien += price;
        if (listCurrentItemInGioHang.Count == 0)
        {
            var tmpItem = Instantiate(gioHangItemPrefab, gioHangScrollViewContent);
            tmpItem.nameItem = name;
            tmpItem.imageItem.sprite = img;
            tmpItem.price = price;
            tmpItem.soLuong++;
            tmpItem.SetTextSoLuong();
            listCurrentItemInGioHang.Add(tmpItem);
            Debug.Log("Them moi chua co gi");
        }
        else
        {
            indexCheckName = 0;
            for (int i = 0; i < listCurrentItemInGioHang.Count; i++)
            {
                if (listCurrentItemInGioHang[i].nameItem == name)
                {
                    listCurrentItemInGioHang[i].soLuong++;
                    listCurrentItemInGioHang[i].SetTextSoLuong();
                    Debug.Log("Them so luong");
                    indexCheckName--;
                }
            }
            if (indexCheckName != 0)
            {
                return;
            }
            else
            {
                var tmpItem = Instantiate(gioHangItemPrefab, gioHangScrollViewContent);
                tmpItem.nameItem = name;
                tmpItem.imageItem.sprite = img;
                tmpItem.price = price;
                tmpItem.soLuong++;
                tmpItem.SetTextSoLuong();
                listCurrentItemInGioHang.Add(tmpItem);
                Debug.Log("Them moi da co gi");
            }
        }
    }





}
