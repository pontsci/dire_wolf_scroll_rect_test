using UnityEditor;

namespace Fleming.Assets.ScriptableObjects.Editor
{
    [CustomEditor(typeof(CardData))]
    [CanEditMultipleObjects]
    public class CardDataEditor : UnityEditor.Editor
    {
        SerializedProperty suit;
        SerializedProperty cardNumber;
        SerializedProperty cardNumberInt;


        void OnEnable()
        {
            suit = serializedObject.FindProperty("Suit");
            cardNumber = serializedObject.FindProperty("CardNumber");
            cardNumberInt = serializedObject.FindProperty("cardNumberInt");
        }

        public override void OnInspectorGUI()
        {
            CardData cardData = (CardData)target;

            serializedObject.Update();
            EditorGUILayout.PropertyField(suit);
            EditorGUILayout.PropertyField(cardNumber);

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(cardNumberInt);
            EditorGUI.EndDisabledGroup();

            if (serializedObject.ApplyModifiedProperties())
            {
                cardData.OnCardNumberUpdated();
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
