using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

#pragma warning disable 0649, 0414
namespace Project
{
    public class AttemptResultUI : MonoBehaviour
    {
        #region ----------------------------------------API
        public void Init(AttemptResult result)
        {
            for (int i = 0; i < result.Guess.Pieces.Count; i++)
                _pieceUIs[i].Init(result.Guess.Pieces[i]);

            OveralFeedbackUI.CorrectPieces.text = result.OveralFeedback.CorrectPieces.ToString();
            OveralFeedbackUI.SimilarPieces.text = result.OveralFeedback.SimilarPieces.ToString();
            OveralFeedbackUI.MisplacedPieces.text = result.OveralFeedback.MisplacedPieces.ToString();

            foreach (var feedback in result.Feedbacks)
                foreach (var ui in FeedbackUIs)
                    if (ui.ReferencingPieces.SequenceEqual(feedback.ReferencingPieces))
                    {
                        ui.CorrectPieces.text = feedback.CorrectPieces.ToString();
                        ui.SimilarPieces.text = feedback.SimilarPieces.ToString();
                    }
        }

        public int PuzzleCount;
        public int SymbolCount;
        public int ColorCount;

        public OveralFeedbackUI OveralFeedbackUI;
        public List<FeedbackUI> FeedbackUIs;
        #endregion

        #region ----------------------------------------Unity Messages
        void Awake()
        {
            _pieceUIs = GetComponentsInChildren<AttemptResultPieceUI>().ToList();
        }
        #endregion

        #region ----------------------------------------details
        List<AttemptResultPieceUI> _pieceUIs = new List<AttemptResultPieceUI>();
        #endregion
    }

    public class OveralFeedbackUI
    {
        public TextMeshProUGUI CorrectPieces;
        public TextMeshProUGUI SimilarPieces;
        public TextMeshProUGUI MisplacedPieces;
    }

    [System.Serializable]
    public class FeedbackUI
    {
        public List<int> ReferencingPieces;
        public TextMeshProUGUI CorrectPieces;
        public TextMeshProUGUI SimilarPieces;
    }
}
