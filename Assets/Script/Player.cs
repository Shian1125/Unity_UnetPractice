using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;


/*
    �`����y�{: 
    1.playerA(client)���UA��
    2.playerA �q�� playerB(server)����CmdPlusHp()
    3.playerB �q����client��playerA���� RpcHpUI(float hp)
    4.�C�@��client����playerA��hp���[�F10
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

    //[Command]:�u�|�bserver�ݰ��檺function
    [Command]
    void CmdPlusHp(int hp) {

        RpcHpUI(hp);
        Debug.Log("only server");
    }

    //[ClientRpc]:server�q����client��playerA�����function�A�G�u�bserver�ݰ���ɤ~���ġA���i�qclient�ݪ����I�s
    [ClientRpc]
    public void RpcHpUI(float hp) {
        curHp += hp;
        _slider.value = curHp / tolHp;
        Debug.Log(this.name + " plus " + hp + "pt");
    }

    //��l�P�B�ثe�ж����Ҧ�client���A
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
