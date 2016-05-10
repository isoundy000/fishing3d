using UnityEngine;
using System.Collections;

public class FishManager
{
    private static FishManager mInstance = null;
    private FishManager()
    { }

    private float timer = 0;

    private Table_Fish mFishTable;

    public static FishManager GetInstance()
    {
        if (mInstance == null)
            mInstance = new FishManager();

        return mInstance;
    }

    public void Initialize()
    {
        mFishTable = GameTableManager.GetInstance().GetTable("table_fish") as Table_Fish;
    }

    public void Update(float dt)
    {
        if (Input.touchCount > 0)
        {
            Debug.Log(Input.GetTouch(0).phase);
        }
    }

    public void CreateFish()
    {
        float hBound = 120;
        float yBottom = -70;
        float yUp = 70;
        int[] temp = new int[2] { -1, 1 };
        int flag = temp[Random.Range(0, 2)];

        Record_Fish record = mFishTable.GetRecord(Random.Range(0, 2)) as Record_Fish;
        GameObject fish = GameObject.Instantiate(ResourcesManager.GetInstance().LoadLocalAsset("FishPrefabs/" + record.prefabName) as GameObject);
        fish.transform.localPosition = new Vector3(hBound * flag, Random.Range(yBottom + 20, yUp - 20), Random.Range(96,96+20));
        fish.transform.eulerAngles = new Vector3(0, -90*flag, 0);
        Fish fishcom = fish.AddComponent<Fish>();
        fishcom.Speed = 30.0f * Random.Range(1,2);
        fishcom.FishPathData = PathConfigManager.GetInstance().GetPath(Random.Range(0,5));
        fishcom.FishPathData.renderPath = false;
    }

    public void CreateFish(int fishid, Vector3 position, Vector3 eulerangle, int pathid, float speed, float unactiveTime)
    {
        Record_Fish record = mFishTable.GetRecord(fishid) as Record_Fish;
        GameObject fish = GameObject.Instantiate(ResourcesManager.GetInstance().LoadLocalAsset("FishPrefabs/" + record.prefabName) as GameObject);
        fish.transform.localPosition = position;
        fish.transform.eulerAngles = eulerangle;
        Fish fishcom = fish.AddComponent<Fish>();
        fishcom.Speed = speed;
        fishcom.FishPathData = PathConfigManager.GetInstance().GetPath(pathid);
        fishcom.UnActiveTime = unactiveTime;
    }
}
