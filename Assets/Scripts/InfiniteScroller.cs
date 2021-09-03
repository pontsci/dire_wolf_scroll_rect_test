using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Fleming.Assets.Scripts
{
    public class InfiniteScroller : MonoBehaviour, IScrollHandler ,IBeginDragHandler, IDragHandler
    {
        /// <summary>
        /// The script that handles the spawning of content.
        /// </summary>
        [SerializeField]
        private ContentSpawner _contentSpawner;

        /// <summary>
        /// How far an item can go before being repositioned to the left or right
        /// dependent on scroll direction.
        /// </summary>
        [SerializeField] private float boundsThreshold;

        /// <summary>
        /// Used for keeping track of the last place the user dragged. Mainly used for calculating rightDrag
        /// </summary>
        [SerializeField] private Vector2 lastDragPosition;

        /// <summary>
        /// Whether the user is dragging right or not
        /// </summary>
        [SerializeField] private bool rightDrag;

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
            rightDrag = eventData.position.x > lastDragPosition.x;

            lastDragPosition = eventData.position;
        }

        /// <summary>
        /// This function is called when the user is scrolling with the mouse wheel.
        /// </summary>
        /// <param name="eventData">The data on the scroll</param>
        public void OnScroll(PointerEventData eventData)
        {
            //if the user is scrolling down, then we are dragging to the right
            rightDrag = eventData.scrollDelta.y < 0;
        }

        /// <summary>
        /// Called when OnValueChanged is invoked on the ScrollRect component. We base the
        /// scrollAmount off of the localPosition change of the contentHolder
        /// </summary>
        /// <param name="position"></param>
        public void UpdateScrollAmount(Vector2 position)
        {
            //try to reposition cards

            //because the items are listed in order in the hierarchy
            //we can use it as a list to keep track of the order of items,
            //in this way we know what item is the "front" and the "end".
            //we get the childCount here so that we know our upper bounds of the list.
            int scrollContentChildCount = _contentSpawner.transform.childCount;

            //we need to get the index of the child we want to check the bounds for.
            //this item would be at the "front" of the side the items are moving towards.
            //if rightDrag is true, items are moving right, so we want to check the right most "front" item against the bounds
            //otherwise we want to check the left most "front" item against the bounds, since items would be moving left.
            int frontItemIndex = rightDrag ? scrollContentChildCount - 1 : 0;
            Transform frontItem = _contentSpawner.transform.GetChild(frontItemIndex);

            //if the "front" item isn't out of bounds, then we're good, we can break out
            if (OutsideBounds(frontItem) == false)
            {
                return;
            }

            //if it is out of bounds, we need to reposition it to the "end".
            //we need to reposition it relative to the item at the "end".
            //the "end" item depends on the direction we are scrolling.
            //if we're dragging right, the "end" item is on the left, so grab index 0
            //otherwise grab the last index
            int endItemIndex = rightDrag ? 0 : scrollContentChildCount - 1;
            Transform endItem = _contentSpawner.transform.GetChild(endItemIndex);

            //we need to create the new position this item is going to exist at.
            //we'll start with the "end" item position
            Vector2 newPositionForItem = endItem.position;

            //if we're dragging right, we need to position left of the "end" item on the left
            if (rightDrag)
            {
                newPositionForItem.x = endItem.position.x - _contentSpawner.ItemWidth * 2 - _contentSpawner.Spacing * 2;
            }
            else
            //if we're dragging left, we need to position right of the "end" item on the right
            {
                newPositionForItem.x = endItem.position.x + _contentSpawner.ItemWidth * 2 + _contentSpawner.Spacing * 2;
            }

            //set the "front" item to its new position at the "end"
            frontItem.position = newPositionForItem;

            //reposition the "front" item in the hierarchy to be a sibling of the "end" item.
            //in this way, we keep the original ordering of the list.
            frontItem.SetSiblingIndex(endItemIndex);
        }

        /// <summary>
        /// Returns true if item is outside the allowed bounds
        /// </summary>
        /// <param name="t">The transform you want to check</param>
        /// <returns>true if outside, false if within</returns>
        private bool OutsideBounds(Transform t)
        {
            //the positive threshold is our current position + half the width + the threshold
            float positiveThreshold = transform.position.x + _contentSpawner.Width * 0.5f + boundsThreshold;

            //the negative threshold is our current position - half the width - the threshold
            float negativeThreshold = transform.position.x - _contentSpawner.Width * 0.5f - boundsThreshold;

            //if cards are moving right, we need to check the positive threshold
            if (rightDrag)
            {
                return t.position.x - _contentSpawner.ItemWidth * 0.5f > positiveThreshold;
            }
            else //otherwise we need to check the negative threshold
            {
                return t.position.x + _contentSpawner.ItemWidth * 0.5f < negativeThreshold;
            }
        }
    }
}
