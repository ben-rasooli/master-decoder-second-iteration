using UnityEngine;
using DG.Tweening;
using Zenject;
using System;
using UnityEngine.UI;

#pragma warning disable 0649, 0414
namespace Project
{
    public class AttemptResultsUI : MonoBehaviour
    {
        #region ----------------------------------------dependencies
        [Inject(Id = "attemptResultUI")] RectTransform _attemptResultUI;
        [Inject(Id = "guessingPanel")] RectTransform _guessingPanel;
        [Inject] ScrollRect _scrollRect;
        #endregion

        #region ----------------------------------------API
        public void AddAttemptResult(AttemptResult result, Action onCompleted)
        {
            expandScrollHieght();
            createAndAddNewAttemptResultUI(result);
            shrinkScrollHieght(onCompleted);
        }
        #endregion

        #region ----------------------------------------Unity Messages
        void Start()
        {
            _transform = GetComponent<RectTransform>();
            _scrollRectTransform = _scrollRect.GetComponent<RectTransform>();
            Canvas.ForceUpdateCanvases();// this needed to make sure UI is updated before accessing it values
            _attemptResultHeight = _attemptResultUI.GetComponent<RectTransform>().rect.height;
            _guessingPanelHeight = _guessingPanel.GetComponent<RectTransform>().rect.height;
            _scrollParrentHeight = _scrollRectTransform.parent.GetComponent<RectTransform>().rect.height;
            _scrollNormalHeight = _scrollParrentHeight - _guessingPanelHeight;
            _scrollExpandedHeight = _scrollNormalHeight + _attemptResultHeight;
            _scrollRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _scrollNormalHeight);
        }
        #endregion

        #region ----------------------------------------details
        RectTransform _transform;
        RectTransform _scrollRectTransform;
        float _attemptResultHeight;
        float _guessingPanelHeight;
        float _scrollParrentHeight;
        float _scrollNormalHeight;
        float _scrollExpandedHeight;

        void expandScrollHieght()
        {
            _scrollRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _scrollExpandedHeight);
        }

        void createAndAddNewAttemptResultUI(AttemptResult result)
        {
            var attemptResultUI = Instantiate(_attemptResultUI, _transform);
            attemptResultUI.GetComponent<AttemptResultUI>().Init(result);
        }

        [SerializeField] float _addingNewAttemptResultSpeed;
        void shrinkScrollHieght(Action onCompleted)
        {
            DOTween.Kill(this, true);
            DOTween.To(
                () => _scrollRectTransform.sizeDelta.y,
                height => _scrollRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height),
                _scrollNormalHeight,
                _addingNewAttemptResultSpeed)
                   .OnComplete(() => onCompleted.Invoke());

            _scrollRect.velocity = new Vector2(0, 1000);
        }
        #endregion
    }
}
