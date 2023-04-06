using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Dissolution : MonoBehaviour
{

    [Range(0.0f, 10f)] public float effectDuration;

    private float startTime;
    private Renderer _renderer;
    private MaterialPropertyBlock mpb;

    private float _dissolveAmount;
    private bool effectOn = false;

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        Debug.Log(collision.gameObject.layer);
        if (collision.gameObject.layer == 6)
        {
            DissolutionFunc();
        }
    }

    public void DissolutionFunc()
    {
        _renderer = GetComponent<Renderer>();
        mpb = new MaterialPropertyBlock();
        StartCoroutine(Dissolvement());
    }

    IEnumerator Dissolvement()
    {
        startTime = Time.time;
        effectOn = true;
        Debug.Log("Start coroutine ");
        while (effectOn)
        {
            _dissolveAmount = (Time.time - startTime) / effectDuration;
            //change the Material properties
            _renderer.GetPropertyBlock(mpb);
            mpb.SetFloat("_DissolveAmount", _dissolveAmount);
            _renderer.SetPropertyBlock(mpb);

            //need to stop effect ?
            if ((Time.time - startTime) > effectDuration)
            {
                effectOn = false;
            }

            yield return new WaitForSeconds(0.01f);
        }
        Debug.Log("End coroutine ");
    }
}
