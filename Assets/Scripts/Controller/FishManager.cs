using UnityEngine;
using System.Collections;

public class FishManager
{
    private static FishManager mInstance = null;
    private FishManager()
    { }

    private float timer = 0;

    public static FishManager GetInstance()
    {
        if (mInstance == null)
            mInstance = new FishManager();

        return mInstance;
    }

    public void Initialize()
    {
 
    }

    public void Update(float dt)
    {
        timer += dt;
        if (timer > 1)
        {
            timer = 0;
            CreateFish();
        }
    }

    public void CreateFish()
    {
        float hBound = 120;
        float yBottom = -70;
        float yUp = 70;
        int[] temp = new int[2] { -1, 1 };
        int flag = temp[Random.Range(0, 2)];
        GameObject fish = GameObject.Instantiate(Resources.Load("FishPrefabs/Fish_00") as GameObject);
        fish.transform.localPosition = new Vector3(hBound * flag, Random.Range(yBottom + 20, yUp - 20), 96);
        fish.transform.eulerAngles = new Vector3(0, -90*flag, 0);
        Fish fishcom = fish.AddComponent<Fish>();
        fishcom.Speed = 50;
        fishcom.FishPathData = PathConfigManager.GetInstance().GetPath(Random.Range(0,4));
        fishcom.FishPathData.renderPath = true;
    }
}
