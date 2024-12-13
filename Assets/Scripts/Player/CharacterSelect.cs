using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelect : MonoBehaviour
{
    public GameObject[] skins;
    public int selectedCharacter;
    public Character[] characters;

    public Button unlockButton;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI unlockButtonText;

    private int coins;

    private void Awake()
    {
        if (skins == null || skins.Length == 0 || characters == null || characters.Length == 0)
        {
            Debug.LogError("Skins or characters array is not assigned or empty.");
            return;
        }

        selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);
        coins = PlayerPrefs.GetInt("NumberOfCoins", 0);

        foreach (GameObject player in skins) player.SetActive(false);
        skins[selectedCharacter].SetActive(true);

        foreach (Character c in characters)
        {
            c.isUnlocked = c.price == 0 || PlayerPrefs.GetInt(c.name, 0) == 1;
        }

        UpdateUI();
    }

    public void ChangeNext()
    {
        skins[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % skins.Length;
        skins[selectedCharacter].SetActive(true);

        if (characters[selectedCharacter].isUnlocked)
        {
            PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
            PlayerPrefs.Save(); // Explicitly save the changes to PlayerPrefs
            Debug.Log("Saved SelectedPlayer: " + PlayerPrefs.GetInt("SelectedPlayer", 0));
        }

        UpdateUI();
    }

    public void ChangePrevious()
    {
        skins[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter - 1 + skins.Length) % skins.Length;
        skins[selectedCharacter].SetActive(true);

        if (characters[selectedCharacter].isUnlocked)
        {
            PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
            PlayerPrefs.Save(); // Explicitly save the changes to PlayerPrefs
            Debug.Log("Saved SelectedPlayer: " + PlayerPrefs.GetInt("SelectedPlayer", 0));
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        coinsText.text = "Coins: " + coins;
        if (characters[selectedCharacter].isUnlocked)
        {
            unlockButton.gameObject.SetActive(false);
        }
        else
        {
            unlockButtonText.text = "Price: " + characters[selectedCharacter].price;
            unlockButton.interactable = coins >= characters[selectedCharacter].price;
            unlockButton.gameObject.SetActive(true);
        }
    }

    public void Unlock()
    {
        int price = characters[selectedCharacter].price;
        if (coins >= price)
        {
            coins -= price;
            PlayerPrefs.SetInt("NumberOfCoins", coins);
            PlayerPrefs.SetInt(characters[selectedCharacter].name, 1);
            PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
            PlayerPrefs.Save(); // Explicitly save the changes to PlayerPrefs
            characters[selectedCharacter].isUnlocked = true;
            UpdateUI();
        }
        else
        {
            Debug.LogWarning("Not enough coins to unlock this character!");
        }
    }
}
