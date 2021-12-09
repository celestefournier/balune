using System;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    public static event Action<string> onChangeLanguage;

    public static void SetLanguage(string language)
    {
        PlayerPrefs.SetString("language", language);
        onChangeLanguage?.Invoke(language);
    }
}
