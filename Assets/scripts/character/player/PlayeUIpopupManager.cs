using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.UIElements;
namespace sg
{

    public class PlayeUIpopupManager : MonoBehaviour
    {
        [Header("You DIED pop up")]
        [SerializeField] GameObject youDiedPopupGameobject;
        [SerializeField] TextMeshProUGUI youDiedPopupbackGroundText;
        [SerializeField] TextMeshProUGUI youDiedPopUpText;
        [SerializeField] CanvasGroup youDiedPopUpcavasGroup;//±±¨îcanvas²H¤J²H¥X
        
        [Header("You interaction something pop up")]
        [SerializeField] public GameObject interactionInformation_gameObject;
        [SerializeField] public GameObject ItemInformation_gameObject;
        [SerializeField] public TextMeshProUGUI interactionInformation_text;
        [SerializeField] public TextMeshProUGUI ItemInformation_text;
        [SerializeField] public RawImage Itemicon;
        [Header("Load buttom")]
        
        [SerializeField] private UnityEngine.UI.Button LoadButton;
        [SerializeField] private UnityEngine.UI.Button ReturnButton;



        public void sendYouDiedPopUp()
        {
            youDiedPopupGameobject.SetActive(true);
            youDiedPopupbackGroundText.characterSpacing = 0;

            StartCoroutine(LoadSelect());
            StartCoroutine(strethpopuptextovertime(youDiedPopUpText, 20f, 10));
            StartCoroutine(FadeInpopuptextovertime(youDiedPopUpcavasGroup, 2));
            StartCoroutine(Fadeoutpopuptextovertime(youDiedPopUpcavasGroup, 2, 2));

        }

        private IEnumerator  LoadSelect()
        {
           
            yield return new WaitForSeconds(2);
            LoadButton.gameObject.SetActive(true);
            ReturnButton.gameObject.SetActive(true);
            LoadButton.Select();
           
            
        }
        private IEnumerator strethpopuptextovertime(TextMeshProUGUI text, float srethAmount, float eduration)
        {
            if (eduration > 0)
            {
                text.characterSpacing = 0;

                float timer = 0;


                yield return null;

                while (timer < eduration)
                {
                    timer += Time.deltaTime;
                    text.characterSpacing = Mathf.Lerp(text.characterSpacing, srethAmount, eduration * (Time.deltaTime / 20));
                    yield return null;
                }
            }
        }
        private IEnumerator FadeInpopuptextovertime(CanvasGroup canvas, float eduration)
        {
            if (eduration > 0)
            {
                canvas.alpha = 0;
                float timer = 0;


                yield return null;

                while (timer < eduration)
                {
                    timer += Time.deltaTime;
                    canvas.alpha = Mathf.Lerp(canvas.alpha, 1, eduration * Time.deltaTime);
                    yield return null;

                }

                canvas.alpha = 1;
                yield return null;

            }


        }
        private IEnumerator Fadeoutpopuptextovertime(CanvasGroup canvas, float eduration, float delay)
        {
            if (eduration > 0)
            {

                while (delay > 0)
                {
                    delay -= Time.deltaTime;
                    yield return null;
                }

                canvas.alpha = 1;
                float timer = 0;


                yield return null;

                while (timer < eduration)
                {
                    timer += Time.deltaTime;
                    canvas.alpha = Mathf.Lerp(canvas.alpha, 0, eduration * Time.deltaTime);
                    yield return null;
                }

                canvas.alpha = 0;
                yield return null;

            }
        }


        
    }
}
