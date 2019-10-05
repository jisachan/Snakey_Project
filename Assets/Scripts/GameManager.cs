using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject block;
    Button button;
    Text buttonText;
    Text gameOverText;
    Text currentScore;
    Text highScore;
    int highScoreINT;
    int tempScore;
    int score = 0;
    float i;
    Vector3 spawnPosition;

    public int Score
    {
        get { return score; }
        set { score = value; }
    }
    void Start()
    {
        // Setting the UI elements and hiding those which should be hidden until activated.
        button = GameObject.Find("RetryButton").GetComponent<Button>();
        button.gameObject.SetActive(false);
        gameOverText = GameObject.Find("GameOverText").GetComponent<Text>();
        gameOverText.text = "";
        currentScore = GameObject.Find("CurrentScore").GetComponent<Text>();
        highScore = GameObject.Find("HighScore").GetComponent<Text>();
        // Gets the high score saved in the GetHighScore method.
        highScoreINT = PlayerPrefs.GetInt("highScoreINT", highScoreINT);
    }
    void Update()
    {
        currentScore.text = "Score: " + score.ToString();
        GetHighScore();

        // Spawns a new block every 3 seconds
        if (Time.timeSinceLevelLoad > i)
        {
            i += 3;
            // Prevents the first block from spawning until 4 seconds into the game.
            if (i > 5)
            {
                SpawnBlock();
            }
        }
    }
    public void GameOver()
    {
        gameOverText.text = "GAME OVER";
        button.gameObject.SetActive(true);
    }
    public GameObject SpawnBlock()
    {
        // Setting the spawn area parameters.
        float minX = -7.5f;
        float maxX = 7.5f;
        float minY = -3.7f;
        float maxY = 3.7f;

        int randomX = (int)Random.Range(minX, maxX);
        int randomY = (int)Random.Range(minY, maxY);

        spawnPosition = new Vector3(randomX, randomY, 0);

        GameObject[] blocks = GameObject.FindGameObjectsWithTag("block");
        GameObject foodable = GameObject.FindGameObjectWithTag("foodables");

        // Preventing blocks from spawning ontop of food items 
        // by returning null if their spawn position is the same
        // and then recalling this SpawnBlock method.
        for (int i = 0; i < blocks.Length; i++)
        {
            if (blocks[i].transform.position == spawnPosition || spawnPosition == foodable.transform.position)
            {
                SpawnBlock();
                return null;
            }
        }
        return Instantiate(block, spawnPosition, Quaternion.identity);
    }
    public void GetHighScore()
    {
        if (score > highScoreINT)
        {
            highScoreINT = score;
        }
        highScore.text = "High Score: " + highScoreINT;

        // Saves the high score, even if the game would reset.
        PlayerPrefs.SetInt("highScoreINT", highScoreINT);
    }
}
