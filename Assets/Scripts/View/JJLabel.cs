using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JJLabel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public string Text
    {
        get 
        {
            Text text = this.GetComponent<Text>();
            return text.text;
        }
        set
        {
            Text text = this.GetComponent<Text>();
            text.text = value;
            print(text.font.name);
        }
    }
}
