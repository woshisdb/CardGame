using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITable
{
    GameObject GetTableAsset();

    void Bind(TableView tableModel);

}