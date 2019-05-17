using UnityEngine;
using Zenject;

namespace Project
{
    public class PlayController : MonoBehaviour
    {
        #region ----------------------------------------dependencies
        [Inject(Id = "guessingPanel")] RectTransform _guessingPanel;
        [Inject(Id = "playCanvas")] RectTransform _playCanvas;
        #endregion

        #region ----------------------------------------Unity Messages
        void Start()
        {
            _guessingPanel.SetParent(_playCanvas, false);
        }
        #endregion
    }
}
