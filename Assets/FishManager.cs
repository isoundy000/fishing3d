using UnityEngine;
using System.Collections;

public class FishManager : MonoBehaviour {
	public GameObject[] fishes;

	// Use this for initialization
	IEnumerator Start ()
	{
		for(int i = 0; i < 100; i ++)
		{
			int randonmIndex = Random.Range(0,1);
			GameObject fish = (GameObject)GameObject.Instantiate(fishes[randonmIndex]);
			int flag = (i % 2 == 0 ? 1 : -1);
			fish.transform.eulerAngles = new Vector3(0,Random.Range(-flag * 45,-flag * 135),0);
			float z = Random.Range(50,100);
			float t = Camera.main.aspect;
			float x =  flag * 1.0278732f * z;
			fish.transform.localPosition = new Vector3(x,0,z);
			Fish fishCom = fish.GetComponent<Fish>();
			fishCom.mPosX = x;
			fishCom.mPosZ = z;
			fishCom.mRotationX = 0;
			fishCom.mRotationY = fish.transform.eulerAngles.y;
			yield return new WaitForSeconds(0.1f);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{

	}
}
