using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

#pragma warning disable 0649, 0414
namespace Project
{
    public class PiecePickingColorUI : MonoBehaviour, IPointerEnterHandler
    {
        #region ----------------------------------------dependencies
        [Inject] PiecePickingUI _piecePickingUI;
        [SerializeField] PieceColor _color;
        #endregion

        #region ----------------------------------------Unity Messages
        public void OnPointerEnter(PointerEventData eventData)
        {
            _piecePickingUI.SetColor(_color);
        }
        #endregion
    }
}
