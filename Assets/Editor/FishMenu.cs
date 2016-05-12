using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class FishMenu
{
	[MenuItem("Fish/CreateFishSeason")]
	static void CreateFishSeasonInfo()
	{

	}

	[MenuItem("Fish/Export Season")]
	static void ExportSeason()
	{
        GameObject fishSeasonObj = Selection.activeGameObject;
        if (fishSeasonObj == null)
        {
            Debug.LogError("no seasonobj selected!");
            return;
        }
        FishSeason fishseason = new FishSeason();
        OneWave oneWave = new OneWave();
        fishseason.AddWave(oneWave);
        int fishcellscnt = fishSeasonObj.transform.childCount;
        for (int i = 0; i < fishcellscnt; i++)
        {
            Transform child = fishSeasonObj.transform.GetChild(i);
            if (child.name == "Head")
            {
                oneWave.ea = child.localEulerAngles;
                float x = float.Parse(child.localPosition.x.ToString("#0.00"));
                float y = float.Parse(child.localPosition.y.ToString("#0.00"));
                float z = float.Parse(child.localPosition.z.ToString("#0.00"));
                oneWave.o = new Vector3(x, y, z);
                oneWave.speed = 100;
                oneWave.pathid = 5;
                continue;
            }
            WaveFish wavefish = new WaveFish();
            wavefish.fkid = 0;
            float x1 = float.Parse(child.localPosition.x.ToString("#0.00"));
            float y1 = float.Parse(child.localPosition.y.ToString("#0.00"));
            float z1 = float.Parse(child.localPosition.z.ToString("#0.00"));
            wavefish.p = new Vector3(x1, y1, z1);
            Debug.Log(wavefish.p);
            wavefish.s = child.localScale;
            oneWave.AddWaveFish(wavefish);
        }

        string destpath = "Assets/Resources/SeasonConfigs/" + "season_0.bytes";
        string json = JsonUtility.ToJson(fishseason);
        FileStream fs = new FileStream(destpath, FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        sw.Write(json);
        sw.Flush();
        fs.Close();
		AssetDatabase.Refresh();
	}
}
