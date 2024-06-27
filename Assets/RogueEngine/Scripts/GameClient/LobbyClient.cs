using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Events;
using RogueEngine.UI;
using System.Threading.Tasks;

namespace RogueEngine.Client
{
    /// <summary>
    /// Main client script for the matchmaker and lobby
    /// Will send requests to server and receive a response when a matchmaking succeed or fail
    /// </summary>

    public class LobbyClient : MonoBehaviour
    {
        public UnityAction<MatchmakingResult> onMatchmaking;
        public UnityAction<LobbyList> onLobbyList;
        public UnityAction<LobbyGame> onLobbyRefresh;
        public UnityAction<string, string> onChat;

        private string host_ip;
        private bool matchmaking = false;
        private float timer = 0f;
        private float match_timer = 0f;
        private string matchmaking_group;
        private int matchmaking_players;
        private bool loading = false;

        private UnityAction<bool> connect_callback;

        private static LobbyClient _instance;

        void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            TcgNetwork.Get().onConnect += OnConnect;
            TcgNetwork.Get().onDisconnect += OnDisconnect;
            Messaging.ListenMsg("matchmaking", ReceiveMatchmaking);
            Messaging.ListenMsg("lobby_list", ReceiveLobbyList);
            Messaging.ListenMsg("lobby_refresh", ReceiveLobbyRefresh);
            Messaging.ListenMsg("lobby_chat", ReceiveLobbyChat);
        }

        private void OnDestroy()
        {
            if (TcgNetwork.Get() != null)
            {
                TcgNetwork.Get().onConnect -= OnConnect;
                TcgNetwork.Get().onDisconnect -= OnDisconnect;
                Messaging.UnListenMsg("matchmaking");
                Messaging.UnListenMsg("lobby_list");
                Messaging.UnListenMsg("lobby_refresh");
                Messaging.UnListenMsg("lobby_chat");
            }

            Disconnect(); //Disconnect when switching scene
        }

        void Update()
        {
            UserData udata = Authenticator.Get().UserData;
            if (matchmaking && udata != null)
            {
                timer += Time.deltaTime;
                match_timer += Time.deltaTime;

                //Send periodic request
                if (IsConnected() && timer > 2f)
                {
                    timer = 0f;
                    SendMatchRequest(udata.id, udata.username, matchmaking_group, matchmaking_players, true);
                }

                //Disconnected, stop
                if (!IsConnected() && !IsConnecting() && timer > 5f)
                {
                    StopMatchmaking();
                }
            }
        }

        public void StartMatchmaking(string group, int nb_players)
        {
            if (matchmaking)
                StopMatchmaking();

            Debug.Log("Start Matchmaking!");
            matchmaking_group = group;
            matchmaking_players = nb_players;
            matchmaking = true;
            match_timer = 0f;
            timer = 0f;

            UserData udata = Authenticator.Get().UserData;

            Connect(NetworkData.Get().url, NetworkData.Get().port, (bool success) =>
            {
                if (success)
                {
                    SendMatchRequest(udata.id, udata.username, group, nb_players, false);
                }
                else
                {
                    StopMatchmaking();
                }
            });
        }

        public void StopMatchmaking()
        {
            if (matchmaking)
            {
                Debug.Log("Stop Matchmaking!");
                onMatchmaking?.Invoke(null);
                matchmaking_group = "";
                matchmaking_players = 0;
                matchmaking = false;
            }
        }

        public void LoadLobbyList()
        {
            Connect(NetworkData.Get().url, NetworkData.Get().port, (bool success) =>
            {
                if (success)
                {
                    Messaging.SendEmpty("lobby_list", ServerID, NetworkDelivery.Reliable);
                }
            });
        }

        public async Task<string> LoadHostIP()
        {
            host_ip = await NetworkTool.GetOnlineIp();
            return host_ip;
        }

        public void KeepGame(string game_uid)
        {
            MsgLobbyUID msg = new MsgLobbyUID();
            msg.uid = game_uid;
            Messaging.SendObject("lobby_keep", ServerID, msg, NetworkDelivery.Reliable);
        }

        public void CreateGame(string title, string filename, bool load = false)
        {
            bool is_lan = NetworkData.Get().server_type == ServerType.PeerToPeer;
            if (is_lan && host_ip == null)
                return; //Host IP not loaded yet

            MsgLobbyCreate msg = new MsgLobbyCreate();
            msg.user_id = Authenticator.Get().UserID;
            msg.username = Authenticator.Get().Username;
            msg.title = title;
            msg.filename = filename;
            msg.load = load;
            msg.host_ip = is_lan ? host_ip : "";
            loading = true;

            Messaging.SendObject("lobby_create", ServerID, msg, NetworkDelivery.Reliable);
        }

