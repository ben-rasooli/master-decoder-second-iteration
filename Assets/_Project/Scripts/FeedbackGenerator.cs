using System.Collections.Generic;
using System.Linq;

namespace Project
{
    public class FeedbackGenerator
    {
        Puzzle _puzzle;
        List<int> _piecesToCheck;

        public FeedbackGenerator(Puzzle puzzle, List<int> piecesToCheck)
        {
            _puzzle = puzzle;
            _piecesToCheck = piecesToCheck;
        }

        public Feedback Calculate(Guess guess)
        {
            Feedback result = new Feedback();

            if (guess?.Pieces.Count != _puzzle.Pieces.Count)
                throw new System.InvalidOperationException("guess.Pieces length should match the puzzle.Pieces length");

            List<Piece> puzzlePieces = new List<Piece>(_puzzle.Pieces);
            List<Piece> guessPieces = new List<Piece>(guess.Pieces);

            result.ReferencingPieces = _piecesToCheck;
            setCorrectPieces(ref result, puzzlePieces, guessPieces);
            setSimilarPieces(ref result, puzzlePieces, guessPieces);

            return result;
        }

        void setCorrectPieces(ref Feedback result, List<Piece> puzzlePieces, List<Piece> guessPieces)
        {
            for (int i = 0; i < puzzlePieces.Count; i++)
            {
                if (puzzlePieces[i].Equals(Piece.Null))
                    continue;

                if (puzzlePieces[i].Equals(guessPieces[i]))
                {
                    puzzlePieces[i] = Piece.Null;
                    guessPieces[i] = Piece.Null;

                    if (_piecesToCheck.Contains(i))
                        result.CorrectPieces++;
                }
            }
        }

        void setSimilarPieces(ref Feedback result, List<Piece> puzzlePieces, List<Piece> guessPieces)
        {
            for (int i = 0; i < puzzlePieces.Count; i++)
            {
                if (puzzlePieces[i].Equals(Piece.Null) || !_piecesToCheck.Contains(i))
                    continue;

                if (puzzlePieces[i].IsSimilar(guessPieces[i]))
                    result.SimilarPieces++;
            }
        }
    }
}
