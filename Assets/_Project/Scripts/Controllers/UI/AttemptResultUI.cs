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

            foreach (var feedback in result.Feedbacks)
                foreach (var ui in FeedbackUIs)
                    if (ui.ReferencingPieces.SequenceEqual(feedback.ReferencingPieces))
                    {
                        ui.CorrectPieces.text = feedback.CorrectPieces.ToString();
                        ui.MisplacedPieces.text = feedback.MisplacedPieces.ToString();
                        ui.SimilarPieces.text = feedback.SimilarPieces.ToString();
                    }
        }

        public int CodeCount;
        public int SymbolCount;
        public int ColorCount;

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

    [System.Serializable]
    public class FeedbackUI
    {
        public List<int> ReferencingPieces;
        public TextMeshProUGUI CorrectPieces;
        public TextMeshProUGUI MisplacedPieces;
        public TextMeshProUGUI SimilarPieces;
    }
}
