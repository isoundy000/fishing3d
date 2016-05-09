using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIAsset
{
    public Record_UI uiRecord;
    public GameObject viewObject;
    public ViewBase view;
}

public class UIManager 
{
    private static UIManager mInstance = null;

    private Transform mWeaponRoot = null;
    public Transform WeaponRoot
    {
        get { return mWeaponRoot; }
        set { mWeaponRoot = value; }
    }

    private Dictionary<string, UIAsset> mUIMap = new Dictionary<string, UIAsset>();
    private UIManager()
    { }

    public static UIManager GetInstance()
    {
        if (mInstance == null)
            mInstance = new UIManager();

        return mInstance;
    }

    public void Initialize()
    {
        Table_UI uitable = GameTableManager.GetInstance().GetTable("table_ui") as Table_UI;
        List<Record> recordsList = uitable.recordsList;
        foreach(Record record in recordsList)
        {
            UIAsset asset = new UIAsset();
            asset.uiRecord = record as Record_UI;
            mUIMap.Add(asset.uiRecord.name, asset);
        }

        mWeaponRoot = GameObject.FindWithTag("WeaponRoot").transform;
    }

    public void Update(float dt)
    {
    }

    public void Show(string viewName)
    { 

    }

    public void Hide(string viewName)
    {
 
    }

    public UIAsset GetUIAsset(string name)
    {
        UIAsset asset = null;
        mUIMap.TryGetValue(name, out asset);
        return asset;
    }
}
