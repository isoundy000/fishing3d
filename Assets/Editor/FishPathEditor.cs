using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(FishPath))]
public class FishPathEditor : Editor {

	void OnSceneGUI()
	{
		//Handles.DrawBezier(Vector3.zero,100*Vector3.one,new Vector3(0,0,0),new Vector3(0,90,0),Color.red,null,2);
	}

	public override void OnInspectorGUI()
	{
		FishPath fishPath = (FishPath)target;

		GUILayout.Space(10);
		fishPath.lineColour = EditorGUILayout.ColorField("Line Colour",fishPath.lineColour);
		GUILayout.Space(5);

		EditorGUILayout.BeginHorizontal();
		fishPath.baseSpeed = EditorGUILayout.FloatField("Base Speed",fishPath.baseSpeed);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		fishPath.renderPath = EditorGUILayout.Toggle("Render Path",fishPath.renderPath);
		EditorGUILayout.EndHorizontal();

//		EditorGUILayout.BeginHorizontal();
//		fishPath.speedScaleFactor = EditorGUILayout.FloatField("Speed Scale Factor",fishPath.speedScaleFactor);
//		EditorGUILayout.EndHorizontal();

		int numberOfControlPoints = fishPath.numberOfControlPoints;
		
		if(numberOfControlPoints>0)
		{

			GUILayout.Space(5);
			if(GUILayout.Button("Reset Path")){
				if(EditorUtility.DisplayDialog("Resetting path?", "Are you sure you want to delete all control points?", "Delete", "Cancel")){
					fishPath.ResetPath();
					return;
				}
			}

			GUILayout.Space(5);
			if(GUILayout.Button("Save"))
			{
                string savepath = EditorUtility.SaveFilePanel("Save",Application.dataPath + "/Resources/Pathes/","","bytes");
                if (savepath.Length > 0)
                {
                    PathConfigManager.GetInstance().Save(savepath, fishPath);
                    AssetDatabase.Refresh();
                }
			}
			
			GUILayout.Space(10);
			GUILayout.Box(EditorGUIUtility.whiteTexture, GUILayout.Height(2), GUILayout.Width(Screen.width-20));
			GUILayout.Space(3);

//			//scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
			for(int i=0; i<numberOfControlPoints; i++)
			{
				FishPathControlPoint point = fishPath.controlPoints[i];
				point.highLight = GUILayout.Toggle(point.highLight,"HighLight");
				point.color = EditorGUILayout.ColorField("Line Colour",point.color);

				point.mTime = EditorGUILayout.FloatField("Time",point.mTime);
				point.mSpeedScale = EditorGUILayout.FloatField("SpeedScale",point.mSpeedScale);
				point.mRotationChange = EditorGUILayout.Vector2Field("RotationChange",point.mRotationChange);

				EditorGUILayout.BeginHorizontal();
				if(GUILayout.Button("Delete"))
				{
					fishPath.DeletePoint(i);
					numberOfControlPoints = fishPath.numberOfControlPoints;
					EditorUtility.SetDirty(fishPath);
					return;	
				}

				if(GUILayout.Button("Add New Point After"))
				{
					fishPath.AddPoint(i+1);
					EditorUtility.SetDirty(fishPath);
				}

				EditorGUILayout.EndHorizontal();
				
				GUILayout.Space(7);
				GUILayout.Box(EditorGUIUtility.whiteTexture, GUILayout.Height(2), GUILayout.Width(Screen.width-25));
				GUILayout.Space(7);
			} 
//			//EditorGUILayout.EndScrollView();
		}
		else
		{
			if(GUILayout.Button("Add New Point"))
			{
				//Undo.RegisterSceneUndo("Create a new Camera Path point");
				fishPath.AddPoint();
				EditorUtility.SetDirty(fishPath);
			}
		}
		
		if(GUI.changed)
		{
			fishPath.CaculateFinePoints();
			EditorUtility.SetDirty(fishPath);
		}
	}
}
