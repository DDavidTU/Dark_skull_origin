using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace sg
{
    public class escapeTextControl : MonoBehaviour
    {
        [SerializeField] private CanvasGroup Backgrouns;
        [SerializeField] private CanvasGroup Text;
        [SerializeField] private float BackgroundTime;
        [SerializeField] private float TextTime;
      

        private void Awake()
        {
            StartCoroutine(FadeInBackGroung(Backgrouns, BackgroundTime, Text, TextTime));
        }
      
        private IEnumerator FadeInBackGroung(CanvasGroup Backgrouns, float eduration, CanvasGroup text,float texteduration)
        {
            if (eduration > 0)
            {
                Backgrouns.alpha = 0;
                float timer = 0;


                yield return null;

                while (timer < eduration)
                {
                    timer += Time.deltaTime;
                    Backgrouns.alpha = Mathf.Lerp(Backgrouns.alpha, 1, 0.05f * Time.deltaTime);
                    yield return null;
                }

                Backgrouns.alpha = 1;
             
            }
            yield return new WaitForSeconds(1);

            if (texteduration > 0)
            {
                text.alpha = 0;
                float timer = 0;


                yield return null;

                while (timer < texteduration)
                {
                    timer += Time.deltaTime;
                    text.alpha = Mathf.Lerp(text.alpha, 1, 0.5f * Time.deltaTime);
                    
                    yield return null;
                }

                text.alpha = 1;

            }
        }
       
    }
}
