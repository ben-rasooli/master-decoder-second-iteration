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
        [Inject] SignalBus _signalBus;
        [Inject] PuzzleGenerator _puzzleGenerator;
        [Inject(Id = "guessingPanel")] RectTransform _guessingPanel;
        [Inject] AttemptResultsUI _attemptResultsUI;
        [Inject] AttemptResultUI _attemptResultUI;
        #endregion

        #region ----------------------------------------API
        public void Initialize()
        {
            _puzzle = _puzzleGenerator.Generate();
            _attemptResult = new AttemptResult();
            _attemptResult.Guess = new Guess(_puzzle.Pieces.Count);
            _attemptResult.OveralFeedback = new OverallFeedback();
            _attemptResult.Feedbacks = new List<Feedback>();

            _overallFeedbackGenerator = new OverallFeedbackGenerator(_puzzle);

            foreach (var feedbackUI in _attemptResultUI.FeedbackUIs)
                _feedbackGenerators.Add(
                    new FeedbackGenerator(
                        _puzzle,
                        feedbackUI.ReferencingPieces));
        }

        public void Calculate(Action onCompleted)
        {
            getGuessPiecesFromGuessingPanel();
            setAttemptResultFeedbacks();
            _attemptResultsUI.AddAttemptResult(_attemptResult, onCompleted);
            checkGameOverCondition();
        }

        void checkGameOverCondition()
        {
            if (_attemptResult.OveralFeedback.CorrectPieces == _puzzle.Pieces.Count)
                _signalBus.Fire<GameIsOverSignal>();
        }
        #endregion

        #region ----------------------------------------details
        Puzzle _puzzle;
        AttemptResult _attemptResult;
        OverallFeedbackGenerator _overallFeedbackGenerator;
        List<FeedbackGenerator> _feedbackGenerators = new List<FeedbackGenerator>();

        void getGuessPiecesFromGuessingPanel()
        {
            _attemptResult.Guess.Pieces.Clear();
            foreach (var guessPieceUI in _guessingPanel.GetComponentsInChildren<GuessingPanelPieceUI>())
                _attemptResult.Guess.Pieces.Add(guessPieceUI.Piece);
        }

        void setAttemptResultFeedbacks()
        {
            OverallFeedback overalFeedback = _overallFeedbackGenerator.Calculate(_attemptResult.Guess);
            _attemptResult.OveralFeedback = overalFeedback;

            _attemptResult.Feedbacks.Clear();
            foreach (var feedbackGenerator in _feedbackGenerators)
            {
                Feedback feedback = feedbackGenerator.Calculate(_attemptResult.Guess);
                _attemptResult.Feedbacks.Add(feedback);
            }
        }
        #endregion
    }
}
