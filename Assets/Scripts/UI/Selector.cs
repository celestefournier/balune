using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    [SerializeField] Text textValue;
    [SerializeField] string[] options;
    [SerializeField] UnityEvent<string> onChange;

    int index = 0;

    void Start()
    {
        textValue.text = options[index];
    }

    public void Prev()
    {
        index = index - 1 < 0 ? options.Length - 1 : index - 1;
        textValue.text = options[index];
        onChange.Invoke(options[index]);
    }

    public void Next()
    {
        index = index + 1 < options.Length ? index + 1 : 0;
        textValue.text = options[index];
        onChange.Invoke(options[index]);
    }
}
