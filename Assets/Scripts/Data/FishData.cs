using UnityEngine;
using System.Collections;

public class FishData
{

	private static FishData mInstance = null;
    private FishData()
    { }

    private bool mGameBegin = false;

    public bool GameBegin
    {
        get { return mGameBegin; }
        set { mGameBegin = value; }
    }

    public static FishData GetInstance()
    {
        if (mInstance == null)
            mInstance = new FishData();

        return mInstance;
    }

    public void Initialize()
    {
 
    }
	
	// Update is called once per frame
	void Update (float dt)
    {
	
	}
}
