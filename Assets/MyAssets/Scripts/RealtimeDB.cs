using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using TMPro;
using System;

public class RealtimeDB : MonoBehaviour
{
    public static RealtimeDB instance;
    public DatabaseReference reference;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        Invoke(nameof(DelayEnableFirebaseDatabase), 0.1f);

    }

    public void DelayEnableFirebaseDatabase()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;

    }

    public void SaveData()
    {
        DonHang donhang = new DonHang();
        reference.Child("DonHang").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                donhang.id = (snapshot.ChildrenCount).ToString();

                donhang.name = AppController.instance.tenNguoiMua.text;
                donhang.sdt = AppController.instance.soDienThoai.text;
                donhang.diaChi = AppController.instance.diaChi.text;
                donhang.listSanPham = AppController.instance.txtListSanPhamDat.text;
                donhang.thanhTien = AppController.instance.txtThanhTien.text;
                donhang.phuongThuc = AppController.instance.txtPhuongThucThanhToan.options[0].text;
                donhang.date = DateTime.Now.ToString();
                donhang.trangThai = "Chờ duyệt";
                string json = JsonUtility.ToJson(donhang);

                reference.Child("DonHang").Child(donhang.id).SetRawJsonValueAsync(json).ContinueWith(task =>
                {
                    if (task.IsCompleted)
                    {
                        Debug.Log("thanh cong");
                    }
                    else
                    {
                        Debug.Log("that bai");
                    }
                });
            }
        });
    }

    public void LoadData()
    {
        reference.Child("DonHang").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.ChildrenCount > 1)
                {
                    ListDonHang.instance.countDonHang = snapshot.ChildrenCount - 1;
                    for (int i = 1; i < snapshot.ChildrenCount; i++)
                    {
                        ListDonHang.instance.date_List.Add(snapshot.Child(i.ToString()).Child("date").GetValue(true).ToString());
                        ListDonHang.instance.diaChi_List.Add(snapshot.Child(i.ToString()).Child("diaChi").GetValue(true).ToString());
                        ListDonHang.instance.id_List.Add(snapshot.Child(i.ToString()).Child("id").GetValue(true).ToString());
                        ListDonHang.instance.listSanPham_List.Add(snapshot.Child(i.ToString()).Child("listSanPham").GetValue(true).ToString());
                        ListDonHang.instance.name_List.Add(snapshot.Child(i.ToString()).Child("name").GetValue(true).ToString());
                        ListDonHang.instance.phuongThuc_List.Add(snapshot.Child(i.ToString()).Child("phuongThuc").GetValue(true).ToString());
                        ListDonHang.instance.sdt_List.Add(snapshot.Child(i.ToString()).Child("sdt").GetValue(true).ToString());
                        ListDonHang.instance.thanhTien_List.Add(snapshot.Child(i.ToString()).Child("thanhTien").GetValue(true).ToString());
                        ListDonHang.instance.trangThai_List.Add(snapshot.Child(i.ToString()).Child("trangThai").GetValue(true).ToString());
                    }
                }

            }
        });
    }

    

    public void SetTrangThai(string trangthai, string id)
    {
        reference.Child("DonHang").Child(id).Child("trangThai").SetValueAsync(trangthai).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("thay doi trang thai");
            }
            else
            {
                Debug.Log("that bai thay doi trang thai");
            }
        });
    }

}

