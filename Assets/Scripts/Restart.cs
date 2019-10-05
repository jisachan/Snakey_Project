using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void RestartGame()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("block");

        // Removes the existing blocks when game is restarted.
        for (int i = 0; i < blocks.Length; i++)
        {
            Destroy(blocks[i]);
        }
        // Reloads the scene, which in this case restarts the game.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

}