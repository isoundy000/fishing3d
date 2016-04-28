using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

//这个类主要是为了序列化json数据时用的
public class ControlPoint
{
    public double time;
    public double speedScale;
    public double rx;
    public double ry;
}

public class PathConfigManager 
{

	private PathConfigManager() { }
	private static PathConfigManager mInstance;
	public static PathConfigManager GetInstance()
	{
		if (mInstance == null)
		{
			mInstance = new PathConfigManager();
		}
		return mInstance;
	}

	public bool Save(string filepath,FishPath path)
	{
        int length = path.controlPoints.Length;
        List<ControlPoint> tempList = new List<ControlPoint>();
        foreach (FishPathControlPoint point in path.controlPoints)
        {
            ControlPoint cp = new ControlPoint();
            cp.time = point.mTime;
            cp.speedScale = point.mSpeedScale;
            cp.rx = point.mRotationChange.x;
            cp.ry = point.mRotationChange.y;
            tempList.Add(cp);
            string json_array = JsonMapper.ToJson(cp);
            Debug.Log(json_array);
        }
        //string json_array = JsonMapper.ToJson(tempList.ToArray());
        //Debug.Log(json_array);
		return true;
	}
}
