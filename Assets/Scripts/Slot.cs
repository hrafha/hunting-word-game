using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Slot : MonoBehaviour
{

    public Letter letter;

    private void Start()
    {
        //Instantiate(letter, transform.position, Quaternion.identity, transform);
    }

}
