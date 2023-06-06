using UnityEngine;
using UnityEngine.UI;

namespace Mirror.Examples.Pong
{
    public class Ball : NetworkBehaviour
    {
        public float speed = 30f;
        public Rigidbody2D rigidbody2d;
        public int playerScore { get; set; }
        public int playerScore2 { get; set; }
        public AudioSource collis;
        public override void OnStartServer()
        {
            base.OnStartServer();
            rigidbody2d.simulated = true;
            playerScore = 0;
            playerScore2 = 0;
            Text p1 = GameObject.Find("Canvas/Player Score").GetComponent<Text>();
            p1.text = playerScore.ToString();
            Text p2 = GameObject.Find("Canvas/Player Score2").GetComponent<Text>();
            p2.text = playerScore2.ToString();
            rigidbody2d.velocity = Vector2.right * speed;
        }

        float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
        {
            return (ballPos.y - racketPos.y) / racketHeight;
        }
        void endP1()
        {
            Text EndP1 = GameObject.Find("Canvas/EndP1").GetComponent<Text>();
            EndP1.text = "WIN";
            Text EndP2 = GameObject.Find("Canvas/EndP2").GetComponent<Text>();
            EndP2.text = "LOSE";
            transform.position = Vector3.zero;
            rigidbody2d.velocity = Vector2.right * 0;
        }
        void endP2()
        {
            Text EndP1 = GameObject.Find("Canvas/EndP1").GetComponent<Text>();
            EndP1.text = "LOSE";
            Text EndP2 = GameObject.Find("Canvas/EndP2").GetComponent<Text>();
            EndP2.text = "WIN";
            transform.position = Vector3.zero;
            rigidbody2d.velocity = Vector2.right * 0;
        }
        [ServerCallback]
        void OnCollisionEnter2D(Collision2D col)
        {
            if (!(col.gameObject.name == "WallLeft" || col.gameObject.name == "WallRight"))
            {
                collis.Play();
                AudioPlayer();
            }

            if (col.gameObject.name == "WallLeft")
            {
                Text p1 = GameObject.Find("Canvas/Player Score2").GetComponent<Text>();
                playerScore++;
                p1.text = playerScore.ToString();
                synchro(playerScore, playerScore2);
                speed = 30f;
                transform.position = Vector3.zero;
                rigidbody2d.velocity = Vector2.left * speed;
                if (playerScore2 == 5)
                {
                    endP1();
                    synchroEnd(playerScore, playerScore2);
                }
                if (playerScore == 5)
                {
                    endP2();
                    synchroEnd(playerScore, playerScore2);
                }
                return;
            }
            else if (col.gameObject.name == "WallRight")
            {
                Text p2 = GameObject.Find("Canvas/Player Score").GetComponent<Text>();
                playerScore2++;
                p2.text = playerScore2.ToString();
                synchro(playerScore, playerScore2);
                speed = 30f;
                transform.position = Vector3.zero;
                rigidbody2d.velocity = Vector2.right * speed;
                if (playerScore2 == 5)
                {
                    endP1();
                    synchroEnd(playerScore, playerScore2);
                }
                if (playerScore == 5)
                {
                    endP2();
                    synchroEnd(playerScore, playerScore2);
                }
            }

            if (col.transform.GetComponent<Player>())
            {
                float y = HitFactor(transform.position,
                                    col.transform.position,
                                    col.collider.bounds.size.y);

                float x = col.relativeVelocity.x > 0 ? 1 : -1;

                Vector2 dir = new Vector2(x, y).normalized;
                if (speed < 150)
                {
                    speed = speed + 10;
                }

                rigidbody2d.velocity = dir * speed;
            }
        }


        [ClientRpc]
        void synchro(int s1, int s2)
        {
            Text p1 = GameObject.Find("Canvas/Player Score2").GetComponent<Text>();
            Text p2 = GameObject.Find("Canvas/Player Score").GetComponent<Text>();
            p1.text = s1.ToString();
            p2.text = s2.ToString();
        }
        [ClientRpc]
        void synchroEnd(int ps, int ps2)
        {
            if (ps2 == 5)
            {
                Text EndP1 = GameObject.Find("Canvas/EndP1").GetComponent<Text>();
                EndP1.text = "WIN";
                Text EndP2 = GameObject.Find("Canvas/EndP2").GetComponent<Text>();
                EndP2.text = "LOSE";
            }
            if (ps == 5)
            {
                Text EndP1 = GameObject.Find("Canvas/EndP1").GetComponent<Text>();
                EndP1.text = "LOSE";
                Text EndP2 = GameObject.Find("Canvas/EndP2").GetComponent<Text>();
                EndP2.text = "WIN";
            }

        }
        [ClientRpc]
        void AudioPlayer()
        {
            collis.Play();
        }
    }
}
