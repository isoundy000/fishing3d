using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fish : MonoBehaviour {

	public FishPath mFishPath;
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
		mCurrentLife = 0;
		mStepTime = 0;
		mPosX = transform.position.x;
		mPosY = transform.position.y;
		mPosZ = transform.position.z;
		mRotationX = transform.eulerAngles.x;
		mRotationY = transform.eulerAngles.y;
		frameCnt = 0;
		posIndex = 0;
		rXDelta = 0;
		rYDelta = 0;
		rotatedVec = Vector3.forward;
	}

	// Update is called once per frame
	void Update () 
	{
		float framedt = Time.deltaTime * mSpeedScaleFactor;
		mLastFrameLife = mCurrentLife;
		mCurrentLife += framedt;
		mStepTime = 0;
		for(int i = 0; i < mFishPath.controlPoints.Length; i ++)
		{
			mStepTime += mFishPath.controlPoints[i].mTime;
			if(mCurrentLife <= mStepTime)
			{
				mCurrentStep = i;
				break;
			}
			else
			{
				mCurrentStep = mFishPath.controlPoints.Length;
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
					t2 += mFishPath.controlPoints[i].mTime;
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
				t3 += mFishPath.controlPoints[i].mTime;
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
	}

	void updateFishPositionAndRotation(int step,float dt)
	{
		if(step >= 0 && step < mFishPath.controlPoints.Length)
		{
			rXDelta = dt * mFishPath.controlPoints[step].mRotationChange.x / mFishPath.controlPoints[step].mTime;
			rYDelta = dt * mFishPath.controlPoints[step].mRotationChange.y / mFishPath.controlPoints[step].mTime;
			mRotationX += rXDelta;
			mRotationY += rYDelta;
		}
		step = Mathf.Min(step,mFishPath.controlPoints.Length-1);
		rotatedVec = MathUtil.GetInstance().Rotate(Vector3.forward,mRotationX,mRotationY);
		Vector3 dL = dt * mFishPath.baseSpeed * rotatedVec * mFishPath.controlPoints[step].mSpeedScale;

		mPosX += dL.x;
		mPosZ += dL.z;
		mPosY += dL.y;

		this.transform.localPosition = new Vector3(mPosX,mPosY,mPosZ);
		this.transform.eulerAngles = new Vector3(mRotationX,mRotationY,0);
	}
}
