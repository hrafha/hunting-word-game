using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Level
{
    [RequireComponent(typeof(Text))]
    public class Letter : MonoBehaviour
    {

        public char value { get; private set; }

        public void SetValue(string word, int index)
        {
            value = word.ToUpper()[index];
            GetComponent<Text>().text = "" + value;
        }

        public void SetRandomValue()
        {
            value = (char)('A' + Random.Range(0, 26));
            GetComponent<Text>().text = "" + value;
        }

    }
}