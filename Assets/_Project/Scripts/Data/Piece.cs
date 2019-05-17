namespace Project
{
    public struct Piece
    {
        public PieceSymbol Symbol;
        public PieceColor Color;

        #region ----------------------------------------overrides
        public override string ToString()
        {
            return Symbol + " : " + Color;
        }

        public bool IsSimilar(Piece other)
        {
            return Symbol == other.Symbol;
        }

        public override bool Equals(object obj)
        {
            return Symbol == ((Piece)obj).Symbol && Color == ((Piece)obj).Color;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
