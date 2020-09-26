using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementGenerator : MonoBehaviour
{
    //エレメントプレハブ
    public GameObject elementPrefab;
    //時間間隔の最小値＆最大値
    private float minInterval=1;
    private float maxInterval=2;
    //時間間隔
    private float interval;
    //経過時間
    private float time = 0f;

    //X座標の最小値
    public float xMinPosition = -10f;
    //X座標の最大値
    public float xMaxPosition = 10f;
    //Y座標の最小値
    public float yMinPosition = 0f;
    //Y座標の最大値
    public float yMaxPosition = 10f;
    //Z座標の最小値
    public float zMinPosition = 10f;
    //Z座標の最大値
    public float zMaxPosition = 20f;

    //element番号
    private int i=0;

    // Start is called before the first frame update
    void Start()
    {
        interval = GetRandomTime();    
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > interval)
        {
            GameObject element = Instantiate(elementPrefab);
//            element.name = "element" + i;
//            element.tag = "Element";
//            i++;
            elementPrefab.transform.position = GetRandomPosition();
            time = 0f;

            interval = GetRandomTime();
        }
    }

    private float GetRandomTime()
    {
        return Random.Range(minInterval, maxInterval);
    }

    private Vector3 GetRandomPosition()
    {
        //それぞれの座標をランダムに生成する
        float x = Random.Range(xMinPosition, xMaxPosition);
        float y = Random.Range(yMinPosition, yMaxPosition);
        float z = Random.Range(zMinPosition, zMaxPosition);

        //Vector3型のPositionを返す
        return new Vector3(x, y, z);
    }
}
