using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace RogueEngine
{
    [System.Serializable]
    public class LobbyGame : INetworkSerializable
    {
        public string game_uid;
        public string game_url;

        public ServerType server_type;
        public string filename;   //Save file name
        public bool load;         //Load or new game ?

        public string scenario;
        public string title;        //Game room title

        public int players_max = 0;
        public ulong last_update = 0;
        public bool expired;
        public bool started;

        public List<LobbyPlayer> players = new List<LobbyPlayer>();

        public LobbyGame() { }
        public LobbyGame(string uid) { game_uid = uid; }

        public bool CanBeJoined()
        {
            return !expired && !started && players.Count < players_max;
        }

        public LobbyPlayer GetPlayer(string user_id)
        {
            foreach (LobbyPlayer player in players)
            {
                if (player.user_id == user_id)
                    return player;
            }
            return null;
        }

        public bool HasPlayer(string user_id)
        {
            return GetPlayer(user_id) != null;
        }

        public bool IsHost(string user_id)
        {
            if(players.Count > 0)
                return players[0].user_id == user_id;
            return false;
        }

        public void AddPlayer(LobbyPlayer player)
        {
            if (HasPlayer(player.user_id))
                return; //ID already connected

            RemovePlayer(player.user_id); //Remove dupplicate

            player.game_uid = game_uid;
            players.Add(player);
        }

        public void RemovePlayer(string user_id)
        {
            for (int i = players.Count - 1; i >= 0; i--)
            {
                if (players[i].user_id == user_id)
                {
                    players[i].game_uid = "";
                    players.RemoveAt(i);
                }
            }
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref game_uid);
            serializer.SerializeValue(ref game_url);
            serializer.SerializeValue(ref server_type);
            serializer.SerializeValue(ref filename);
            serializer.SerializeValue(ref load);
            serializer.SerializeValue(ref title);
            serializer.SerializeValue(ref scenario);
            serializer.SerializeValue(ref players_max);
            serializer.SerializeValue(ref last_update);
            serializer.SerializeValue(ref expired);
            serializer.SerializeValue(ref started);

            if (serializer.IsReader)
            {
                int count = 0;
                serializer.SerializeValue(ref count);
                for (int i = 0; i < count; i++)
                {
                    LobbyPlayer player = new LobbyPlayer();
                    serializer.SerializeNetworkSerializable(ref player);
                    players.Add(player);
                }
            }

            if (serializer.IsWriter)
            {
                int count = players.Count;
                serializer.SerializeValue(ref count);
                for (int i = 0; i < count; i++)
                {
                    LobbyPlayer player = players[i];
                    serializer.SerializeNetworkSerializable(ref player);
                }
            }
        }
    }

    [System.Serializable]
    public class LobbyPlayer : INetworkSerializable
    {
        public ulong client_id;
        public string user_id;
        public string username;
        public string game_uid;

        public LobbyPlayer() { }
        public LobbyPlayer(ulong cid, string uid, string user) { client_id = cid; user_id = uid; username = user; }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref client_id);
            serializer.SerializeValue(ref user_id);
            serializer.SerializeValue(ref username);
            serializer.SerializeValue(ref game_uid);
        }

    }

}
