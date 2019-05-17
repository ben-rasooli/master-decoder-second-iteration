using System;
using UnityEngine;
using Zenject;

#pragma warning disable 0649, 0414
namespace Project
{
    public class PiecePickingUI : MonoBehaviour
    {
        #region ----------------------------------------dependencies
        //[Inject] RectTransform _attemptResultUIPrefab;
        #endregion

        #region ----------------------------------------API
        public void Show(Action<Piece> onPiecePicked)
        {
            _onPiecePicked = onPiecePicked;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetSymbole(PieceSymbol symbol)
        {
            _piece.Symbol = symbol;
            _onPiecePicked?.Invoke(_piece);
        }

        public void SetColor(PieceColor color)
        {
            _piece.Color = color;
            _onPiecePicked?.Invoke(_piece);
        }
        #endregion

        #region ----------------------------------------Unity Messages
        void Start()
        {
            _transform = GetComponent<RectTransform>();
            _piece = new Piece();
            Hide();
        }
        #endregion

        #region ----------------------------------------details
        RectTransform _transform;
        Piece _piece;
        Action<Piece> _onPiecePicked;
        #endregion
    }
}
