using System.Collections.Generic;
using System.Text;

namespace Project
{
    public class Guess
    {
        public List<Piece> Pieces { get; private set; }

        public Guess(int count)
        {
            Pieces = new List<Piece>();

            for (int i = 0; i < count; i++)
                Pieces.Add(new Piece { Symbol = PieceSymbol.One, Color = PieceColor.White });
        }

        public void SetPiece(PieceSymbol symbol, PieceColor color, int index)
        {
            Piece piece;
            piece.Symbol = symbol;
            piece.Color = color;
            Pieces[index] = piece;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Pieces.ForEach(x => sb.Append(string.Format("({0}-{1})", x.Symbol, x.Color.ToString().Substring(0, 1))));
            return string.Format("[Guess: {0}]", sb);
        }
    }
}
