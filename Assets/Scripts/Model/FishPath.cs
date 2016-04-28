using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FishPath : MonoBehaviour {

	//基准速度
	[SerializeField]
	private float mBaseSpeed = 100;

	//速度缩放因子
	private float mSpeedScaleFactor = 1;

	public float speedScaleFactor
	{
		get{return mSpeedScaleFactor;}
		set{mSpeedScaleFactor = value;}
	}

	[SerializeField]
	private FishPathControlPoint[] mControlPoints = new FishPathControlPoint[0];

	[SerializeField]
	private bool mRenderPath = false;

	public bool renderPath
	{
		get{return mRenderPath;}
		set{mRenderPath = value;}
	}

	//通过控制点插值来的点，路径上的点
	public class FinePoint
	{
		public Vector3 position = Vector3.zero;
		public Vector2 rotation = Vector2.zero;
		public int controlIndex = 0;
	}
	private List<FinePoint> mFinePointsList = new List<FinePoint>();

	//路径的颜色
	public Color lineColour = Color.white;

	private float mCurrentLife;
	private float mLastFrameLife;
	private float mStepTime;
	private int mCurrentStep;
	private int mLastFrameStep;
	private float SECOND_ONE_FRAME = 0.02f;

	private Vector3 rotatedVec=Vector3.forward;
	private Vector3 mBornPosition;
	private Vector3 mBornRotation;
	private FinePoint mCurrentFinePoint = new FinePoint();

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

	public float baseSpeed
	{
		set{mBaseSpeed = value;}
		get{return mBaseSpeed;}
	}

	// Use this for initialization
	void Start ()
	{

	}

	void OnEnable()
	{
		mBornPosition = transform.position;
		mBornRotation = transform.eulerAngles;
		mCurrentFinePoint.rotation = transform.eulerAngles;
		rotatedVec = Vector3.forward;
		mCurrentLife = 0;
		mLastFrameLife = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDrawGizmos()
	{
		if(mRenderPath)
		{
			if(mBornPosition != transform.position || mFinePointsList.Count == 0 || mBornRotation != transform.eulerAngles)
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
		mCurrentFinePoint.position = mBornPosition;
		mCurrentFinePoint.rotation = mBornRotation;
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
						mCurrentFinePoint = caculateOneFinePoint(mCurrentFinePoint.position,mCurrentFinePoint.rotation,tmpStep-1,SECOND_ONE_FRAME);
						mFinePointsList.Add(mCurrentFinePoint);
					}
					mCurrentFinePoint = caculateOneFinePoint(mCurrentFinePoint.position,mCurrentFinePoint.rotation,tmpStep-1,dt1-SECOND_ONE_FRAME*cnt1);
					mFinePointsList.Add(mCurrentFinePoint);

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
					mCurrentFinePoint = caculateOneFinePoint(mCurrentFinePoint.position,mCurrentFinePoint.rotation,mCurrentStep,SECOND_ONE_FRAME);
					mFinePointsList.Add(mCurrentFinePoint);
				}
				mCurrentFinePoint = caculateOneFinePoint(mCurrentFinePoint.position,mCurrentFinePoint.rotation,mCurrentStep,dt2 - SECOND_ONE_FRAME*cnt2);
				mFinePointsList.Add(mCurrentFinePoint);
				mLastFrameStep = mCurrentStep;
			}
			else
			{
				mCurrentFinePoint = caculateOneFinePoint(mCurrentFinePoint.position,mCurrentFinePoint.rotation,mCurrentStep,SECOND_ONE_FRAME);
				mFinePointsList.Add(mCurrentFinePoint);
			}
		}
	}
	
	public FinePoint caculateOneFinePoint(Vector3 startPosition,Vector2 startRotation,int step,float dt)
	{
		FinePoint point = new FinePoint();
		if(step < 0 || dt < 0) return point;

		if(step >= 0 && step < mControlPoints.Length)
		{
			float rXDelta = dt * mControlPoints[step].mRotationChange.x / mControlPoints[step].mTime;
			float rYDelta = dt * mControlPoints[step].mRotationChange.y / mControlPoints[step].mTime;
			startRotation.x += rXDelta;
			startRotation.y += rYDelta;
		}
		step = Mathf.Min(step,mControlPoints.Length-1);
		rotatedVec = MathUtil.GetInstance().Rotate(Vector3.forward,startRotation.x,startRotation.y);
		Vector3 dL = dt * mBaseSpeed * rotatedVec * mControlPoints[step].mSpeedScale;
		
		startPosition +=dL;

		point.position = startPosition;
		point.rotation = startRotation;
		point.controlIndex = step;

		return point;
	}
}
