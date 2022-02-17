using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ItemData/MotaVaModel")]
public class ScriptableObj_ItemData : ScriptableObject
{
    public List<string> listMoTa;
    public List<GameObject> listModel;
    public List<Color> listColor;
    public List<string> listKichThuoc;
}
