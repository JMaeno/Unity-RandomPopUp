using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementParameters : MonoBehaviour
{
    //elementレベル
    public int elementLevel = 1;

    public bool isHitted = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public int GetElementLevel()
    {
        return elementLevel;
    }

    public void IncrementElementLevel()
    {
        elementLevel++;
    }

    public void hit()
    {
        isHitted = true;
    }

}
