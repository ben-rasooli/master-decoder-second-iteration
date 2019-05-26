using UnityEngine;
using Zenject;

#pragma warning disable 0649, 0414
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
