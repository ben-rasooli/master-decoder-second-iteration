using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

#pragma warning disable 0649, 0414
namespace Project
{
    public class ResultsController : IInitializable
    {
        #region ----------------------------------------dependencies
        [Inject] CodeGenerator _codeGenerator;
        [Inject(Id = "guessingPanel")] RectTransform _guessingPanel;
        [Inject] AttemptResultsUI _attemptResultsUI;
        [Inject] AttemptResultUI _attemptResultUI;
        #endregion

        #region ----------------------------------------API
        public void Initialize()
        {
            Code code = _codeGenerator.Generate();
            _attemptResult = new AttemptResult();
            _attemptResult.Guess = new Guess(code.Pieces.Count);
            _attemptResult.Feedbacks = new List<Feedback>();

            foreach (var feedbackUI in _attemptResultUI.FeedbackUIs)
                _feedbackGenerators.Add(
                    new FeedbackGenerator(
                        code, 
                        feedbackUI.ReferencingPieces));
        }

        public void Calculate(Action onCompleted)
        {
            _attemptResult.Guess.Pieces.Clear();
            foreach (var guessPieceUI in _guessingPanel.GetComponentsInChildren<GuessingPanelPieceUI>())
                _attemptResult.Guess.Pieces.Add(guessPieceUI.Piece);

            _attemptResult.Feedbacks.Clear();
            foreach (var feedbackGenerator in _feedbackGenerators)
            {
                Feedback feedback = feedbackGenerator.Calculate(_attemptResult.Guess);
                _attemptResult.Feedbacks.Add(feedback);
            }

            _attemptResultsUI.AddAttemptResult(_attemptResult, onCompleted);
        }
        #endregion

        #region ----------------------------------------details
        AttemptResult _attemptResult;
        List<FeedbackGenerator> _feedbackGenerators = new List<FeedbackGenerator>();
        #endregion
    }
}
