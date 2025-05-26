using System.Collections;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class SaveFile
{
    public int sizeX;
    public int sizeY;
    public List<List<Cell>> cells;
    public List<CardModel> cards;
    public TableModel tableModel;
    public CardDeckModel cardDeckModel;
    [Button]
    public void InitCountry()
    {
    }
    [Button]
    public void InitMap()
    {
        cells.Clear();
        for (int i = 0; i < sizeX; i++)
        {
            cells.Add(new List<Cell>());
            for(int j = 0; j < sizeY; j++)
            {
                cells[i].Add(new Cell());
                cells[i][j].pos = new Vector2Int(i, j);
            }
        }
        tableModel = new TableModel();
    }
}


[CreateAssetMenu(fileName = "newSaveData", menuName = "SaveData/newSaveData")]
public class SaveData : SerializedScriptableObject
{
    public ResConfig resConfig;
    public SaveFile saveFile;
    [Button]
    public void Save()
    {
        var filePath = "Assets/Resources/Saves" + "/tablesaveData.dat";
        Debug.Log(filePath);
        var json = SerializationUtility.SerializeValue(saveFile, DataFormat.JSON);
        File.WriteAllBytes(filePath, json);
        Debug.Log("Data Saved: " + json);
    }
    [Button]
    public void Load()
    {
        var filePath = "Assets/Resources/Saves" + "/tablesaveData.dat";
        var json = File.ReadAllBytes(filePath);
        saveFile = SerializationUtility.DeserializeValue<SaveFile>(json, DataFormat.JSON);
    }
    [Button]
    public void AddCard(CardAsset asset)
    {
        saveFile.cards.Add(asset.CreateCardModel());
    }
}
