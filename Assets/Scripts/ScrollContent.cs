using System.Collections.Generic;
using UnityEngine;

namespace Fleming.Assets.Scripts
{
    public class ScrollContent : MonoBehaviour
    {

        //the runtime list of spawned objects
        public List<GameObject> LiveContent = new List<GameObject>();

        [Header("Dimensions")]
        [Tooltip("The width of the items you have in the scroll view.")]
        public float ItemWidth;
        [SerializeField]
        private float spacing;

        [Header("Prefabs")]
        //the list of prefabs we'll spawn
        [SerializeField] private List<GameObject> prefabs;

        

        void Start()
        {
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
                rt.anchoredPosition = new Vector2(i * (ItemWidth+spacing), 0);

                //grab the GameObject for the runtime list
                LiveContent.Add(obj);
            }

            //split the list in half and put half of that on the left side (negative x)
            //int splitAmount = LiveContent.Count / 2 / 2;
            //for (int i = LiveContent.Count - 1; i > LiveContent.Count - splitAmount - 1; i--)
            //{
            //    //save the transform
            //    GameObject objToMove = LiveContent[i];

            //    //get rid of it
            //    LiveContent.RemoveAt(i);

            //    //move it to the front
            //    LiveContent.Insert(0, objToMove);
            //}

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
