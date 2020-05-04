using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedPlayerOI : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject avatar;
    public GameObject handL;
    public GameObject handR;
    public Transform playerGlobal;
    public Transform playerLocal;
    public Transform handLLocal;
    public Transform handRLocal;

    private void Start()
    {
        Debug.Log("NetworkedPlayer Start");

        if (photonView.IsMine)
        {
            playerGlobal = GameObject.Find("OVRPlayerController").transform;
            playerLocal = playerGlobal.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor");
            handLLocal = playerGlobal.Find("OVRCameraRig/TrackingSpace/LeftHandAnchor");
            handRLocal = playerGlobal.Find("OVRCameraRig/TrackingSpace/RightHandAnchor");

            transform.SetParent(playerLocal);
            transform.localPosition = Vector3.zero;

            avatar.SetActive(false);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(playerGlobal.position);
            stream.SendNext(playerGlobal.rotation);
            stream.SendNext(playerLocal.localPosition);
            stream.SendNext(playerLocal.localRotation);
            stream.SendNext(handLLocal.localPosition);
            stream.SendNext(handLLocal.localRotation);
            stream.SendNext(handRLocal.localPosition);
            stream.SendNext(handRLocal.localRotation);
        }
        else
        {
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
            avatar.transform.localPosition = (Vector3)stream.ReceiveNext();
            avatar.transform.localRotation = (Quaternion)stream.ReceiveNext();
            handL.transform.localPosition = (Vector3)stream.ReceiveNext();
            handL.transform.localRotation = (Quaternion)stream.ReceiveNext();
            handR.transform.localPosition = (Vector3)stream.ReceiveNext();
            handR.transform.localRotation = (Quaternion)stream.ReceiveNext();

        }
    }
}