using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroy : MonoBehaviour
{
    private ParticleSystem par = null;
    private WaitForSeconds wait = new WaitForSeconds(0.5f);

    private void Awake()
    {
        par = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        StartCoroutine(DestroyEffect());
    }

    IEnumerator DestroyEffect()
    {
        yield return wait;
        Pool.Destory(this.gameObject);
    }
}
