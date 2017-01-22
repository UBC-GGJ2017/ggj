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

    void Update()
    {
        if (animating)
        {
            float new_value = Mathf.Lerp(start_val, end_val, Time.deltaTime * speed);
            EffectMaterial.SetFloat("_Magnitude", new_value);
            Debug.Log("Fading out with " + new_value + ", rv: " + (new_value >= 1f));
            if (new_value == end_val)
            {
                animating = false;
            }
        }
    }

    public void SetValue(float val)
    {
        animating = false;
        EffectMaterial.SetFloat("_Magnitude", val); 
    }

    public void FadeOut()
    {
        startTime = Time.time;
        animating = true;
        start_val = 0f;
        end_val = 0.2f;
    }

    public void FadeIn()
    {
        startTime = Time.time;
        animating = true;
        start_val = 0.2f;
        end_val = 0f;
    }
}
