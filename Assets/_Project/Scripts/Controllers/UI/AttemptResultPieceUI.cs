using TMPro;
using UnityEngine;

namespace Project
{
    public class AttemptResultPieceUI : MonoBehaviour
    {
        #region ----------------------------------------API
        public void Init(Piece piece)
        {
            _symbolText.text = ((int)piece.Symbol + 1).ToString();
        }
        #endregion

        #region ----------------------------------------Unity Messages
        void Awake()
        {
            _symbolText = GetComponentInChildren<TextMeshProUGUI>();
        }
        #endregion

        #region ----------------------------------------details
        TextMeshProUGUI _symbolText;
        #endregion
    }
}
