using UnityEngine;
using DG.Tweening;
using Zenject;
using System;

#pragma warning disable 0649, 0414
namespace Project
{
    public class AttemptResultsUI : MonoBehaviour
    {
        #region ----------------------------------------dependencies
        [Inject(Id = "attemptResultUI")] RectTransform _attemptResultUI;
        [Inject(Id = "guessingPanel")] RectTransform _guessingPanel;
        #endregion

        #region ----------------------------------------API
        public void AddAttemptResult(AttemptResult result, Action onCompleted)
        {
            expandHieght();
            createAndAddNewAttemptResultUI(result);
            shrinkHieght(onCompleted);
        }
        #endregion

        #region ----------------------------------------Unity Messages
        void Start()
        {
            _transform = GetComponent<RectTransform>();
            Canvas.ForceUpdateCanvases();// this needed to make sure UI is updated before accessing it values
            _attemptResultHeight = _attemptResultUI.GetComponent<RectTransform>().rect.height;
            _guessingPanelHeight = _guessingPanel.GetComponent<RectTransform>().rect.height;
            _parrentHeight = _transform.parent.GetComponent<RectTransform>().rect.height;
            _normalHeight = _parrentHeight - _guessingPanelHeight;
            _expandedHeight = _normalHeight + _attemptResultHeight;
            _transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _normalHeight);
        }
        #endregion

        #region ----------------------------------------details
        RectTransform _transform;
        float _attemptResultHeight;
        float _guessingPanelHeight;
        float _parrentHeight;
        float _normalHeight;
        float _expandedHeight;

        void expandHieght()
        {
            _transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _expandedHeight);
        }

        void createAndAddNewAttemptResultUI(AttemptResult result)
        {
            var attemptResultUI = Instantiate(_attemptResultUI, _transform);
            attemptResultUI.GetComponent<AttemptResultUI>().Init(result);
        }

        [SerializeField] float _addingNewAttemptResultSpeed;
        void shrinkHieght(Action onCompleted)
        {
            DOTween.Kill(this, true);
            DOTween.To(
                () => _transform.sizeDelta.y,
                height => _transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height),
                _normalHeight,
                _addingNewAttemptResultSpeed)
                   .OnComplete(() => onCompleted.Invoke());
        }
        #endregion
    }
}
