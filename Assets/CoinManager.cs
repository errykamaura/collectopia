using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public int coinCount = 0;              
    public int totalCoins = 0;              
    public TMP_Text coinText;               
    public GameObject successPanel;         

    private bool gameEnded = false;         

    void Start()
    {
        UpdateCoinUI();
        if (successPanel != null)
        {
            successPanel.SetActive(false);  // Sembunyikan UI gambar di awal
        }
    }

    void Update()
    {
        UpdateCoinUI();

        if (!gameEnded && coinCount >= totalCoins)
        {
            gameEnded = true;
            if (successPanel != null)
            {
                successPanel.SetActive(true);  // Tampilkan gambar "Game Berhasil"
            }
            Debug.Log("Semua koin terkumpul. Game berhasil!");
        }
    }

    public void AddCoin()
    {
        coinCount++;
    }

    void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = ":" + coinCount.ToString();
        }
    }
}
