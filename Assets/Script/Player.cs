using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;


/*
    流程: 
    1.playerA(client)點擊A鍵
    2.playerA 通知 playerB(server)執行CmdPlusHp()
    3.playerB 通知所有client中的playerA執行RpcHpUI(float hp)
    4.所有client中的playerA的hp都+10
*/

public class Player : NetworkBehaviour {
    public Slider _slider;
    [SyncVar]
    public float curHp = 0;
    public float tolHp = 100;
    public GameObject timerPrefab;

    void Awake() {
        ReFreashAllClientState();
    }

    void Start () {
        if (!isLocalPlayer) {
            //this.transform.localPosition = new Vector3(600, 0, 0);
            this.name = "Player_other";
        } else {

            this.name = "Player_self";
            //CmdRefreshStat();
        }
    }

    // Update is called once per frame
    void Update () {
        if (isLocalPlayer) {
            if (Input.GetKeyDown(KeyCode.A)) {
                CmdPlusHp(10);
            }
        }
    }

    //[Command]:只有server端執行的function
    [Command]
    void CmdPlusHp(int hp) {

        RpcHpUI(hp);
        Debug.Log("only server");
    }

    //[ClientRpc]:server端讓所有client端的playerA執行此function。因為所有client都只有連向server，故必須透過server不能直接命令其他client端執行
    [ClientRpc]
    public void RpcHpUI(float hp) {
        curHp += hp;
        _slider.value = curHp / tolHp;
        Debug.Log(this.name + " plus " + hp + "pt");
    }

    //讓剛加入連線(房間)的client同步房間內其他client的狀況
    void ReFreashAllClientState() {
        Player[] players = Object.FindObjectsOfType<Player>();
        for (int i = 0; i < players.Length; i++) {
            //wayA
            //players[i].RpcHpUI(0);    
            //wayB
            players[i]._slider.value = players[i].curHp / players[i].tolHp;
            Debug.Log(players[i].name + " is refresh");
        }
    }
}
