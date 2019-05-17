using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

#pragma warning disable 0649, 0414
namespace Project
{
    public class PiecePickingSymbolUI : MonoBehaviour, IPointerEnterHandler
    {
        #region ----------------------------------------dependencies
        [Inject] PiecePickingUI _piecePickingUI;
        [SerializeField] PieceSymbol _symbol;
        #endregion

        #region ----------------------------------------Unity Messages
        public void OnPointerEnter(PointerEventData eventData)
        {
            _piecePickingUI.SetSymbole(_symbol);
        }
        #endregion
    }
}
