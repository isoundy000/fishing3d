using UnityEngine;
using System.Collections;

public class Fish1 : MonoBehaviour {
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
	public ControlPoint[] mControlPoints;
	public float mCurrentLife;
	public float mStepTime;
	public int mCurrentStep;
	public float mPosX,mPosY,mPosZ;
	public float mRotationX,mRotationY;
	public float mSpeedScaleFactor;
	float rXDelta,rYDelta;
	public LineRenderer lineRender;
	int frameCnt;
	int posIndex;
	Vector3 rotatedVec;
	// Use this for initialization
	void Start () {
		mControlPoints = new ControlPoint[5];
		mControlPoints[0] = new ControlPoint(4,1,30,-30);
		mControlPoints[1] = new ControlPoint(4,1.20f,-30,30);
		mControlPoints[2] = new ControlPoint(10,1.40f,-30,360);
		mControlPoints[3] = new ControlPoint(10,1.50f,30,360);
		mControlPoints[4] = new ControlPoint(10,1.80f,0,360);

		mCurrentLife = 0;
		//mBaseSpeed = 0;
		mStepTime = 0;
		//mPosX = 0;
		mPosY = 0;
		mPosZ = 0;
		mRotationX = 0;
		mRotationY = 0;
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
		//Vector3 a = Quaternion.Euler(new Vector3(30,90,0)) * Vector3.forward;
		//this.transform.eulerAngles = a;
	}

	// Update is called once per frame
	void Update () {
		//return;
		mStepTime = 0;
		for(int i = 0; i < 5; i ++)
		{
			mStepTime += mControlPoints[i].time;
			if(mCurrentLife <= mStepTime)
			{
				mCurrentStep = i;
				break;
			}
		}


		mCurrentLife += Time.deltaTime * mSpeedScaleFactor;

		this.transform.Rotate(Vector3.right * Time.deltaTime * mSpeedScaleFactor  *  mControlPoints[mCurrentStep].rChangeX / mControlPoints[mCurrentStep].time);
		this.transform.Rotate(Vector3.up * Time.deltaTime * mSpeedScaleFactor * mControlPoints[mCurrentStep].rChangeY / mControlPoints[mCurrentStep].time,Space.World);
		this.transform.Translate(Vector3.forward * Time.deltaTime * mSpeedScaleFactor * mBaseSpeed  * mControlPoints[mCurrentStep].speedFactor);
		//print ("fish1" + this.transform.rotation.ToString());

//		if(frameCnt == 3)
//		{
			lineRender.SetPosition(posIndex,this.transform.position);
			posIndex ++;
			frameCnt = 0;
			//print (posIndex);
//		}
		frameCnt ++;

//		Vector3 pos = P0 * (-0,5*t*t*t + t*t – 0,5*t) +
//			
//			P1 * (1,5*t*t*t - 2,5*t*t + 1,0) +
//				
//				P2 * (-1,5*t*t*t + 2,0*t*t + 0,5*t) +
//				
//				P3 * (0,5*t*t*t – 0,5*t*t);
	}
}
