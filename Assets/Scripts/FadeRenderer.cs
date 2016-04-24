using UnityEngine;
using System.Collections;
public delegate void FadeRendererEvent(FadeRenderer aRenderer);
public class FadeRenderer : MonoBehaviour
{
    #region Fields

    public float fadeTime = 2;
    public float waitBeforeStarting = 0;
    public Color startColor = Color.black;
    
    public Color finalColor = Color.white;
    private Color curentColor;
    public bool mustDestroy = true;
    #endregion Fields
    public event FadeRendererEvent onFadeComplete;
    #region Methods

    IEnumerator Start()
    {
        curentColor = startColor;
        GetComponent<Renderer>().material.SetColor("_Color", curentColor);
        yield return new WaitForSeconds(waitBeforeStarting);

        float currentRealTime = Time.realtimeSinceStartup;
        float realDeltaTime = 0;
        float t = 0;
        float speed = 1.0f / fadeTime;
        while (t < 1)
        {
            curentColor = Color.Lerp(startColor, finalColor, t);
            GetComponent<Renderer>().material.SetColor("_Color", curentColor);
            t += realDeltaTime * speed;
            yield return null;
            realDeltaTime = Time.realtimeSinceStartup - currentRealTime;
            currentRealTime = Time.realtimeSinceStartup;
            
        }

        GetComponent<Renderer>().material.SetColor("_Color", finalColor);

        if (onFadeComplete != null)
        {
            onFadeComplete(this);
        }
        if (mustDestroy)
        {
            Destroy(gameObject);
        }

    }

  
    #endregion Methods
}