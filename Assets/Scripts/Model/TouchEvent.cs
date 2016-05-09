using UnityEngine;
using System.Collections;

public class TouchEvent : MonoBehaviour {

    public Camera uiCamera;
    bool isTouched = false;
    public Cannon cannon;
    public Transform uiRoot;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    public void OnPressed()
    {
        isTouched = true;
    }

    public void OnReleased()
    {
        isTouched = false;
        
        float angle = MathUtil.Angle(Input.mousePosition, new Vector3(0, -315));
        cannon.transform.eulerAngles = new Vector3(0, 0, angle);
        cannon.FireAction();
        Bullet bullet = (Instantiate(Resources.Load("BulletPrefabs/Bullet")) as GameObject).GetComponent<Bullet>();
        bullet.Angle = cannon.transform.eulerAngles.z;
        bullet.transform.parent = UIManager.GetInstance().WeaponRoot;
        bullet.transform.localPosition = cannon.transform.localPosition;
        bullet.transform.localScale = Vector3.one;
    }

    
}
