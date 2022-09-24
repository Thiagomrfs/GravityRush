using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public int score { get; private set; }
    private GameObject spawner;

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
    private void Awake()
    {
        spawner = GameObject.FindWithTag("Spawner");
    }
}
