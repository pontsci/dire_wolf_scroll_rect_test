using System;
using System.Collections.Generic;
using Fleming.Assets.Scripts.Enums;
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

            //if AutoSort is true, we try to sort by card data
            if (AutoSort)
            {
                List<GameObject> sortedPrefabs = new List<GameObject>();
                List<GameObject> hearts = new List<GameObject>();
                List<GameObject> diamonds = new List<GameObject>();
                List<GameObject> clubs = new List<GameObject>();
                List<GameObject> spades = new List<GameObject>();

                //add each suit to relevant list
                foreach (GameObject prefab in prefabs)
                {
                    Card card = prefab.GetComponent<Card>();
                    if (card == null)
                    {
                        Debug.LogError("No card component found on prefab! Make sure each item in the prefab list has a Card component.");
                        return;
                    }

                    switch (card.cardData.Suit)
                    {
                        case Suits.Hearts:
                            hearts.Add(prefab);
                            break;
                        case Suits.Diamonds:
                            diamonds.Add(prefab);
                            break;
                        case Suits.Clubs:
                            clubs.Add(prefab);
                            break;
                        case Suits.Spades:
                            spades.Add(prefab);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                //sort each suited listed by card value
                hearts.Sort(SortByCardValue);
                diamonds.Sort(SortByCardValue);
                clubs.Sort(SortByCardValue);
                spades.Sort(SortByCardValue);

                //add them in the correct suit order according to the specified problem
                sortedPrefabs.AddRange(hearts);
                sortedPrefabs.AddRange(diamonds);
                sortedPrefabs.AddRange(clubs);
                sortedPrefabs.AddRange(spades);

                //set our prefabs to the newly sorted ones
                prefabs = sortedPrefabs;
            }
            
            //populate the screen with cards
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

        private int SortByCardValue(GameObject o1, GameObject o2)
        {
            Card card1 = o1.GetComponent<Card>();
            Card card2 = o2.GetComponent<Card>();

            //if the component doesn't exist, tell the dev
            if (card1 == null || card2 == null)
            {
                Debug.LogError("No Card Component Exists...");
                return 0;
            }

            //if the data isn't there, tell the dev
            if (card1.cardData == null)
            {
                Debug.LogError("No ScriptableObject Data exists for this card: " + card1 + "\nObject: " + o1);
                return 0;
            }

            //if the data isn't there, tell the dev
            if (card2.cardData == null)
            {
                Debug.LogError("No ScriptableObject Data exists for this card: " + card2 + "\nObject: " + o2);
                return 0;
            }

            //sort ascending
            return card1.cardData.cardNumberInt.CompareTo(card2.cardData.cardNumberInt);
        }
    }
}
