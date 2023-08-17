using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class Menu : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject ButtonsPanel = null;
   
    [SerializeField] private GameObject RoomStatusPnael=null;
    [SerializeField] private TextMeshProUGUI waitingStatusText = null;

    [SerializeField] private TMP_InputField joinRoomInputField = null;
    [SerializeField] private TMP_InputField createRoomInputField = null;

    public TextMeshProUGUI playernames = null;
    public Button startButton;
    public Text roomName;
   
    

    private const string GameVersion = "0.1";
    private const int MaxPlayerPerRoom = 2;
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = GameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }
  
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        ButtonsPanel.SetActive(true);
        RoomStatusPnael.SetActive(false);
        waitingStatusText.text = "Room Can't create plese enter diferent RoomName...";
    }
    public void CreateRoom()
    {



        ButtonsPanel.SetActive(false);
       

      
    }
    public void CreatingRoomAndJoin()
    {
        if (!string.IsNullOrEmpty(createRoomInputField.text))
        {
            roomName.text = createRoomInputField.text;
            PhotonNetwork.CreateRoom(createRoomInputField.text, new RoomOptions { MaxPlayers = MaxPlayerPerRoom, IsOpen = true, IsVisible = true });
            waitingStatusText.text = "Room Creating...";
        }
    }
    public void JoinReady()
    {
        PhotonNetwork.GameVersion = GameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }
    public void JoinInputRoom()
    {
        if (!string.IsNullOrEmpty(joinRoomInputField.text) && PhotonNetwork.IsConnected)
        {
            roomName.text = joinRoomInputField.text;
            PhotonNetwork.JoinRoom(joinRoomInputField.text);
            waitingStatusText.text = "Room Joinned...";
        }

    }

    public void setupOppenent()
    {
  
        ButtonsPanel.SetActive(false);

        waitingStatusText.text = "Searching.....";
      //  PhotonNetwork.JoinRandomOrCreateRoom(null, MaxPlayerPerRoom, MatchmakingMode.FillRoom, null, null, "PhotonNetwork.NickName", new RoomOptions { MaxPlayers = MaxPlayerPerRoom, IsOpen = true, IsVisible = true }, null);
        
        PhotonNetwork.JoinRandomRoom();
     

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Server Joined");
      
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        ButtonsPanel.SetActive(true);
        RoomStatusPnael.SetActive(false);
        Debug.Log($"Disconected  due to :{cause}");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No cillent are waiting for an opponent, Createing a new room");
  
        roomName.text = PhotonNetwork.NickName + "'s Room";
        PhotonNetwork.CreateRoom(PhotonNetwork.NickName, new RoomOptions { MaxPlayers = MaxPlayerPerRoom });
            
    }
    
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        ButtonsPanel.SetActive(true);
        RoomStatusPnael.SetActive(false);
        waitingStatusText.text = "Room has'nt Found...";
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Cilent sucssesfuly joined room");
        RoomStatusPnael.SetActive(true);
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        RoomRefresh();
        

        if (playerCount!=MaxPlayerPerRoom)
        {
            
            waitingStatusText.text = "Other opponents Waiting...";
            
        }
        else
        {
            waitingStatusText.text = "Opponents are Found...";
            Debug.Log("Match is Ready to start");
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        RoomRefresh();
       

        if (PhotonNetwork.CurrentRoom.PlayerCount==MaxPlayerPerRoom)
        {
            startButton.interactable = true;
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            waitingStatusText.text = "Opponents are Found...";

        }
        else
        startButton.interactable = false;
    }
  

    private void RoomRefresh()
    {

   
        roomName.text = PhotonNetwork.CurrentRoom.Name;
      
        playernames.text = "";
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            if (PhotonNetwork.PlayerList[i].IsMasterClient)
                playernames.text += "\n" + PhotonNetwork.PlayerList[i].NickName+"(M)";//ekleme
            else
            playernames.text += "\n" + PhotonNetwork.PlayerList[i].NickName;//ekleme
        }
    }
    public void LeftRoom()
    {
        PhotonNetwork.LeaveRoom();
        Debug.Log("odadan ayrıld");
    }
    public override void OnLeftRoom()
    {
       
        ButtonsPanel.SetActive(true);
        RoomStatusPnael.SetActive(false);
        Debug.Log("biri odadan ayrıldı");
        waitingStatusText.text = "";
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        waitingStatusText.text = "Other opponents Waiting...";
        PhotonNetwork.SetMasterClient(otherPlayer);
        RoomRefresh();
        base.OnPlayerLeftRoom(otherPlayer);
        PhotonNetwork.CurrentRoom.IsOpen = true;
        PhotonNetwork.CurrentRoom.IsVisible = true;
        startButton.interactable = false;
    }
    
    public void gameStart()
    {
        waitingStatusText.text = "Starting...";
        PhotonNetwork.LoadLevel("SampleScene");//sahne yüklensin
       
    }
}
