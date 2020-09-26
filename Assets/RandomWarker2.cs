using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWarker2 : MonoBehaviour
{
    [SerializeField]
    public string target_tag;

    Rigidbody rb;

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
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == target_tag)
        {
            GameObject target = collision.gameObject;
            ElementFusion(target);
        }
    }

    private void ElementFusion(GameObject target)
    {
        ElementParameters targetScript = target.GetComponent<ElementParameters>();
        ElementParameters selfScript = gameObject.GetComponent<ElementParameters>();

        // 衝突判定が同時に実行されるため、片方のスクリプトが実行された時点で
        // 両方の衝突判定フラグをTrueにし、片方のフラグがTrueの場合は
        // 衝突判定実施後であるため処理をスキップする
        if (selfScript.isHitted || targetScript.isHitted)
        {
            selfScript.isHitted = false;
            targetScript.isHitted = false;
            return;
        }

        // エレメントレベルが大きいものを残す
        int targetLevel = targetScript.GetElementLevel();
        int elementLevel = selfScript.GetElementLevel();
        if (elementLevel > targetLevel)
        {
            Destroy(target);
            GetComponent<Renderer>().material.color = Color.blue;
            selfScript.IncrementElementLevel();
            transform.localScale *= 1.2f;
        }
        else if (elementLevel < targetLevel)
        {
            Destroy(gameObject);
            target.GetComponent<Renderer>().material.color = Color.blue;
            targetScript.IncrementElementLevel();
            target.transform.localScale *= 1.2f;
        }
        else if (targetLevel == elementLevel)
        {
            if (int.Parse(gameObject.name) > int.Parse(target.name))
            {
                Destroy(gameObject);
                target.GetComponent<Renderer>().material.color = Color.red;
                targetScript.IncrementElementLevel();
                target.transform.localScale *= 1.2f;
            }

            else
            {
                Destroy(target);
                GetComponent<Renderer>().material.color = Color.red;
                gameObject.GetComponent<ElementParameters>().IncrementElementLevel();
                transform.localScale *= 1.2f;
            }
        }
        targetScript.hit();
        selfScript.hit();

    }
}
