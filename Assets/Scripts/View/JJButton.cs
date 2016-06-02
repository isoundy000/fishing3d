using UnityEngine;
using System.Collections;
using LuaInterface;
using UnityEngine.UI;

public class JJButton : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddClickCallback(LuaTable table , LuaFunction luafunc)
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(
                delegate()
                {
                    luafunc.Call(table,this.gameObject);
                }
        );
    }
}
