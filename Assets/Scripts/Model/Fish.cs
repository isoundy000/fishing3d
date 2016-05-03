using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fish : MonoBehaviour {

	private FishPath mFishPath;
	public float mCurrentLife = 0;
	public float mLastFrameLife;
	public float mStepTime = 0;
	public int mCurrentStep;
	public int mLastFrameStep;
	private float mSpeedScaleFactor;
    private float mSpeed;
    private FinePoint oneFinePoint;

	float SECOND_ONE_FRAME = 0.02f;

    public float Speed
    {
        get { return mSpeed; }
        set 
        { 
            mSpeed = value;
            mSpeedScaleFactor = mSpeed / mFishPath.baseSpeed;
        }
    }

	// Use this for initialization
	void Start ()
    {
        mFishPath = this.GetComponent<FishPath>();
	}

	// Update is called once per frame
	void Update () 
	{
        if (mFishPath == null) return;

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
                    CaculateTransform(tmpStep - 1, SECOND_ONE_FRAME);
				}
                CaculateTransform(tmpStep - 1, dt1 - SECOND_ONE_FRAME * cnt1);
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
                CaculateTransform(mCurrentStep, SECOND_ONE_FRAME);
			}
            CaculateTransform(mCurrentStep, dt2 - SECOND_ONE_FRAME * cnt2);
			mLastFrameStep = mCurrentStep;
		}
		else
		{
			int cnt1 = Mathf.FloorToInt(framedt / SECOND_ONE_FRAME);
			for(int i = 0; i < cnt1; i ++)
			{
                CaculateTransform(mCurrentStep, SECOND_ONE_FRAME);
			}
            CaculateTransform(mCurrentStep, framedt - SECOND_ONE_FRAME * cnt1);
		}        
	}

    public void CaculateTransform(int step, float dt)
    {
        oneFinePoint = mFishPath.CaculateOneFinePoint(transform.localPosition, transform.eulerAngles, step, dt);
        transform.localPosition = oneFinePoint.position;
        transform.eulerAngles = oneFinePoint.rotation;
    }
}
