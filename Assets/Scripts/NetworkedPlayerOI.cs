using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedPlayerOI : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject networkPlayerHead, networkPlayerLHand, networkPlayerRHand;
    Transform playerHead, playerLHand, playerRHand;

    private void Start()
    {
        if (photonView.IsMine)
        {
            playerHead = GameObject.Find("CenterEyeAnchor").transform;
            playerLHand = GameObject.Find("LeftControllerAnchor").transform;
            playerRHand = GameObject.Find("RightControllerAnchor").transform;

            networkPlayerHead.transform.SetParent(playerHead);
            networkPlayerLHand.transform.SetParent(playerLHand);
            networkPlayerRHand.transform.SetParent(playerRHand);

            networkPlayerHead.SetActive(false);
            networkPlayerLHand.SetActive(false);
            networkPlayerRHand.SetActive(false);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(playerHead.position);
            stream.SendNext(playerHead.rotation);

            stream.SendNext(playerLHand.position);
            stream.SendNext(playerLHand.rotation);

            stream.SendNext(playerRHand.position);
            stream.SendNext(playerRHand.rotation);
        }
        else
        {
            networkPlayerHead.transform.position = (Vector3)stream.ReceiveNext();
            networkPlayerHead.transform.rotation = (Quaternion)stream.ReceiveNext();

            networkPlayerLHand.transform.position = (Vector3)stream.ReceiveNext();
            networkPlayerLHand.transform.rotation = (Quaternion)stream.ReceiveNext();

            networkPlayerRHand.transform.position = (Vector3)stream.ReceiveNext();
            networkPlayerRHand.transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
