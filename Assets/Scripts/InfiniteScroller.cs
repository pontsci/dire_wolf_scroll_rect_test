using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Fleming.Assets.Scripts
{
    public class InfiniteScroller : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        /// <summary>
        /// The script the handles our scroll content
        /// </summary>
        [SerializeField]
        private ScrollContent scrollContent;

        private List<GameObject> liveContent;

        /// <summary>
        /// The parent GameObject of all the content
        /// </summary>
        [SerializeField] private GameObject contentHolder;

        /// <summary>
        /// How far has the user scrolled? Use this to know where to place our next card.
        /// </summary>
        [SerializeField] private float scrollAmount;

        /// <summary>
        /// How far an item can go before being repositioned to the left or right
        /// dependent on scroll direction.
        /// </summary>
        [SerializeField] private float boundsThreshold;

        /// <summary>
        /// Used for keeping track of the last place the user dragged. Mainly used for calculating positiveDrag
        /// </summary>
        [SerializeField] private Vector2 lastDragPosition;

        [SerializeField] private bool positiveDrag;

        void Start()
        {
            liveContent = scrollContent.LiveContent;
        }


        /// <summary>
        /// This function is called when the user starts the drag.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnBeginDrag(PointerEventData eventData)
        {
            lastDragPosition = eventData.position;
        }

        /// <summary>
        /// This function is called when the user is dragging.
        /// </summary>
        /// <param name="eventData">The data on the drag.</param>
        public void OnDrag(PointerEventData eventData)
        {
            positiveDrag = eventData.position.x > lastDragPosition.x;

            lastDragPosition = eventData.position;
        }

        /// <summary>
        /// Called when OnValueChanged is invoked on the ScrollRect component. We base the
        /// scrollAmount off of the localPosition change of the contentHolder
        /// </summary>
        /// <param name="position"></param>
        public void UpdateScrollAmount(Vector2 position)
        {
            //update the scrollAmount
            scrollAmount = contentHolder.transform.localPosition.x;


            //try to reposition cards

            //if cards are moving right, we need to add cards to the left
            if (positiveDrag)
            {
                //grab the left and right items
                GameObject leftMostItem = liveContent[0];
                GameObject rightMostItem = liveContent[liveContent.Count];

                //grab the RectTransforms for positioning




            }
            //if cards are moving left, we need to ad cards to the right
            else
            {

            }
        }
    }
}
