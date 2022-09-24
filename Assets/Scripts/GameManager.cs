using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public int score { get; private set; }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
