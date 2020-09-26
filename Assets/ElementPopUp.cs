using System.Collections;
using UnityEngine;


public class ElementPopUp : MonoBehaviour
{
    public GameObject[] elements;
    private int number = 0;

    // Start is called before the first frame update
    void Start()
    {
        number = Random.Range(0, elements.Length);
        Instantiate(elements[number], transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
