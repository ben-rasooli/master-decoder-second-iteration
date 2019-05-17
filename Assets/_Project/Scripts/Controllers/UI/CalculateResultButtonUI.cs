using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

#pragma warning disable 0649, 0414
namespace Project
{
    public class CalculateResultButtonUI : MonoBehaviour
    {
        #region ----------------------------------------dependencies
        [Inject] ResultsController _resultsController;
        #endregion

        #region ----------------------------------------Unity Messages
        void Start()
        {
            _button = GetComponent<Button>();

            _onCalculateResultCompleted = () => { _button.interactable = true; };

            _button.onClick.AddListener(() =>
            {
                _button.interactable = false;
                _resultsController.Calculate(_onCalculateResultCompleted);
            });
        }
        #endregion

        #region ----------------------------------------details
        Button _button;
        Action _onCalculateResultCompleted;
        #endregion
    }
}
