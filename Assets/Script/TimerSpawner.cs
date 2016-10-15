using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class TimerSpawner : NetworkBehaviour {
    public GameObject timerPrefab;

    public override void OnStartServer() {
        Debug.Log(this.name + " im server");
        GameObject aa = Instantiate(timerPrefab) as GameObject;
        NetworkServer.Spawn(aa);
    }
}
