using Photon.Pun;
using UnityEngine;

public class NetworkedGun : MonoBehaviourPunCallbacks, IPunObservable
{
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (photonView.Owner != PhotonNetwork.LocalPlayer)
            photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
    }
}
