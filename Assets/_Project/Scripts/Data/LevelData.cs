using UnityEngine;

namespace Project
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData", order = 1)]
    public class LevelData : ScriptableObject
    {
        public int Index;
        public string Name;
        public string Description;
        public Sprite Sprite;
        public GameObject AttemptResultPrefab;
        public GameObject GuessingPanelPrefab;
    }
}