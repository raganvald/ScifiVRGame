using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkControllerOI : MonoBehaviourPunCallbacks
{

    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    [SerializeField]
    private byte maxPlayersPerRoom = 4;


    string gameVersion = "1";


    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        Connect();
    }


    public void Connect()
    {
        if(PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }






    //string _room = "Tutorial_Convrge";

    //void Start()
    //{
    //    PhotonNetwork.ConnectUsingSettings();
    //}

    //void OnJoinedLobby()
    //{
    //    Debug.Log("joined lobby");

    //    RoomOptions roomOptions = new RoomOptions() { };
    //    PhotonNetwork.JoinOrCreateRoom(_room, roomOptions, TypedLobby.Default);
    //}

    //void OnJoinedRoom()
    //{
    //    PhotonNetwork.Instantiate("", Vector3.zero, Quaternion.identity, 0);
    //}



    public override void OnConnectedToMaster()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");

        PhotonNetwork.JoinRandomRoom();
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        //PhotonNetwork.CreateRoom(null, new RoomOptions());
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");


        PhotonNetwork.Instantiate("NetworkedPlayerOI", Vector3.zero, Quaternion.identity, 0);

    }



}
