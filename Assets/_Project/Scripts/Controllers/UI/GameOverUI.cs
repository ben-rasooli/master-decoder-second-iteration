using Doozy.Engine.UI;
using UnityEngine;
using Zenject;

namespace Project
{
    public class GameOverUI : MonoBehaviour
    {
        #region ----------------------------------------dependencies
        [Inject] MainController _mainController;
        [Inject(Id = "okButton")] UIButton _okButton;
        #endregion

        #region ----------------------------------------Unity Messages
        void Start()
        {
            _okButton.OnClick.OnTrigger.SetAction(_ =>
            {
                _mainController.Handle_GameOverPopupButtonClick();
            });
        }
        #endregion

        #region ----------------------------------------details
        #endregion
    }
}