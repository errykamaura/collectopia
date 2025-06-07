using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Nama scene bisa diset langsung atau dari Inspector
    public string nextLevelName = "level2";
    public string mainMenuName = "MainMenu";

    // Dipanggil oleh tombol Continue
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }

    // Dipanggil oleh tombol Quit
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuName);
    }

    // Opsional: keluar aplikasi kalau di-build sebagai game standalone
    public void QuitGame()
    {
        Application.Quit();
    }
}
