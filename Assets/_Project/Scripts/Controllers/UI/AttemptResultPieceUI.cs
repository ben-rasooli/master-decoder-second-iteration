using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    public class AttemptResultPieceUI : MonoBehaviour
    {
        #region ----------------------------------------API
        public void Init(Piece piece)
        {
            _symbolText.text = ((int)piece.Symbol + 1).ToString();
            if (piece.Color == PieceColor.White)
            {
                _colorImage.color = Color.white;
                _symbolText.color = Color.black;
            }
            else
            {
                _colorImage.color = Color.black;
                _symbolText.color = Color.white;
            }
        }
        #endregion

        #region ----------------------------------------Unity Messages
        void Awake()
        {
            _symbolText = GetComponentInChildren<TextMeshProUGUI>();
            _colorImage = GetComponent<Image>();
        }
        #endregion

        #region ----------------------------------------details
        TextMeshProUGUI _symbolText;
        Image _colorImage;
        #endregion
    }
}
