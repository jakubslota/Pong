using UnityEngine;
using UnityEngine.UI;
/*
	Documentation: https://mirror-networking.gitbook.io/docs/components/network-manager
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkManager.html
*/

namespace Mirror.Examples.Pong
{
    // Custom NetworkManager that simply assigns the correct racket positions when
    // spawning players. The built in RoundRobin spawn method wouldn't work after
    // someone reconnects (both players would be on the same side).
    [AddComponentMenu("")]
    public class NetworkManagerPong : NetworkManager
    {
        public Transform leftRacketSpawn;
        public Transform rightRacketSpawn;
        GameObject ball;

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            // add player at correct spawn position
            Transform start = numPlayers == 0 ? leftRacketSpawn : rightRacketSpawn;
            GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
            NetworkServer.AddPlayerForConnection(conn, player);

            // spawn ball if two players
            if (numPlayers == 2)
            {
                ball = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Ball"));
                NetworkServer.Spawn(ball);
            }
        }

        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            // destroy ball
            if (ball != null)
                NetworkServer.Destroy(ball);
            Text p1 = GameObject.Find("Canvas/Player Score").GetComponent<Text>();
            p1.text = "0";
            Text p2 = GameObject.Find("Canvas/Player Score2").GetComponent<Text>();
            p2.text = "0";
            Text EndP1 = GameObject.Find("Canvas/EndP1").GetComponent<Text>();
            EndP1.text = "";
            Text EndP2 = GameObject.Find("Canvas/EndP2").GetComponent<Text>();
            EndP2.text = "";
            // call base functionality (actually destroys the player)
            base.OnServerDisconnect(conn);
        }
    }
}
