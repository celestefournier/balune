using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Localization : MonoBehaviour
{
    [SerializeField] string ptBr;
    [SerializeField] string eng;

    void Start()
    {
        if (GetComponent<Text>())
        {
            ChangeText(PlayerPrefs.GetString("language", "eng"));
            ConfigManager.onChangeLanguage += ChangeText;
        }

        if (GetComponent<TextMeshProUGUI>())
        {
            ChangeTextMeshPro(PlayerPrefs.GetString("language", "eng"));
            ConfigManager.onChangeLanguage += ChangeTextMeshPro;
        }
    }

    void ChangeText(string language)
    {
        if (language == "pt-br") GetComponent<Text>().text = ptBr;
        if (language == "eng") GetComponent<Text>().text = eng;
    }

    void ChangeTextMeshPro(string language)
    {
        if (language == "pt-br") GetComponent<TextMeshProUGUI>().text = ptBr;
        if (language == "eng") GetComponent<TextMeshProUGUI>().text = eng;
    }
}
