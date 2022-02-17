using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ListDonHang : MonoBehaviour
{
    public static ListDonHang instance;

    public DonHangItem donHangItemPrefab;
    public TMP_Dropdown dropLoc;
    public Transform donHangScrollViewContent;
    public List<DonHangItem> listCurrentItemInDonHang = new List<DonHangItem>();
    public List<string> date_List, diaChi_List, id_List, listSanPham_List, name_List, phuongThuc_List, sdt_List, thanhTien_List, trangThai_List;
    public long countDonHang;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        countDonHang = 0;
        Loc(dropLoc);
        dropLoc.onValueChanged.AddListener(delegate { Loc(dropLoc); });
    }

    public void Loc(TMP_Dropdown dropdown)
    {
        listCurrentItemInDonHang.ForEach(s =>
        {
            if (s.dropTrangThai.options[s.dropTrangThai.value].text == dropdown.options[dropdown.value].text)
            {
                s.gameObject.SetActive(true);
            }
            else if (dropdown.options[dropdown.value].text == "All")
            {
                listCurrentItemInDonHang.ForEach(a => a.gameObject.SetActive(true));
            }
            else
            {
                s.gameObject.SetActive(false);
            }
        });
    }

    public void SpawnDonHangItem()
    {
        for (int i = 0; i < countDonHang; i++)
        {
            var tmpDonHangItem = Instantiate(donHangItemPrefab, donHangScrollViewContent);
            tmpDonHangItem.txtDate.text = date_List[i];
            tmpDonHangItem.txtDiaChi.text = diaChi_List[i];
            tmpDonHangItem.txtId.text = id_List[i];
            tmpDonHangItem.txtListSanPham.text = listSanPham_List[i];
            tmpDonHangItem.txtName.text = name_List[i];
            //tmpDonHangItem.txtPhuongThuc.text = phuongThuc_List[i];
            tmpDonHangItem.txtPhuongThuc.text = AppController.instance.txtPhuongThucThanhToan.options[0].text;
            tmpDonHangItem.txtSdt.text = sdt_List[i];
            tmpDonHangItem.txtThanhTien.text = thanhTien_List[i];
            tmpDonHangItem.trangThai = trangThai_List[i];

            var value = tmpDonHangItem.dropTrangThai.options.FindIndex(option => option.text == tmpDonHangItem.trangThai);
            var tmpOption = tmpDonHangItem.dropTrangThai.options[0];
            tmpDonHangItem.dropTrangThai.options[0] = tmpDonHangItem.dropTrangThai.options[value];
            tmpDonHangItem.dropTrangThai.options[value] = tmpOption;

            listCurrentItemInDonHang.Add(tmpDonHangItem);
        }
    }
    public void DelayLoadData_Spawn()
    {
        Invoke(nameof(SpawnDonHangItem), 0.5f);
    }
}
