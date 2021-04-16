using System.Collections.Generic;

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
            setDisplacedPieces(ref result, puzzlePieces, guessPieces);

            return result;
        }

        void setCorrectPieces(ref Feedback result, List<Piece> puzzlePieces, List<Piece> guessPieces)
        {
            for (int i = 0; i < puzzlePieces.Count; i++)
            {
                if (puzzlePieces[i].Equals(_nullPiece))
                    continue;

                if (puzzlePieces[i].Equals(guessPieces[i]))
                {
                    puzzlePieces[i] = _nullPiece;
                    guessPieces[i] = _nullPiece;

                    if (_piecesToCheck.Contains(i))
                        result.CorrectPieces++;
                }
            }
        }

        void setSimilarPieces(ref Feedback result, List<Piece> puzzlePieces, List<Piece> guessPieces)
        {
            for (int i = 0; i < puzzlePieces.Count; i++)
            {
                if (puzzlePieces[i].Equals(_nullPiece) || !_piecesToCheck.Contains(i))
                    continue;

                if (puzzlePieces[i].IsSimilar(guessPieces[i]))
                    result.SimilarPieces++;
            }
        }

        void setDisplacedPieces(ref Feedback result, List<Piece> puzzlePieces, List<Piece> guessPieces)
        {
            foreach (var pieceIndex in _piecesToCheck)
            {
                Piece guessPiece = guessPieces[pieceIndex];
                if (guessPiece.Equals(_nullPiece))
                    continue;

                if (puzzlePieces.Contains(guessPiece))
                {
                    result.MisplacedPieces++;
                    puzzlePieces[puzzlePieces.IndexOf(guessPiece)] = _nullPiece;
                    guessPieces[pieceIndex] = _nullPiece;
                }
            }
        }

        #region ----------------------------------------details
        Piece _nullPiece = new Piece { Symbol = PieceSymbol.None, Color = PieceColor.None };
        #endregion
    }
}
