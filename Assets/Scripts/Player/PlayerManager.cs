using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    public static bool isGameOver;
    public static Vector2 lastCheckPointPos = new Vector2(-11, 0);
    public static int numberOfCoins;

    public GameObject gameOverScreen;
    public GameObject pauseMenuScreen;

    public TextMeshProUGUI coinsText;

    public CinemachineVirtualCamera vCam;
    public GameObject[] playerPrefabs;
    private int characterIndex;

    private bool hasHandledGameOver = false;
    private int previousCoinCount;

    public void Awake()
    {
        if (playerPrefabs == null || playerPrefabs.Length == 0)
        {
            Debug.LogError("Player prefabs are not assigned or empty.");
            return;
        }

        characterIndex = PlayerPrefs.GetInt("SelectedPlayer", 0);
        if (characterIndex < 0 || characterIndex >= playerPrefabs.Length)
        {
            Debug.LogWarning("SelectedPlayer index is out of bounds. Using default index 0.");
            characterIndex = 0;
        }

        GameObject player = Instantiate(playerPrefabs[characterIndex], lastCheckPointPos, Quaternion.identity);
        vCam.m_Follow = player.transform;

        numberOfCoins = PlayerPrefs.GetInt("NumberOfCoins", 0);
        isGameOver = false;
        previousCoinCount = numberOfCoins;
        Debug.Log("SelectedPlayer index: " + characterIndex);
        Debug.Log("Instantiating prefab: " + playerPrefabs[characterIndex].name);
    }

    void Update()
    {
        if (numberOfCoins != previousCoinCount)
        {
            coinsText.text = numberOfCoins.ToString();
            previousCoinCount = numberOfCoins;
        }

        if (isGameOver && !hasHandledGameOver)
        {
            hasHandledGameOver = true;
            HandleGameOver();
        }
    }

    private void HandleGameOver()
    {
        Time.timeScale = 0; // Pause the game
        gameOverScreen.SetActive(true);
    }

    public void ReplayLevel()
    {
        Time.timeScale = 1; // Reset time scale
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenuScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuScreen.SetActive(false);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1; // Reset time scale
        SceneManager.LoadScene("Menu");
    }
}
