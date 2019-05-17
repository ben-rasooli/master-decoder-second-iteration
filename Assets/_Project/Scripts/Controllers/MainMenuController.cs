using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Project
{
    public class MainMenuController : MonoBehaviour
    {
        #region ----------------------------------------dependencies
        [Inject] List<LevelData> _levelDatas;
        [Inject] List<LevelInfoUI> _levelInfoUIs;
        #endregion

        #region ----------------------------------------Unity Messages
        void Start()
        {
            for (int i = 0; i < _levelInfoUIs.Count; i++)
                _levelInfoUIs[i].Init(_levelDatas[i]);
            #endregion

            #region ----------------------------------------details
            #endregion
        }
    }
}