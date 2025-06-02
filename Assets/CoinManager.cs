using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public int coinCount = 0;              
    public int totalCoins = 0;              
    public TMP_Text coinText;               
    public GameObject successImage;         

    private bool gameEnded = false;         

    void Start()
    {
        UpdateCoinUI();
        if (successImage != null)
        {
            successImage.SetActive(false);  // Sembunyikan UI gambar di awal
        }
    }

    void Update()
    {
        UpdateCoinUI();

        if (!gameEnded && coinCount >= totalCoins)
        {
            gameEnded = true;
            if (successImage != null)
            {
                successImage.SetActive(true);  // Tampilkan gambar "Game Berhasil"
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
