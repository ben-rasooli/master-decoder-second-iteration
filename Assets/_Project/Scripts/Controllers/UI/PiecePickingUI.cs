using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

#pragma warning disable 0649, 0414, IDE0051
namespace Project
{
    public class PiecePickingUI : MonoBehaviour
    {
        #region ----------------------------------------dependencies
        [Inject] AttemptResultUI _attemptResultUI;
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
            _piece = new Piece();
            _piecePickingSymbolUIs = new List<PiecePickingSymbolUI>(GetComponentsInChildren<PiecePickingSymbolUI>());
            _piecePickingColorUIs = new List<PiecePickingColorUI>(GetComponentsInChildren<PiecePickingColorUI>());
            setupSymbolAndColorUIs();
            Hide();
        }
        #endregion

        #region ----------------------------------------details
        Piece _piece;
        List<PiecePickingSymbolUI> _piecePickingSymbolUIs;
        List<PiecePickingColorUI> _piecePickingColorUIs;
        Action<Piece> _onPiecePicked;

        void setupSymbolAndColorUIs()
        {
            _piecePickingSymbolUIs.ForEach(UI => UI.gameObject.SetActive(false));
            _piecePickingColorUIs.ForEach(UI => UI.gameObject.SetActive(false));

            for (int i = 0; i < _attemptResultUI.SymbolCount; i++)
                _piecePickingSymbolUIs[i].gameObject.SetActive(true);

            if (_attemptResultUI.ColorCount == 1)
                return;

            for (int i = 0; i < _attemptResultUI.ColorCount; i++)
                _piecePickingColorUIs[i].gameObject.SetActive(true);
        }
        #endregion
    }
}
