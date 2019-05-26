using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Zenject;

#pragma warning disable 0649, 0414
namespace Project
{
    public class GuessingPanelPieceUI : MonoBehaviour, IPointerDownHandler
    {
        #region ----------------------------------------dependencies
        [Inject] PiecePickingUI _piecePickingUI;
        #endregion

        #region ----------------------------------------API
        public Piece Piece { get; private set; }
        #endregion

        #region ----------------------------------------Unity Messages
        public void OnPointerDown(PointerEventData eventData)
        {
            _piecePickingUI.Show(setPiece);
        }

        void Awake()
        {
            _symbolText = GetComponentInChildren<TextMeshProUGUI>();
            _colorImage = GetComponent<Image>();
        }

        void Start()
        {
            Piece = new Piece();
            updateUI();

            setPiece = piece =>
            {
                Piece = piece;
                updateUI();
            };
        }

        void Update()
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonUp(0))
                _piecePickingUI.Hide();
#else
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                    _piecePickingUI.Hide();
            }
#endif
        }
        #endregion

        #region ----------------------------------------details
        TextMeshProUGUI _symbolText;
        Image _colorImage;
        Action<Piece> setPiece;

        void updateUI()
        {
            _symbolText.text = ((int)Piece.Symbol + 1).ToString();
            if (Piece.Color == PieceColor.White)
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
    }
}
