using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class PhotonManager : MonoBehaviourPunCallbacks
{
    public string userName = "ANONYMOUS";
    public string objectName;

    public GUIStyle uStyle;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.GameVersion = "0.1";
        //一秒間に送るパケット数
        PhotonNetwork.SendRate = 20;
        //一秒間にシリアライズする回数。SendRateより多い値にすることはできません
        PhotonNetwork.SerializationRate = 20;
        //PhotonNetWarkクラスに、設定上のAppIDを代入
        PhotonNetwork.NetworkingClient.AppId =
            PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime;
        //PhotonNetWorkに接続
        PhotonNetwork.ConnectUsingSettings();
        uStyle = new GUIStyle();
        uStyle.fontSize = 30;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinOrCreateRoom("ROOM_NAME", new RoomOptions(), TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("RoomJoined.RoomName" + PhotonNetwork.CurrentRoom.Name +
            ",JoinedNum" + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    private void OnGUI()
    {
        //接続状態及びリージョンの表示
        string region = PhotonNetwork.CloudRegion ?? "";
        GUILayout.Label(PhotonNetwork.NetworkStatisticsToString() + "\n" + region, uStyle);
    }
}
