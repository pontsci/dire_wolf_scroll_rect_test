using System.Collections.Generic;
using UnityEngine;

namespace Fleming.Assets.Scripts
{

    /// <summary>
    /// Spawns the items in at their initial positions.
    /// </summary>
    public class ContentSpawner : MonoBehaviour
    {
        [Header("Dimensions")]
        [Tooltip("The width of the items you have in the scroll view.")]
        public float ItemWidth;

        [Tooltip("The amount of space between cards.")]
        public float Spacing;

        public float Width
        {
            get;
            set;
        }

        [Tooltip("Should the cards be sorted automatically?")]
        public bool AutoSort;

        [Header("Prefabs")]
        //the list of prefabs we'll spawn
        [SerializeField] private List<GameObject> prefabs;

        /// <summary>
        /// Used for setting the initial positions of the cards.
        /// </summary>
        void Start()
        {
            //set our width
            Width = GetComponent<RectTransform>().rect.width;

            for (int i = 0; i < prefabs.Count; i++)
            {
                GameObject obj = Instantiate(prefabs[i], transform);

                //the RectTransform for positioning the item
                RectTransform rt = obj.GetComponent<RectTransform>();

                //set middle left anchoring
                rt.anchorMax = new Vector2(0, 0.5f);
                rt.anchorMin = new Vector2(0, 0.5f);

                //zero it out relative to parent
                rt.anchoredPosition = Vector2.zero;

                //pivot from left
                rt.pivot = new Vector2(0, 0.5f);

                //set calculated position based on content width and spacing
                rt.anchoredPosition = new Vector2(i * (ItemWidth+Spacing), 0);
            }
        }

        /// <summary>
        /// Sets the prefabs to the passed list of prefabs
        /// </summary>
        /// <param name="prefabs">a list of GameObjects you want to be the content</param>
        public void SetPrefabs(List<GameObject> prefabs)
        {
            this.prefabs = prefabs;
        }
    }
}
