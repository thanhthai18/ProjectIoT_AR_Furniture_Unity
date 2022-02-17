using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AppController : MonoBehaviour
{
    public static AppController instance;

    public GameObject currentScreen;
    public Camera mainCamera;

    public GameObject screenGenaral, screenLogin, screenListDonHang, screenShop, screenGioHang, screenDetail, screenThongTin;


    public Button btnBack_Login, btnLogout, btnBack_GioHang, btnBackDetail, btnBack_Shop, btnBack_ThongTin;
    public Button btnShop, btnGioHang, btnAdmin;
    public Button btnDatHang, btnHuyDonHang; // GioHang

    public TMP_Text txtListSanPhamDat, txtThanhTien;
    public TMP_InputField tenNguoiMua, soDienThoai, diaChi;
    public TMP_Dropdown txtPhuongThucThanhToan;
    public Button btnDAT;
    public Button btnDone;

    public GameObject currentModel;
    public List<GameObject> listModelPrefab = new List<GameObject>();




    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    private void Start()
    {
        SetSizeCamera();
        btnDAT.interactable = false;
        btnAdmin.onClick.AddListener(AdminLogin);
        btnShop.onClick.AddListener(OpenShop);
        btnGioHang.onClick.AddListener(OpenGioHang);
        btnDatHang.onClick.AddListener(OpenThongTin);
        btnHuyDonHang.onClick.AddListener(HuyDonHang);
        btnDone.onClick.AddListener(DatHangThanhCong);

        btnBack_Login.onClick.AddListener(BackToScreenGeneral);
        btnLogout.onClick.AddListener(Logout);
        btnBack_GioHang.onClick.AddListener(BackToScreenGeneral);
        btnBack_Shop.onClick.AddListener(BackToScreenGeneral);
        btnBack_ThongTin.onClick.AddListener(BackToScreenGioHang);
        btnBackDetail.onClick.AddListener(BackToScreenShop);


        screenGenaral.SetActive(true);
        screenLogin.SetActive(true);
        screenListDonHang.SetActive(true);
        screenShop.SetActive(true);
        screenGioHang.SetActive(true);
        screenDetail.SetActive(true);
        screenThongTin.SetActive(true);

        screenLogin.SetActive(false);
        screenListDonHang.SetActive(false);
        screenShop.SetActive(false);
        screenGioHang.SetActive(false);
        screenDetail.SetActive(false);
        screenThongTin.SetActive(false);

        currentScreen = screenGenaral;
    }

    void SetSizeCamera()
    {
        float f1, f2;
        f1 = 9.0f / 19.5f;
        f2 = Screen.width * 1.0f / Screen.height;
        mainCamera.orthographicSize *= f1 / f2;
    }



    public void BackToScreenGeneral()
    {
        ChangeScreen(screenGenaral);
    }

    public void ChangeScreen(GameObject screen)
    {
        if (currentScreen != null)
        {
            currentScreen.SetActive(false);
        }
        screen.SetActive(true);
        currentScreen = screen;
    }

    public void AdminLogin()
    {
        ChangeScreen(screenLogin);
    }

    public void Logout()
    {
        AuthManager.instance.emailloginField.text = "";
        AuthManager.instance.passwordLoginfield.text = "";

        if (ListDonHang.instance.dropLoc.captionText.text != "All")
        {
            Debug.Log(ListDonHang.instance.dropLoc.captionText.text);
            var value1 = ListDonHang.instance.dropLoc.options.FindIndex(option => option.text == "All");
            var value2 = ListDonHang.instance.dropLoc.options.FindIndex(option => option.text == ListDonHang.instance.dropLoc.captionText.text);
            var tmpDrop = ListDonHang.instance.dropLoc.options[value1];
            ListDonHang.instance.dropLoc.options[value1] = ListDonHang.instance.dropLoc.options[value2];
            ListDonHang.instance.dropLoc.options[value2] = tmpDrop;
        }
        ListDonHang.instance.dropLoc.RefreshShownValue();

        ListDonHang.instance.listCurrentItemInDonHang.ForEach(s => Destroy(s.gameObject));
        ListDonHang.instance.listCurrentItemInDonHang.Clear();
        ListDonHang.instance.date_List.Clear();
        ListDonHang.instance.diaChi_List.Clear();
        ListDonHang.instance.id_List.Clear();
        ListDonHang.instance.listSanPham_List.Clear();
        ListDonHang.instance.name_List.Clear();
        ListDonHang.instance.phuongThuc_List.Clear();
        ListDonHang.instance.sdt_List.Clear();
        ListDonHang.instance.thanhTien_List.Clear();
        ListDonHang.instance.trangThai_List.Clear();
        ListDonHang.instance.countDonHang = 0;
        ChangeScreen(screenGenaral);
    }

    public void OpenShop()
    {
        ChangeScreen(screenShop);
    }

    public void OpenGioHang()
    {
        ChangeScreen(screenGioHang);
    }

    public void BackToScreenGioHang()
    {
        ChangeScreen(screenGioHang);
    }

    public void OpenThongTin()
    {
        txtListSanPhamDat.text = "";
        txtThanhTien.text = "";
        txtThanhTien.text = GioHang.instance.txtTongTien.text;
        for (int i = 0; i < GioHang.instance.listCurrentItemInGioHang.Count; i++)
        {
            if (GioHang.instance.listCurrentItemInGioHang[i].soLuong != 0)
            {
                txtListSanPhamDat.text += GioHang.instance.listCurrentItemInGioHang[i].txtName.text + "x" + GioHang.instance.listCurrentItemInGioHang[i].soLuong;
                if (i != GioHang.instance.listCurrentItemInGioHang.Count - 1)
                {
                    txtListSanPhamDat.text += ", ";
                }
            }
        }

        ChangeScreen(screenThongTin);

    }

    public void HuyDonHang()
    {
        GioHang.instance.HuyDonHang();
    }

    public void BackToScreenShop()
    {
        ChangeScreen(screenShop);
    }

    public void DatHangThanhCong()
    {
        GioHang.instance.listCurrentItemInGioHang.ForEach(s => Destroy(s.gameObject));
        GioHang.instance.listCurrentItemInGioHang.Clear();
        GioHang.instance.txtTongTien.text = "Tổng tiền: 0 VNĐ";
        btnHuyDonHang.interactable = false;
        btnDatHang.interactable = false;
        ChangeScreen(screenGenaral);
    }


    public void LoadScene(string scenceName)
    {
        SceneManager.LoadScene(scenceName);
    }


    private void Update()
    {
        if (screenThongTin.activeSelf)
        {
            if (tenNguoiMua.text == "" || soDienThoai.text == "" || soDienThoai.text.Length < 10 || soDienThoai.text.Length > 11 || diaChi.text == "")
            {
                if (btnDAT.interactable)
                {
                    btnDAT.interactable = false;
                }
            }
            else if (!btnDAT.interactable)
            {
                btnDAT.interactable = true;
            }
        }
    }



}
