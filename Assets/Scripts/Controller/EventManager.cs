using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;

public class FishEvent
{
    public int et;  //eventtype
    public int t;   //time
    public int begint;  //begintime
    public int endt;  //endtime

    public FishEvent()
    { }
    public FishEvent(int eventtime, int time)
    {
        et = eventtime;
        t = time;
    }
}

public class EventManager
{
	private static EventManager mInstance = null;
    private EventManager()
    { }

    private List<FishEvent> mEventList = new List<FishEvent>();

    private FishEvent mEvent = null;

    private float timer = 2;

    public static EventManager GetInstance()
    {
        if (mInstance == null)
            mInstance = new EventManager();

        return mInstance;
    }

    public void Initialize()
    {
        mEventList = new List<FishEvent>();
        //mEventList.Add(new FishEvent(0, 60));
        //mEventList.Add(new FishEvent(1, 60));
        //string jsonStr = JsonMapper.ToJson(mEventList);
        //string filepath = Application.dataPath + "/Resources/event.bytes";
        //JsonUtil.Save(jsonStr, filepath);

        //LoadEventConfig();
        //CaculateBeginAndEndTime();
    }

    public void Update(float dt)
    {
        if (FishData.GetInstance().GameState == GameState.MainLoop)
        {
            timer += dt;
            if (timer > 3f)
            {
                timer = 0;
                TestTeam();
            }
        }
        return;
        mEvent = GetEvent(dt);
        if(mEvent == null) return;
        switch (mEvent.et)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            default:
                break;
        }
    }

    public void LoadEventConfig()
    {
        string filepath = Application.dataPath + "/Resources/event.bytes";
        FileStream fs = new FileStream(filepath,FileMode.Open);
        StreamReader sr = new StreamReader(fs);
        string jsonStr = sr.ReadToEnd();
        mEventList = JsonMapper.ToObject<List<FishEvent>>(jsonStr);
    }

    public void CaculateBeginAndEndTime()
    {
        if (mEventList != null)
        {
            int t = 0;
            foreach (FishEvent eventdata in mEventList)
            {
                eventdata.begint = t;
                eventdata.endt = t + eventdata.t;
                t = eventdata.endt;
            }
        }
    }

    public FishEvent GetEvent(float t)
    {
        foreach (FishEvent eventdata in mEventList)
        {
            if (t < eventdata.endt)
                return eventdata;
        }
        return null;
    }

    public void TestTeam()
    {
        float hBound = 120;
        float yBottom = -70;
        float yUp = 70;
        int[] temp = new int[2] { -1, 1 };
        int flag = temp[Random.Range(0, 2)];
        float speed = Random.Range(40, 70);
        int pathid = Random.Range(0, 5);
        Vector3 headPosition = new Vector3(hBound * flag, Random.Range(yBottom + 20, yUp - 20), Random.Range(96, 96 + 20));
        //Vector3 bornEulerAngles = new Vector3(Random.Range(-20, 20), Random.Range(80, 100) * flag, 0);
        Vector3 bornEulerAngles = new Vector3(0, -90 * flag, 0);
        int randomCnt = Random.Range(5,11);
        for (int i = 0; i < randomCnt; i++)
        {
            Vector3 offset = new Vector3(i * 15 * flag, 0, 0);
            //offset = Quaternion.Euler(bornEulerAngles) * offset;
            FishManager.GetInstance().CreateFish(0, headPosition + offset, bornEulerAngles, pathid, speed, i * 15.0f / speed);
        }
    }
}
