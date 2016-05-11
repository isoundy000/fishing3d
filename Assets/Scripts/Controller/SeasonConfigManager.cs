using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;

public class SeasonConfigManager 
{

	private SeasonConfigManager() { }
	private static SeasonConfigManager mInstance;

    private Dictionary<int, FishSeason> mSeasonMap = new Dictionary<int, FishSeason>();
	public static SeasonConfigManager GetInstance()
	{
		if (mInstance == null)
		{
			mInstance = new SeasonConfigManager();
		}
		return mInstance;
	}

    public void Initialize()
    {
        //LoadAllPathes();
        FishSeason season = new FishSeason();
        OneWave wave = new OneWave();
        season.AddWave(wave);
        wave.o = Vector3.zero;
        wave.speed = 100;
        wave.ea = Vector3.zero;
        wave.pathid = 0;
        for (int i = 0; i < 10; i++)
        {
            WaveFish waveFish = new WaveFish();
            waveFish.fkid = 0;
            waveFish.p = Vector3.zero;
            waveFish.s = Vector3.zero;
            wave.AddWaveFish(waveFish);
        }

        Debug.Log(JsonUtility.ToJson(season, true));
        //Debug.Log(JsonMapper.ToJson(season));
    }

    //public bool Save(string filepath,FishPath path)
    //{
    //    JsonPath jsonPath = new JsonPath();
    //    jsonPath.pointList = new List<JsonControlPoint>();
    //    jsonPath.r = (int)path.lineColour.r;
    //    jsonPath.g = (int)path.lineColour.g;
    //    jsonPath.b = (int)path.lineColour.b;
    //    jsonPath.baseSpeed = (int)path.baseSpeed;
    //    foreach (FishPathControlPoint point in path.controlPoints)
    //    {
    //        JsonControlPoint cp = new JsonControlPoint();
    //        cp.time = point.mTime;
    //        cp.speedScale = point.mSpeedScale;
    //        cp.rx = point.mRotationChange.x;
    //        cp.ry = point.mRotationChange.y;
    //        jsonPath.pointList.Add(cp);
    //    }
    //    string json = JsonMapper.ToJson(jsonPath);
    //    FileStream fs = new FileStream(filepath, FileMode.Create);
    //    StreamWriter sw = new StreamWriter(fs);
    //    sw.Write(json);
    //    sw.Flush();
    //    fs.Close();
    //    return true;
    //}

    //public FishPath Load(string filepath)
    //{
    //    FishPath fishPath = ScriptableObject.CreateInstance<FishPath>();
    //    FileStream fs = new FileStream(filepath,FileMode.Open);
    //    StreamReader sr = new StreamReader(fs);
    //    string jsonStr = sr.ReadToEnd();
    //    JsonPath jsonPath = new JsonPath();
    //    jsonPath = JsonMapper.ToObject<JsonPath>(jsonStr);
    //    fishPath.ResetPath();
    //    fishPath.lineColour = new Color(jsonPath.r, jsonPath.g, jsonPath.b);
    //    fishPath.baseSpeed = (float)jsonPath.baseSpeed;
    //    foreach (JsonControlPoint point in jsonPath.pointList)
    //    {
    //        FishPathControlPoint fpcp = ScriptableObject.CreateInstance<FishPathControlPoint>();
    //        fpcp.mSpeedScale = (float)point.speedScale;
    //        fpcp.mRotationChange = new Vector2((float)point.rx,(float)point.ry);
    //        fpcp.mTime = (float)point.time;
    //        fishPath.AddPoint(fpcp);
    //    }
    //    fishPath.isNewPath = false;
    //    return fishPath;
    //}

    //private void LoadAllPathes()
    //{
    //    for (int i = 0; i < 5; i++)
    //    {
    //        TextAsset ta = Resources.Load<TextAsset>("Pathes/" + i.ToString());
    //        //Debug.Log(ta == null);
    //        if (ta != null)
    //        {
    //            string jsonStr = ta.text;
    //            if (jsonStr != null && jsonStr.Length > 0)
    //            {
    //                JsonPath jsonPath = new JsonPath();
    //                jsonPath = JsonMapper.ToObject<JsonPath>(jsonStr);
    //                if (jsonPath == null) continue;
    //                FishPath fishPath = ScriptableObject.CreateInstance<FishPath>();
    //                fishPath.lineColour = new Color(jsonPath.r, jsonPath.g, jsonPath.b);
    //                fishPath.baseSpeed = (float)jsonPath.baseSpeed;
    //                foreach (JsonControlPoint point in jsonPath.pointList)
    //                {
    //                    FishPathControlPoint fpcp = ScriptableObject.CreateInstance<FishPathControlPoint>();
    //                    fpcp.mSpeedScale = (float)point.speedScale;
    //                    fpcp.mRotationChange = new Vector2((float)point.rx, (float)point.ry);
    //                    fpcp.mTime = (float)point.time;
    //                    fishPath.AddPoint(fpcp);
    //                }
    //                mSeasonMap.Add(i, fishPath);
    //            }
    //        }
    //    }
    //}

    //public FishPath GetPath(int id)
    //{
    //    FishPath path = null;
    //    mSeasonMap.TryGetValue(id, out path);
    //    return path;
    //}
}
