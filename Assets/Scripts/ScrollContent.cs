using System.Collections.Generic;
using UnityEngine;

namespace Fleming.Assets.Scripts
{
    public class ScrollContent : MonoBehaviour
    {
        //the list of prefabs we'll spawn
        [SerializeField] private List<GameObject> _prefabs;

        //the runtime list of spawned objects
        private List<RectTransform> _liveContent;


        void Start()
        {
            //spawn in the prefabs
            foreach (GameObject prefab in _prefabs)
            {
                //spawn in the prefab with this as the parent
                Instantiate(prefab, transform);
            }
        }

        /// <summary>
        /// Sets the prefabs to the passed list of prefabs
        /// </summary>
        /// <param name="prefabs">a list of GameObjects you want to be the content</param>
        public void SetPrefabs(List<GameObject> prefabs)
        {
            _prefabs = prefabs;
        }
    }
}
