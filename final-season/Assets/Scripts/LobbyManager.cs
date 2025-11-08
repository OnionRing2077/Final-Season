using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Collections.Generic;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomNameInput;
    public Transform roomListContainer;
    public GameObject roomListItemPrefab; // UI Prefab แสดงชื่อห้อง

    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();

    public void OnClickCreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInput.text)) return;

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 10;
        PhotonNetwork.CreateRoom(roomNameInput.text, options);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform child in roomListContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (RoomInfo room in roomList)
        {
            if (room.RemovedFromList) continue;

            GameObject item = Instantiate(roomListItemPrefab, roomListContainer);
            item.GetComponentInChildren<TMP_Text>().text = room.Name;
            item.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => JoinRoom(room.Name));
        }
    }

    void JoinRoom(string name)
    {
        PhotonNetwork.JoinRoom(name);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("✅ Joined Room: " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel("Game"); // เข้าฉากเกม
    }
}
