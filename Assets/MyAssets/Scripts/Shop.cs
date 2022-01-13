using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [System.Serializable]
    public class ShopItem
    {
        public Sprite image;
        public string name;
        public int price;
    }

    [Header("General")]
    public GameObject itemTemplate;
    public GameObject item;
    public Transform shopScrollViewContent;
    public List<ShopItem> listShopItem;


    [Header("Detail Item")]
    public ScriptableObj_ItemData data;
    public TextMeshProUGUI txtNameDetail;
    public Image imageDetail;
    public TextMeshProUGUI txtPriceDetail;
    public TextMeshProUGUI txtMoTa;
    public Image colorImg;
    public TextMeshProUGUI txtKichThuoc;

    [Header("Button")]
    public Button btnShowDetail;



    private void Start()
    {

        itemTemplate = shopScrollViewContent.GetChild(0).gameObject;

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
        ControllerShop.instance.screenDetail.SetActive(true);
        ControllerShop.instance.screenShop.SetActive(false);
        txtNameDetail.text = listShopItem[itemIndex].name;
        imageDetail.sprite = listShopItem[itemIndex].image;
        txtPriceDetail.text = "Giá: " + listShopItem[itemIndex].price.ToString().Insert((listShopItem[itemIndex].price.ToString().Length) - 3, ".") + " VNĐ";
        txtMoTa.text = data.listMoTa[itemIndex];
        colorImg.color = data.listColor[itemIndex];
        txtKichThuoc.text = "Kich thuoc: " + data.listKichThuoc[itemIndex];

    }


}



