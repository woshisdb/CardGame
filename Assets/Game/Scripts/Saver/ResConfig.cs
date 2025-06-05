using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "newResConfig", menuName = "Config/newResConfig")]
public class ResConfig : SerializedScriptableObject
{
    public GameObject cell;
    public List<CardAsset> cardAssets;
    public Dictionary<CardEnum,CardUIView> cardUITemplate;
    public Dictionary<CardEnum, CardScView> cardSCTemplate;
    public GameObject slotView;
    public Dictionary<TableEnum,GameObject> tableAssets;
    public Dictionary<DialoguePortraitEnum, Sprite> DialoguePortraits;
    public GameObject FindTableObject(TableEnum name)
    {
        if(tableAssets.ContainsKey(name))
        {
            return tableAssets[name];
        }
        return null;
    }
}

