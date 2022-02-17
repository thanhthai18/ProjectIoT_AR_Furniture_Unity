using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DonHangItem : MonoBehaviour
{
    public TMP_Text txtId;
    public TMP_Text txtDate;
    public TMP_Text txtName;
    public TMP_Text txtSdt;
    public TMP_Text txtDiaChi;
    public TMP_Text txtPhuongThuc;
    public TMP_Text txtListSanPham;
    public TMP_Text txtThanhTien;
    public TMP_Dropdown dropTrangThai;
    public string trangThai;

    private void Start()
    {
        trangThai = dropTrangThai.options[dropTrangThai.value].text;
        dropTrangThai.onValueChanged.AddListener(delegate { ChangeTrangThai(dropTrangThai); });
    }

    public void ChangeTrangThai(TMP_Dropdown dropdown)
    {
        trangThai = dropdown.options[dropdown.value].text;
        RealtimeDB.instance.SetTrangThai(trangThai, txtId.text);
    }
}
