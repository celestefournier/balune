using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject menuScreen;
    [SerializeField] GameObject modeScreen;
    [SerializeField] FadeManager fadeManager;

    public void StartGame(string mode)
    {
        ModeManager.mode = mode;
        fadeManager.FadeIn(() => SceneManager.LoadScene("Game"));
    }

    public void OpenMode()
    {
        menuScreen.SetActive(false);
        modeScreen.SetActive(true);
    }
}
