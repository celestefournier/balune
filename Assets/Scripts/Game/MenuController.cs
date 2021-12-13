using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject menuScreen;
    [SerializeField] GameObject modeScreen;
    [SerializeField] FadeManager fadeManager;
    [SerializeField] Selector languageOption;
    [SerializeField] Selector soundOption;

    void Start()
    {
        soundOption.SetIndex(PlayerPrefs.GetInt("sound", 1));
        languageOption.SetIndex(PlayerPrefs.GetString("language", "eng") == "eng" ? 0 : 1);
    }

    public void StartGame(string mode)
    {
        float fadeInDuration = 0.3f;
        GameController.gameMode = mode;
        fadeManager.FadeIn(() => SceneManager.LoadScene("Game"), fadeInDuration);
    }

    public void ToggleSound()
    {
        int soundActive = 1 - PlayerPrefs.GetInt("sound", 1);
        PlayerPrefs.SetInt("sound", soundActive);
    }
}
