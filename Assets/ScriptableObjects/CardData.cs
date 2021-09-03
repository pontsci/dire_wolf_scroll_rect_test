using System;
using Fleming.Assets.Scripts.Enums;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

namespace Fleming.Assets.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CardData", menuName = "Cards/CardData")]
    public class CardData : ScriptableObject
    {
        [Tooltip("The Suit of the card.")]
        public Suits Suit;

        [Tooltip("The card's number.")] 
        public CardNumbers CardNumber;

        [Tooltip("NOTE: Use the enum above! 2-10, 11=Jack, 12=Queen, 13=King, 14=Ace"), Range(2,14), ReadOnly]
        public int cardNumberInt = 2;


        public void OnCardNumberUpdated()
        {
            switch (CardNumber)
            {
                case CardNumbers.Two:
                    cardNumberInt = 2;
                    break;
                case CardNumbers.Three:
                    cardNumberInt = 3;
                    break;
                case CardNumbers.Four:
                    cardNumberInt = 4;
                    break;
                case CardNumbers.Five:
                    cardNumberInt = 5;
                    break;
                case CardNumbers.Six:
                    cardNumberInt = 6;
                    break;
                case CardNumbers.Seven:
                    cardNumberInt = 7;
                    break;
                case CardNumbers.Eight:
                    cardNumberInt = 8;
                    break;
                case CardNumbers.Nine:
                    cardNumberInt = 9;
                    break;
                case CardNumbers.Ten:
                    cardNumberInt = 10;
                    break;
                case CardNumbers.Jack:
                    cardNumberInt = 11;
                    break;
                case CardNumbers.Queen:
                    cardNumberInt = 12;
                    break;
                case CardNumbers.King:
                    cardNumberInt = 13;
                    break;
                case CardNumbers.Ace:
                    cardNumberInt = 14;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum CardNumbers
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }
}
