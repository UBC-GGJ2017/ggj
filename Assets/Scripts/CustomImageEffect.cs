using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CustomImageEffect : MonoBehaviour
{
    public Material EffectMaterial;

    public float speed = 0.1f;

    private float startTime;
    private float start_val;
    private float end_val;
    private bool animating;

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (EffectMaterial != null)
            Graphics.Blit(src, dst, EffectMaterial);
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOut2());

    }

    public void FadeIn()
    {
        StartCoroutine(FadeIn2());
    }

    public float transitionDuration = 0.3f;
    // assume magnitude is at 0
    IEnumerator FadeOut2()
    {
        float t = 0.0f;
        while (t < transitionDuration)
        {
            t+= Time.deltaTime * (Time.timeScale / transitionDuration);
            EffectMaterial.SetFloat("_Magnitude", Mathf.Lerp(0f, 0.3f, t));
            yield return 0;
        }
    }

    // assume magnitude is at 1
    IEnumerator FadeIn2()
    {
        float t = 0.0f;
        while (t < transitionDuration)
        {
            t += Time.deltaTime * (Time.timeScale / transitionDuration);
            EffectMaterial.SetFloat("_Magnitude", Mathf.Lerp(0.3f, 0f, t));
            yield return 0;
        }
    }
}
