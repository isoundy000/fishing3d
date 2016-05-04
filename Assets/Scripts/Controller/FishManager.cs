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
        float xLeft = -120;
        float xRight = 120;
        float yBottom = -70;
        float yUp = 70;

        GameObject fish = GameObject.Instantiate(Resources.Load("FishPrefabs/Fish_00") as GameObject);
        fish.transform.localPosition = new Vector3(xLeft, Random.Range(yBottom + 20, yUp - 20), 96);
        fish.transform.eulerAngles = new Vector3(0, 90, 0);
        Fish fishcom = fish.AddComponent<Fish>();
        fishcom.Speed = 50;
        fishcom.FishPathData = PathConfigManager.GetInstance().GetPath(Random.Range(0,4));
        fishcom.FishPathData.renderPath = true;
    }
}
