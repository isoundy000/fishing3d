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
}
