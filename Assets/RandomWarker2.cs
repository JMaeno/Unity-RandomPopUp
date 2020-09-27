using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWarker2 : MonoBehaviour
{
    [SerializeField]
    public string target_tag;

    Rigidbody rb;

    private ParticleSystem particle;
//    private ParticleSystem.MainModule particleMain;
    private ParticleSystem.EmissionModule particleEmission;

    public float growSpeed = 1.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        particle = GetComponent<ParticleSystem>();
//        particleMain = particle.main;
        particleEmission = particle.emission;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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

/*
    void OnParticleCollision(GameObject obj)
    {
        if(obj.tag == gameObject.tag)
        {
            ElementFusion(obj);
        }
    }
*/
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
        bool isTargetDestroy;
        if (elementLevel != targetLevel)
        {
            isTargetDestroy = elementLevel > targetLevel;
            FusinoAndDestroy(target, isTargetDestroy);
            return;
        }
        isTargetDestroy = int.Parse(gameObject.name) < int.Parse(target.name);
        FusinoAndDestroy(target, isTargetDestroy);
        return;
    }

    private void FusinoAndDestroy(GameObject target, bool isTargetDestroy)
    {
        ElementParameters targetScript = target.GetComponent<ElementParameters>();
        ElementParameters selfScript = gameObject.GetComponent<ElementParameters>();
        int targetLevel = targetScript.GetElementLevel();
        int elementLevel = selfScript.GetElementLevel();
        if (isTargetDestroy)
        {
            Destroy(target);
            gameObject.GetComponent<ElementParameters>().IncrementElementLevel();
            particleEmission.rateOverTime = 2.0f * particleEmission.rateOverTime.constant;
            targetScript.hit();
            selfScript.hit();
            return;
        }
        Destroy(gameObject);
        targetScript.IncrementElementLevel();
        ParticleSystem targetParticle = target.GetComponent<ParticleSystem>();
        //ParticleSystem.MainModule targetParticleMain = targetParticle.main;
        ParticleSystem.EmissionModule targetParticleEmission = targetParticle.emission;
        targetParticleEmission.rateOverTime = 2.0f * targetParticleEmission.rateOverTime.constant;
        targetScript.hit();
        selfScript.hit();
        return;
    }

}
