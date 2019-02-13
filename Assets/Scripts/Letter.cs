using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Letter : MonoBehaviour
{
    
    public char value { get; private set; }

    private void Start()
    {
        SetValue("-", 0);
        GetComponent<Text>().text = value.ToString();
    }

    public void SetValue(string word, int index)
    {
        value = word.ToUpper()[index];
    }

}
