using System.Collections.Generic;

namespace Project
{
    public class OverallFeedbackGenerator
    {
        Puzzle _puzzle;

        public OverallFeedbackGenerator(Puzzle puzzle)
        {
            _puzzle = puzzle;
        }

        public OverallFeedback Calculate(Guess guess)
        {
            OverallFeedback result = new OverallFeedback();

            if (guess?.Pieces.Count != _puzzle.Pieces.Count)
                throw new System.InvalidOperationException("guess.Pieces length should match the puzzle.Pieces length");

            List<Piece> puzzlePieces = new List<Piece>(_puzzle.Pieces);
            List<Piece> guessPieces = new List<Piece>(guess.Pieces);

            setCorrectPieces(ref result, puzzlePieces, guessPieces);
            setSimilarPieces(ref result, puzzlePieces, guessPieces);
            setMisplacedPieces(ref result, puzzlePieces, guessPieces);

            return result;
        }

        void setCorrectPieces(ref OverallFeedback result, List<Piece> puzzlePieces, List<Piece> guessPieces)
        {
            for (int i = 0; i < puzzlePieces.Count; i++)
            {
                if (puzzlePieces[i].Equals(Piece.Null))
                    continue;

                if (puzzlePieces[i].Equals(guessPieces[i]))
                {
                    puzzlePieces[i] = Piece.Null;
                    guessPieces[i] = Piece.Null;

                    result.CorrectPieces++;
                }
            }
        }

        void setSimilarPieces(ref OverallFeedback result, List<Piece> puzzlePieces, List<Piece> guessPieces)
        {
            for (int i = 0; i < puzzlePieces.Count; i++)
            {
                if (puzzlePieces[i].Equals(Piece.Null))
                    continue;

                if (puzzlePieces[i].IsSimilar(guessPieces[i]))
                    result.SimilarPieces++;
            }
        }

        void setMisplacedPieces(ref OverallFeedback result, List<Piece> puzzlePieces, List<Piece> guessPieces)
        {
            for (int i = 0; i < puzzlePieces.Count; i++)
            {
                Piece guessPiece = guessPieces[i];
                if (guessPiece.Equals(Piece.Null))
                    continue;

                if (puzzlePieces.Contains(guessPiece))
                {
                    result.MisplacedPieces++;
                    puzzlePieces[puzzlePieces.IndexOf(guessPiece)] = Piece.Null;
                    guessPieces[i] = Piece.Null;
                }
            }
        }
    }
}
