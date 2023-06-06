using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public Ball2 ball;
    public GameObject Panel;
    public TextMeshProUGUI Mess;

    public Paddle playerPaddle;
    public int playerScore { get; private set; }
    public Text playerScoreText;

    public Paddle computerPaddle;
    public int computerScore { get; private set; }
    public Text computerScoreText;
    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        SetPlayerScore(0);
        SetComputerScore(0);
        StartRound();
    }

    public void Stop()
    {
        SetPlayerScore(0);
        SetComputerScore(0);
    }

    public void StartRound()
    {
        if (playerScore == 5 || computerScore == 5)
        {
            if (playerScore == 5)
                Mess.text = "Win";
            else
                Mess.text = "Lose";
            Panel.SetActive(true);
            Stop();
            ball.ResetPosition();
            ball.StopBall();
        }
        else
        {
            playerPaddle.ResetPosition();
            computerPaddle.ResetPosition();
            ball.ResetPosition();
            ball.AddStartingForce();
        }
    }

    public void PlayerScores()
    {
        SetPlayerScore(playerScore + 1);
        StartRound();
    }

    public void ComputerScores()
    {
        SetComputerScore(computerScore + 1);
        StartRound();
    }

    private void SetPlayerScore(int score)
    {
        playerScore = score;
        playerScoreText.text = score.ToString();
    }

    private void SetComputerScore(int score)
    {
        computerScore = score;
        computerScoreText.text = score.ToString();
    }

}
