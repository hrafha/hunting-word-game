using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Letter : MonoBehaviour
{

    public char value { get; private set; }

    public void SetValue(string word, int index)
    {
        value = word.ToUpper()[index];
        GetComponent<Text>().text = "" + value;
    }

}
