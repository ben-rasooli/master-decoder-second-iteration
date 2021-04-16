using System;
using System.Linq;
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
            _attemptResult.Feedbacks = new List<Feedback>();

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
            Feedback totalFeedback = _attemptResult.Feedbacks
                          .First(feedback => feedback.ReferencingPieces.Count == _puzzle.Pieces.Count);

            if (totalFeedback.CorrectPieces == _puzzle.Pieces.Count){
                _signalBus.Fire<GameIsOverSignal>();
            }
        }
        #endregion

        #region ----------------------------------------details
        Puzzle _puzzle;
        AttemptResult _attemptResult;
        List<FeedbackGenerator> _feedbackGenerators = new List<FeedbackGenerator>();

        void getGuessPiecesFromGuessingPanel()
        {
            _attemptResult.Guess.Pieces.Clear();
            foreach (var guessPieceUI in _guessingPanel.GetComponentsInChildren<GuessingPanelPieceUI>())
                _attemptResult.Guess.Pieces.Add(guessPieceUI.Piece);
        }

        void setAttemptResultFeedbacks()
        {
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
