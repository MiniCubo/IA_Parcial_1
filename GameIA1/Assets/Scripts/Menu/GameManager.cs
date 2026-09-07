using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI thievesText; 
    public GameObject hudPanel;    
    public GameObject endGamePanel; 

    [Header("Scene Settings")]
    public string mainMenuSceneName = "MainMenu"; 

    private bool gameEnded = false; 

    void Update()
    {
        if (gameEnded) return;

        int remainingThieves = SingletonActors.Instance.GetThievesCount();
        thievesText.text = $"Thieves Missing:\n{remainingThieves}";

        if (remainingThieves <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        gameEnded = true; 
        
        if (hudPanel != null)
        {
            hudPanel.SetActive(false); 
        }

        endGamePanel.SetActive(true); 
        Time.timeScale = 0f; 
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(mainMenuSceneName);
    }
}