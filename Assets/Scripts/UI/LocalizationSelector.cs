using UnityEngine;

public class LocalizationSelector : MonoBehaviour
{
    [SerializeField] string[] optionsPtBr;
    [SerializeField] string[] optionsEng;

    Selector selector;

    void Start()
    {
        selector = GetComponent<Selector>();

        ConfigManager.onChangeLanguage += ChangeText;
        ChangeText(PlayerPrefs.GetString("language", "eng"));
    }

    void ChangeText(string language)
    {
        if (language == "pt-br") selector.SetOptions(optionsPtBr);
        if (language == "eng") selector.SetOptions(optionsEng);
    }
}
