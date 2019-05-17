using System.Collections.Generic;

namespace Project
{
    public class FeedbackGenerator
    {
        Code _code;
        List<int> _piecesToCheck;

        public FeedbackGenerator(Code code, List<int> piecesToCheck)
        {
            _code = code;
            _piecesToCheck = piecesToCheck;
        }

        public Feedback Calculate(Guess guess)
        {
            Feedback result = new Feedback();

            if (guess?.Pieces.Count != _code.Pieces.Count)
                throw new System.InvalidOperationException("guess.Pieces length should match the code.Pieces length");

            List<Piece> codePieces = new List<Piece>(_code.Pieces);
            List<Piece> guessPieces = new List<Piece>(guess.Pieces);

            result.ReferencingPieces = _piecesToCheck;
            setCorrectPieces(ref result, codePieces, guessPieces);
            setSimilarPieces(ref result, codePieces, guessPieces);
            setDisplacedPieces(ref result, codePieces, guessPieces);

            return result;
        }

        void setCorrectPieces(ref Feedback result, List<Piece> codePieces, List<Piece> guessPieces)
        {
            for (int i = 0; i < codePieces.Count; i++)
            {
                if (codePieces[i].Equals(_nullPiece))
                    continue;

                if (codePieces[i].Equals(guessPieces[i]))
                {
                    codePieces[i] = _nullPiece;
                    guessPieces[i] = _nullPiece;

                    if (_piecesToCheck.Contains(i))
                        result.CorrectPieces++;
                }
            }
        }

        void setSimilarPieces(ref Feedback result, List<Piece> codePieces, List<Piece> guessPieces)
        {
            for (int i = 0; i < codePieces.Count; i++)
            {
                if (codePieces[i].Equals(_nullPiece) || !_piecesToCheck.Contains(i))
                    continue;

                if (codePieces[i].IsSimilar(guessPieces[i]))
                    result.SimilarPieces++;
            }
        }

        void setDisplacedPieces(ref Feedback result, List<Piece> codePieces, List<Piece> guessPieces)
        {
            foreach (var pieceIndex in _piecesToCheck)
            {
                Piece guessPiece = guessPieces[pieceIndex];
                if (guessPiece.Equals(_nullPiece))
                    continue;

                if (codePieces.Contains(guessPiece))
                {
                    result.MisplacedPieces++;
                    codePieces[codePieces.IndexOf(guessPiece)] = _nullPiece;
                    guessPieces[pieceIndex] = _nullPiece;
                }
            }
        }

        #region ----------------------------------------details
        Piece _nullPiece = new Piece { Symbol = PieceSymbol.None, Color = PieceColor.None };
        #endregion
    }
}
