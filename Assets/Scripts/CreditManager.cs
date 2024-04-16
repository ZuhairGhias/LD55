using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CreditManager : MonoBehaviour
{

    public static bool CreditsFinished;

    public Canvas jakodi;
    public Canvas jadelynn;
    public Canvas jake;
    public Canvas kevin;
    public Canvas zuhair;
    public Canvas thanks;

    public Image black;
    public float fadeRate;
    public TMPro.TMP_Text text;


    private void Awake()
    {
        CreditsFinished = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RollCredits());
    }

    private IEnumerator RollCredits()
    {
        yield return ShowFadeInFadeOutHide(jakodi);
        yield return ShowFadeInFadeOutHide(jadelynn);
        yield return ShowFadeInFadeOutHide(jake);
        yield return ShowFadeInFadeOutHide(kevin);
        yield return ShowFadeInFadeOutHide(zuhair);

        thanks.gameObject.SetActive(true);
        yield return FadeToWhite();
        yield return new WaitForSeconds(3);
        text.gameObject.SetActive(true);

        while (!Input.GetButtonDown("Attack"))
        {
            yield return null;
        }

        CreditsFinished = true;
    }

    private IEnumerator FadeToBlack()
    {
        while (black.color.a < 1)
        {
            black.color = new Color(0,0,0, Mathf.Clamp01(black.color.a + (fadeRate * Time.deltaTime)));
            yield return null;
        }
    }

    private IEnumerator FadeToWhite()
    {
        while (black.color.a > 0)
        {
            black.color = new Color(0, 0, 0, Mathf.Clamp01(black.color.a - (fadeRate * Time.deltaTime)));
            yield return null;
        } 
    }

    private IEnumerator ShowFadeInFadeOutHide(Canvas canvas)
    {
        canvas.gameObject.SetActive(true);
        yield return FadeToWhite();
        yield return new WaitForSeconds(5);
        yield return FadeToBlack();
        canvas.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
    }
}
