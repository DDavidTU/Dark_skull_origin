using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

namespace sg
{
    public class scroll_select : MonoBehaviour
    {
        [SerializeField] GameObject currentselect;
        [SerializeField] GameObject previousselect;
        [SerializeField]  RectTransform currentselectTransform;
        [SerializeField] RectTransform contentpanel;
        [SerializeField] ScrollRect scrollRect;

        private void Update()
        {
            currentselect = EventSystem.current.currentSelectedGameObject;

            if (currentselect != null )
            {
                if( currentselect != previousselect )
                {
                    previousselect=currentselect;
                    currentselectTransform=currentselect.GetComponent<RectTransform>();
                    Snapto(currentselectTransform);
                }
            }
        }

        private void Snapto(RectTransform target)
        {
            Canvas.ForceUpdateCanvases();
            Vector2 newposition=
                (Vector2)scrollRect.transform.InverseTransformPoint(contentpanel.position)-(Vector2)scrollRect.transform.InverseTransformPoint(target.position);

            newposition.x = 0;

            contentpanel.anchoredPosition = newposition;
        }





    }
}
