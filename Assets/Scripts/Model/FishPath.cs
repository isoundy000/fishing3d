using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FishPath : MonoBehaviour {

	public float mBaseSpeed = 100;
	
	private float mSpeedScaleFactor = 1;

	public float speedScaleFactor
	{
		get{return mSpeedScaleFactor;}
		set{mSpeedScaleFactor = value;}
	}

	[SerializeField]
	private FishPathControlPoint[] mControlPoints = new FishPathControlPoint[0];

	public class FinePoint
	{
		public Vector3 position;
		public int controlIndex;
	}
	private List<FinePoint> mFinePointsList = new List<FinePoint>();
	
	public Color lineColour = Color.white;

	private float mCurrentLife;
	private float mLastFrameLife;
	private float mStepTime;
	private int mCurrentStep;
	private int mLastFrameStep;
	private float mPosX,mPosY,mPosZ;
	private float mRotationX,mRotationY;
	private float SECOND_ONE_FRAME = 0.02f;
	private float rXDelta = 0;
	private float rYDelta = 0;
	private Vector3 rotatedVec=Vector3.forward;
	private Vector3 mBornPosition;
	private Vector3 mBornRotation;

	public int numberOfControlPoints
	{
		get
		{
			if (mControlPoints != null)
				return mControlPoints.Length;
			else
				return 0;
		}
	}

	//get the array of control points
	public FishPathControlPoint[] controlPoints
	{
		get { return mControlPoints; }
	}

	// Use this for initialization
	void Start () {

	}

	void OnEnable()
	{
		mBornPosition = transform.position;
		mBornRotation = transform.eulerAngles;
		mRotationX = transform.eulerAngles.x;
		mRotationY = transform.eulerAngles.y;
		rXDelta = 0;
		rYDelta = 0;
		rotatedVec = Vector3.forward;
		mCurrentLife = 0;
		mLastFrameLife = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDrawGizmos()
	{
		if(mBornPosition != transform.position || mFinePointsList.Count == 0)
		{
			if(Application.isPlaying == false)
			{
				mBornPosition = transform.position;
			   	mBornRotation = transform.eulerAngles;
			}
			this.CaculateFinePoints();
		}
		for(int i = 0; i < mFinePointsList.Count-1; i ++)
		{
			if(controlPoints[mFinePointsList[i].controlIndex].highLight)
				Gizmos.color = controlPoints[mFinePointsList[i].controlIndex].color;
			else
				Gizmos.color = this.lineColour;
			Gizmos.DrawLine(mFinePointsList[i].position,mFinePointsList[i + 1].position);
		}
	}

	public void AddPoint(int index)
	{
		List<FishPathControlPoint> tempList = new List<FishPathControlPoint>(mControlPoints);
		FishPathControlPoint newpoint = new FishPathControlPoint();
		tempList.Insert(index,newpoint);
		mControlPoints = tempList.ToArray();
		this.CaculateFinePoints();
	}

	public void AddPoint()
	{
		AddPoint(mControlPoints.Length);
	}

	public void DeletePoint(int index)
	{
		List<FishPathControlPoint> tempList = new List<FishPathControlPoint>(mControlPoints);
		tempList.RemoveAt(index);
		mControlPoints = tempList.ToArray();
		this.CaculateFinePoints();
	}

	public void ResetPath()
	{
		mControlPoints = new FishPathControlPoint[0];
		mFinePointsList.Clear();
	}

	public float GetTotalTime()
	{
		float time = 0;
		for(int i = 0; i < numberOfControlPoints; i ++)
		{
			time += controlPoints[i].mTime;
		}
		return time;
	}

	public void CaculateFinePoints()
	{
		float time = 0;

		rXDelta = 0;
		rYDelta = 0;
		mPosX = mBornPosition.x;
		mPosY = mBornPosition.y;
		mPosZ = mBornPosition.z;
		mRotationX = mBornRotation.x;
		mRotationY = mBornRotation.y;
		rotatedVec = Vector3.forward;
		mCurrentLife = 0;
		mLastFrameLife = 0;
		float totaltime = GetTotalTime();
		mFinePointsList.Clear();
		while(time < totaltime)
		{
			time += SECOND_ONE_FRAME;
			mLastFrameLife = mCurrentLife;
			mCurrentLife += SECOND_ONE_FRAME * mSpeedScaleFactor;
			mStepTime = 0;
			for(int i = 0; i < mControlPoints.Length; i ++)
			{
				mStepTime += mControlPoints[i].mTime;
				if(mCurrentLife <= mStepTime)
				{
					mCurrentStep = i;
					break;
				}
				else
				{
					mCurrentStep = mControlPoints.Length;
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
						t2 += mControlPoints[i].mTime;
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
					t3 += mControlPoints[i].mTime;
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
				updateFishPositionAndRotation(mCurrentStep,SECOND_ONE_FRAME);
			}
		}
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
		if(step >= 0 && step < mControlPoints.Length)
		{
			rXDelta = dt * mControlPoints[step].mRotationChange.x / mControlPoints[step].mTime;
			rYDelta = dt * mControlPoints[step].mRotationChange.y / mControlPoints[step].mTime;
			mRotationX += rXDelta;
			mRotationY += rYDelta;
		}
		step = Mathf.Min(step,mControlPoints.Length-1);
		rotatedVec = this.rotate(Vector3.forward,mRotationX,mRotationY);
		Vector3 dL = dt * mBaseSpeed * rotatedVec * mControlPoints[step].mSpeedScale;
		
		mPosX += (dL.magnitude * Mathf.Cos(Mathf.Deg2Rad * mRotationX) * Mathf.Sin(Mathf.Deg2Rad * mRotationY));
		mPosZ += (dL.magnitude * Mathf.Cos(Mathf.Deg2Rad * mRotationX) * Mathf.Cos(Mathf.Deg2Rad * mRotationY));
		mPosY -= (dL.magnitude * Mathf.Sin(Mathf.Deg2Rad * mRotationX));

		FinePoint point = new FinePoint();
		point.position = new Vector3(mPosX,mPosY,mPosZ);
		point.controlIndex = step;
		mFinePointsList.Add(point);
	}
}
