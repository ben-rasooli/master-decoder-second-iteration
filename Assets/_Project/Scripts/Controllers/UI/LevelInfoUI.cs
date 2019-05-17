using UnityEngine;
using UnityEngine.UI;
using Doozy.Engine.UI;
using TMPro;
using Zenject;

namespace Project
{
    public class LevelInfoUI : MonoBehaviour
    {
        #region ----------------------------------------dependencies
        [Inject(Id = "backgroundImage")] Image _backgroundImage;
        [Inject(Id = "titleText")] TextMeshProUGUI _titleText;
        [Inject(Id = "descriptionText")] TextMeshProUGUI _descriptionText;
        [Inject(Id = "playButton")] UIButton _playButton;
        [Inject] MainController _mainController;
        #endregion

        #region ----------------------------------------API
        public void Init(LevelData levelData)
        {
            _backgroundImage.sprite = levelData.Sprite;
            _titleText.SetText(levelData.Name);
            _descriptionText.SetText(levelData.Description);
            _playButton.OnClick.OnTrigger.SetAction(_ =>
            {
                _mainController.LevelData = levelData;
                _mainController.HandleMainMenu_LevelButtonClick();
            });
        }
        #endregion
    }
}