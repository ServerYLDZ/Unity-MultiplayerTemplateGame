using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class ServerMannager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
     

    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
      
        Debug.Log("Servere baglandi");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Lobiye baglandi");
        PhotonNetwork.JoinOrCreateRoom("room1", new RoomOptions { MaxPlayers = 4, IsOpen = true, IsVisible = true }, TypedLobby.Default);
        
       

    }
    public override void OnJoinedRoom()
    {
        int index = 0;
        base.OnJoinedRoom();
        Debug.Log("odaya girildi");
        GameObject go = PhotonNetwork.Instantiate("kare", Vector3.zero, Quaternion.identity, 0, null);
    
        if(index %2==0)
            go.GetComponent<Gammer>().my_rol = Gammer.rol.killer;
        else
            go.GetComponent<Gammer>().my_rol = Gammer.rol.victom;
        index++;
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log("random odaya girilemedi");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("odaya girilemedi");
    }
}
