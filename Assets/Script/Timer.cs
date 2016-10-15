using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
public class Timer : NetworkBehaviour {

    [SyncVar] public float curTime;     //[SyncVar]會以server端的數據為準
    public Text timeTxt;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        TimerCount();
    }
    void TimerCount() {
        curTime += Time.deltaTime;
        timeTxt.text = Mathf.FloorToInt(curTime).ToString();
    }
}
