using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedPlayer : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject avatar;
    //public Transform playerGlobal;
    public Transform playerLocal;

    private void Start()
    {
        Debug.Log("NetworkedPlayer Start");

        if (photonView.IsMine)
        {
            GameObject trackedAlias = GameObject.Find("TrackedAlias");

            //playerGlobal = GameObject.Find("OVRPlayerController").transform;
            //playerLocal = playerGlobal.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor");

            //playerGlobal = trackedAlias.transform.Find("Aliases/PlayAreaAlias").transform;
            playerLocal = trackedAlias.transform.Find("Aliases/HeadsetAlias").transform;

            transform.SetParent(playerLocal);
            transform.localPosition = Vector3.zero;

            avatar.SetActive(false);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            //stream.SendNext(playerGlobal.position);
            //stream.SendNext(playerGlobal.rotation);
            stream.SendNext(playerLocal.position);
            stream.SendNext(playerLocal.rotation);
        }
        else
        {
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
            //avatar.transform.localPosition = (Vector3)stream.ReceiveNext();
            //avatar.transform.localRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
