using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Shop : MonoBehaviour
{
    public static Shop instance;

    [System.Serializable]
    public class ShopItem
    {
        public Sprite image;
        public string name;
        public float price;
    }

    [Header("General")]
    public GameObject itemTemplate;
    public GameObject item;
    public Transform shopScrollViewContent;
    public List<ShopItem> listShopItem;
    public TMP_Text txtPopupAdd;



    [Header("Detail Item")]
    public ScriptableObj_ItemData data;
    public TextMeshProUGUI txtNameDetail;
    public Image imageDetail;
    public TextMeshProUGUI txtPriceDetail;
    public TextMeshProUGUI txtMoTa;
    public float currentItemPrice;
    public Image colorImg;
    public TextMeshProUGUI txtKichThuoc;

    [Header("Button")]
    public Button btnShowDetail;
    public Button btnThemVaoGioHang;
    public Button btnXemAR;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    private void Start()
    {
        //btnThemVaoGioHang.AddEventListener(txtNameDetail.text, imageDetail.sprite, currentItemPrice, ButtonThemGioHang);
        btnThemVaoGioHang.onClick.AddListener(delegate { ButtonThemGioHang(txtNameDetail.text, imageDetail.sprite, currentItemPrice); });
        itemTemplate = shopScrollViewContent.GetChild(0).gameObject;
        txtPopupAdd.text = "+1 vào giỏ hàng";
        txtPopupAdd.gameObject.SetActive(false);
        btnXemAR.onClick.AddListener(XemAR);

        int len = listShopItem.Count;

        for (int i = 0; i < len; i++)
        {
            item = Instantiate(itemTemplate, shopScrollViewContent);
            item.transform.GetChild(0).GetComponent<Image>().sprite = listShopItem[i].image;
            item.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Giá: " + listShopItem[i].price.ToString().Insert((listShopItem[i].price.ToString().Length) - 3, ".") + " VNĐ";
            item.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = listShopItem[i].name;

            //listCurrentItem.Add(item.GetComponent<ShopItem>());

            //Xu ly button giong nhau cua nhieu item
            btnShowDetail = item.transform.GetChild(0).GetChild(2).GetComponent<Button>();
            btnShowDetail.AddEventListener(i, OpenScreenDetail);
        }

        Destroy(itemTemplate);
    }

    void OpenScreenDetail(int itemIndex)
    {
        AppController.instance.ChangeScreen(AppController.instance.screenDetail);
        txtNameDetail.text = listShopItem[itemIndex].name;
        imageDetail.sprite = listShopItem[itemIndex].image;
        currentItemPrice = listShopItem[itemIndex].price;
        txtPriceDetail.text = "Giá: " + listShopItem[itemIndex].price.ToString().Insert((listShopItem[itemIndex].price.ToString().Length) - 3, ".") + " VNĐ";
        txtMoTa.text = data.listMoTa[itemIndex];
        colorImg.color = data.listColor[itemIndex];
        txtKichThuoc.text = "Kich thuoc: " + data.listKichThuoc[itemIndex];

        //btnThemVaoGioHang.onClick.AddListener(delegate { ButtonThemGioHang(txtNameDetail.text, imageDetail.sprite, listShopItem[itemIndex].price); });

        //btnThemVaoGioHang = ControllerShop.instance.screenDetail.transform.GetChild(2).GetComponent<Button>();


    }

    public void PopupText()
    {
        txtPopupAdd.color = new Color(txtPopupAdd.color.r, txtPopupAdd.color.g, txtPopupAdd.color.b, 1);
        txtPopupAdd.transform.DOKill();
        txtPopupAdd.DOKill();
        txtPopupAdd.gameObject.SetActive(true);
        txtPopupAdd.transform.DOMoveY(txtPopupAdd.transform.position.y + 1, 1).SetEase(Ease.Linear);
        txtPopupAdd.DOFade(0, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            txtPopupAdd.gameObject.SetActive(false);
        });
    }

    void ButtonThemGioHang(string name, Sprite img, float price)
    {
        GioHang.instance.AddItem(name, img, price);
        txtPopupAdd.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        txtPopupAdd.transform.position = new Vector3(txtPopupAdd.transform.position.x, txtPopupAdd.transform.position.y + 0.2f, 0);
        PopupText();
    }

    public void XemAR()
    {
        if (txtNameDetail.text == "Table")
        {
            AppController.instance.currentModel = AppController.instance.listModelPrefab[0];
        }
        else if (txtNameDetail.text == "Chair")
        {
            AppController.instance.currentModel = AppController.instance.listModelPrefab[1];
        }
        else if (txtNameDetail.text == "Sofa")
        {
            AppController.instance.currentModel = AppController.instance.listModelPrefab[2];
        }
        else if (txtNameDetail.text == "BookCase")
        {
            AppController.instance.currentModel = AppController.instance.listModelPrefab[3];
        }
        else if (txtNameDetail.text == "CabinetTV")
        {
            AppController.instance.currentModel = AppController.instance.listModelPrefab[4];
        }
        ConnectScene.instance.UnActiveApp();

        AppController.instance.LoadScene("SampleScene");
    }


}
