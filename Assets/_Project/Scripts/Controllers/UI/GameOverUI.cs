using Doozy.Engine.UI;
using UnityEngine;
using Zenject;

namespace Project
{
    public class GameOverUI : MonoBehaviour
    {
        #region ----------------------------------------dependencies
        [Inject] SignalBus _signalBus;
        [Inject] MainController _mainController;
        [Inject(Id = "gameOverPopup")] UIPopup _popup;
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

        void OnEnable()
        {
            subscribeToSignals();
        }

        void OnDestroy()
        {
            unsubscribeFromSignals();
        }
        #endregion

        #region ----------------------------------------signals
        void subscribeToSignals()
        {
            _signalBus.Subscribe<GameIsOverSignal>(onGameIsOver);
        }

        void unsubscribeFromSignals()
        {
            _signalBus.TryUnsubscribe<GameIsOverSignal>(onGameIsOver);
        }

        void onGameIsOver(GameIsOverSignal arg)
        {
            _popup.Show();
        }
        #endregion

        #region ----------------------------------------details
        #endregion
    }
}