        public void JoinGame(string game_uid)
        {
            MsgLobbyJoin msg = new MsgLobbyJoin();
            msg.user_id = Authenticator.Get().UserID;
            msg.username = Authenticator.Get().Username;
            msg.uid = game_uid;
            loading = true;

            Messaging.SendObject("lobby_join", ServerID, msg, NetworkDelivery.Reliable);
        }

        public void LeaveGame(string game_uid)
        {
            MsgLobbyJoin msg = new MsgLobbyJoin();
            msg.user_id = Authenticator.Get().UserID;
            msg.username = Authenticator.Get().Username;
            msg.uid = game_uid;
            loading = true;

            Messaging.SendObject("lobby_quit", ServerID, msg, NetworkDelivery.Reliable);
        }

        public void StartGame(string game_uid)
        {
            MsgLobbyUID msg = new MsgLobbyUID();
            msg.uid = game_uid;
            loading = true;

            Messaging.SendObject("lobby_start", ServerID, msg, NetworkDelivery.Reliable);
        }

        public void SendChat(string msg)
        {
            MsgChat cmsg = new MsgChat();
            cmsg.username = Authenticator.Get().Username;
            cmsg.msg = msg;

            Messaging.SendObject("lobby_chat", ServerID, cmsg, NetworkDelivery.Reliable);
        }

        public void Connect(string url, ushort port, UnityAction<bool> callback=null)
        {
            //Check if already connected
            if (IsConnected() || IsConnecting())
            {
                callback?.Invoke(IsConnected());
                return;
            }

            connect_callback = callback;
            TcgNetwork.Get().StartClient(url, port);
        }

        public void Disconnect()
        {
            TcgNetwork.Get()?.Disconnect();
        }

        private void OnConnect()
        {
            Debug.Log("Connected to server!");
            connect_callback?.Invoke(true);
            connect_callback = null;
        }

        private void OnDisconnect()
        {
            StopMatchmaking(); //Stop if currently running
            connect_callback?.Invoke(false);
            connect_callback = null;
            matchmaking = false;
            loading = false;
        }

        private void SendMatchRequest(string user_id, string username, string group, int nb_players, bool refresh)
        {
            MsgMatchmaking msg_match = new MsgMatchmaking();
            msg_match.user_id = user_id;
            msg_match.username = username;
            msg_match.group = group;
            msg_match.players = nb_players;
            msg_match.time = match_timer;
            msg_match.refresh = refresh;
            Messaging.SendObject("matchmaking", ServerID, msg_match, NetworkDelivery.Reliable);
        }

        private void ReceiveMatchmaking(ulong client_id, FastBufferReader reader)
        {
            reader.ReadNetworkSerializable(out MatchmakingResult msg);

            if (IsConnected() && matchmaking && matchmaking_group == msg.group)
            {
                matchmaking = !msg.success; //Stop matchmaking if success
                onMatchmaking?.Invoke(msg);
            }
        }

        private void ReceiveLobbyList(ulong client_id, FastBufferReader reader)
        {
            reader.ReadNetworkSerializable(out LobbyList list);
            onLobbyList?.Invoke(list);
        }

        private void ReceiveLobbyRefresh(ulong client_id, FastBufferReader reader)
        {
            loading = false;
            reader.ReadNetworkSerializable(out LobbyGame list);
            onLobbyRefresh?.Invoke(list);
        }

        private void ReceiveLobbyChat(ulong client_id, FastBufferReader reader)
        {
            reader.ReadNetworkSerializable(out MsgChat msg);
            onChat?.Invoke(msg.username, msg.msg);
        }

        public bool IsMatchmaking()
        {
            return matchmaking;
        }

        public bool IsLoading()
        {
            return loading;
        }

        public string GetGroup()
        {
            return matchmaking_group;
        }

        public int GetNbPlayers()
        {
            return matchmaking_players;
        }

        public float GetTimer()
        {
            return match_timer;
        }

        public bool IsConnected()
        {
            return TcgNetwork.Get().IsConnected();
        }

        public bool IsConnecting()
        {
            return TcgNetwork.Get().IsConnecting();
        }

        public ulong ServerID { get { return TcgNetwork.Get().ServerID; } }
        public NetworkMessaging Messaging { get { return TcgNetwork.Get().Messaging; } }

        public static LobbyClient Get()
        {
            return _instance;
        }
    }

}