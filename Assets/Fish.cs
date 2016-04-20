﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fish : MonoBehaviour {
	public class ControlPoint
	{
		public float time;
		public float speedFactor;
		public float rChangeX;
		public float rChangeY;
		
		public ControlPoint(float t,float sf,float rcx,float rcy)
		{
			this.time = t;
			this.speedFactor = sf;
			this.rChangeX = rcx;
			this.rChangeY = rcy;
		}

	}
	public float mBaseSpeed;
	public List<ControlPoint> mControlPoints;
	public float mCurrentLife;
	public float mLastFrameLife;
	public float mStepTime;
	public int mCurrentStep;
	public int mLastFrameStep;
	public float mPosX,mPosY,mPosZ;
	public float mRotationX,mRotationY;
	public float mSpeedScaleFactor;
	float rXDelta,rYDelta;
	public LineRenderer lineRender;
	int frameCnt;
	int posIndex;
	Vector3 rotatedVec;
	float SECOND_ONE_FRAME = 0.02f;
	// Use this for initialization
	void Start () {
		mControlPoints = new List<ControlPoint>();
		mControlPoints.Add(new ControlPoint(10,1,0,0));
		mControlPoints.Add(new ControlPoint(1,1.2f,0,-180));
		mControlPoints.Add(new ControlPoint(3,1,-10,0));
		mControlPoints.Add(new ControlPoint(1,1.5f,0,-180));
		mControlPoints.Add(new ControlPoint(2,1,0,0));
		mControlPoints.Add(new ControlPoint(1,2,0,180));
		mControlPoints.Add(new ControlPoint(2,1.2f,0,0));
		mControlPoints.Add(new ControlPoint(1,1,0,-180));


		mCurrentLife = 0;
		//mBaseSpeed = 0;
		mStepTime = 0;
		mPosX = transform.position.x;
		mPosY = transform.position.y;
		mPosZ = transform.position.z;
		mRotationX = transform.eulerAngles.x;
		mRotationY = transform.eulerAngles.y;
		//this.transform.Rotate(Vector3.right * 60);
		//this.transform.Translate(Vector3.forward*100);
		//this.transform.Rotate(Vector3.up * 30,Space.World);
		//mSpeedScaleFactor = mBaseSpeed / 10.0f;
		frameCnt = 0;
		posIndex = 0;
		lineRender.SetVertexCount(100000);
		rXDelta = 0;
		rYDelta = 0;
		rotatedVec = Vector3.forward;

		print (Screen.width +  " , " + Screen.height + " , " +  Camera.main.aspect);
	}

	// Update is called once per frame
	void Update () {
		float framedt = Time.deltaTime * mSpeedScaleFactor;
		mLastFrameLife = mCurrentLife;
		mCurrentLife += Time.deltaTime * mSpeedScaleFactor;
		mStepTime = 0;
		for(int i = 0; i < mControlPoints.Count; i ++)
		{
			mStepTime += mControlPoints[i].time;
			if(mCurrentLife <= mStepTime)
			{
				mCurrentStep = i;
				break;
			}
			else
			{
				mCurrentStep = mControlPoints.Count;
			}
		}

		if(mLastFrameStep != mCurrentStep)
		{
			int tmpStep = mLastFrameStep;
			float t1 = mLastFrameLife;
			while(true)
			{
				tmpStep = tmpStep + 1;
				if(tmpStep > mCurrentStep)
				{
					break;
				}
				float t2 = 0;
				for(int i = 0; i < tmpStep; i ++)
				{
					t2 += mControlPoints[i].time;
				}
				float dt1 = t2 - t1;
				t1 = t2;
				int cnt1 = Mathf.FloorToInt(dt1 / SECOND_ONE_FRAME);
				for(int i = 0; i < cnt1; i ++)
				{
					updateFishPositionAndRotation(tmpStep-1,SECOND_ONE_FRAME);
				}
				updateFishPositionAndRotation(tmpStep-1,dt1-SECOND_ONE_FRAME*cnt1);
			}
			float t3 = 0;
			for(int i = 0; i < mCurrentStep; i ++)
			{
				t3 += mControlPoints[i].time;
			}
			float dt2 = mCurrentLife - t3;
			int cnt2 = Mathf.FloorToInt(dt2 / SECOND_ONE_FRAME);
			for(int i = 0; i < cnt2; i ++)
			{
				updateFishPositionAndRotation(mCurrentStep,SECOND_ONE_FRAME);
			}
			updateFishPositionAndRotation(mCurrentStep,dt2 - SECOND_ONE_FRAME*cnt2);
			mLastFrameStep = mCurrentStep;
		}
		else
		{
			int cnt1 = Mathf.FloorToInt(framedt / SECOND_ONE_FRAME);
			for(int i = 0; i < cnt1; i ++)
			{
				updateFishPositionAndRotation(mCurrentStep,SECOND_ONE_FRAME);
			}
			updateFishPositionAndRotation(mCurrentStep,framedt-SECOND_ONE_FRAME*cnt1);
		}




		//this.transform.Rotate(Vector3.right * Time.deltaTime * mSpeedScaleFactor  *  mControlPoints[mCurrentStep].rChangeX / mControlPoints[mCurrentStep].time);
		//this.transform.Rotate(Vector3.up * Time.deltaTime * mSpeedScaleFactor * mControlPoints[mCurrentStep].rChangeY / mControlPoints[mCurrentStep].time,Space.World);
		//this.transform.Translate(Vector3.forward * Time.deltaTime * mSpeedScaleFactor * mBaseSpeed  * mControlPoints[mCurrentStep].speedFactor);

//		rXDelta = Time.deltaTime * mSpeedScaleFactor * mControlPoints[mCurrentStep].rChangeX / mControlPoints[mCurrentStep].time;
//		rYDelta = Time.deltaTime * mSpeedScaleFactor * mControlPoints[mCurrentStep].rChangeY / mControlPoints[mCurrentStep].time;
//		mRotationX += rXDelta;
//		mRotationY += rYDelta;

//		rotatedVec = Quaternion.Euler(new Vector3(mRotationX,mRotationY,0)) * Vector3.forward;
		//rotatedVec = this.rotate(Vector3.forward,mRotationX,mRotationY);
		//print (rotatedVec.sqrMagnitude.ToString());
//		Vector3 dL = Time.deltaTime * mSpeedScaleFactor * mBaseSpeed * rotatedVec * mControlPoints[mCurrentStep].speedFactor;

//		mPosX += (dL.magnitude * Mathf.Cos(Mathf.Deg2Rad * mRotationX) * Mathf.Sin(Mathf.Deg2Rad * mRotationY));
//		mPosZ += (dL.magnitude * Mathf.Cos(Mathf.Deg2Rad * mRotationX) * Mathf.Cos(Mathf.Deg2Rad * mRotationY));

//		mPosY -= (dL.magnitude * Mathf.Sin(Mathf.Deg2Rad * mRotationX));
		//print (dL.sqrMagnitude.ToString() + "   " + mPosY.ToString());

//		this.transform.localPosition = new Vector3(mPosX,mPosY,mPosZ);
		//this.transform.Translate(Vector3.forward * Time.deltaTime * mSpeedScaleFactor * mBaseSpeed  * mControlPoints[mCurrentStep].speedFactor);

//		this.transform.eulerAngles = new Vector3(mRotationX,mRotationY,0);
		//print ("fish" + this.transform.rotation.ToString());
//		if(frameCnt == 3)
//		{
			lineRender.SetPosition(posIndex,this.transform.position);
			posIndex ++;
			//frameCnt = 0;
			//print (posIndex);
//		}
		//frameCnt ++;
		//if(frameCnt % 60 == 0)
		//{
		//	print ("fish1life: " + mCurrentLife + " pos: " + this.transform.localPosition + " angle: " + this.transform.eulerAngles);
		//}
	}

	Vector3 rotate( Vector3 point, float angleX, float angleY)
	{
		Vector3 resultX = new Vector3();
		resultX.x = point.x;
		resultX.y = point.x * 0 + point.y * Mathf.Cos(angleX*Mathf.Deg2Rad) + point.z * (-Mathf.Sin(angleX*Mathf.Deg2Rad));
		resultX.z = point.x * 0 + point.y * Mathf.Sin(angleX*Mathf.Deg2Rad) + point.z * Mathf.Cos(angleX*Mathf.Deg2Rad);

		Vector3 result = new Vector3();
		result.x = resultX.x * Mathf.Cos(angleX*Mathf.Deg2Rad) + resultX.y * 0 + resultX.z * Mathf.Sin(angleX*Mathf.Deg2Rad);
		result.y = resultX.x * 0 + resultX.y * 1 + resultX.z * 0;
		result.z = resultX.x * (-Mathf.Sin(angleX*Mathf.Deg2Rad)) + resultX.y * 0 + resultX.z * Mathf.Cos(angleX*Mathf.Deg2Rad);

		return result;
	}

	void updateFishPositionAndRotation(int step,float dt)
	{
		if(step >= 0 && step < mControlPoints.Count)
		{
			rXDelta = dt * mControlPoints[step].rChangeX / mControlPoints[step].time;
			rYDelta = dt * mControlPoints[step].rChangeY / mControlPoints[step].time;
			mRotationX += rXDelta;
			mRotationY += rYDelta;
		}
		step = Mathf.Min(step,mControlPoints.Count-1);
		rotatedVec = this.rotate(Vector3.forward,mRotationX,mRotationY);
		Vector3 dL = dt * mBaseSpeed * rotatedVec * mControlPoints[step].speedFactor;
		
		mPosX += (dL.magnitude * Mathf.Cos(Mathf.Deg2Rad * mRotationX) * Mathf.Sin(Mathf.Deg2Rad * mRotationY));
		mPosZ += (dL.magnitude * Mathf.Cos(Mathf.Deg2Rad * mRotationX) * Mathf.Cos(Mathf.Deg2Rad * mRotationY));
		mPosY -= (dL.magnitude * Mathf.Sin(Mathf.Deg2Rad * mRotationX));

		this.transform.localPosition = new Vector3(mPosX,mPosY,mPosZ);
		this.transform.eulerAngles = new Vector3(mRotationX,mRotationY,0);
	}
}
