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
            successPanel.SetActive(false);  // Sembunyikan panel di awal
        }
    }

    void Update()
    {
        if (!gameEnded && coinCount >= totalCoins && totalCoins > 0)
        {
            gameEnded = true;
            if (successPanel != null)
                successPanel.SetActive(true);

            Debug.Log("Semua koin terkumpul!");
        }
    }

    public void AddCoin()
    {
        coinCount++;
        UpdateCoinUI();
    }

    void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = ":" + coinCount.ToString();
        }
    }
}
