using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWarker2 : MonoBehaviour
{
    //　歩くスピード
    [SerializeField]
    private float walkSpeed = 3.0f;

    Rigidbody rb;

    //GameObject[] elements;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb = GetComponent<Rigidbody>();
        //方向転換（Y軸ランダム回転）
        float angle = Random.Range(-45f, 45f);
        transform.Rotate(0, angle, 0);

        float x = Random.Range(-3f, 3f);
        float z = Random.Range(-3f, 3f);
        rb.AddForce(x, 0, z);
/*
        elements = GameObject.FindGameObjectsWithTag("Element");
        for(int i =0; i < elements.Length; i++)
        {
            elements[i].transform.position += transform.forward * walkSpeed * Time.deltaTime;
        }
*/

    }
}
