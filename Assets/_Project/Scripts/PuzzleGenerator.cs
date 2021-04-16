namespace Project
{
    public class PuzzleGenerator
    {
        int _puzzleCount;
        int _symbolCount;
        int _colorCount;

        public PuzzleGenerator(int puzzleCount, int symbolCount, int colorCount)
        {
            _puzzleCount = puzzleCount;
            _symbolCount = symbolCount;
            _colorCount = colorCount;
        }

        public Puzzle Generate()
        {
            Puzzle result = new Puzzle(_puzzleCount);

            for (int i = 0; i < _puzzleCount; i++)
            {
                int randomSymbolIndex = UnityEngine.Random.Range(0, _symbolCount);
                int randomColorIndex = UnityEngine.Random.Range(0, _colorCount);
                result.SetPiece((PieceSymbol)randomSymbolIndex, (PieceColor)randomColorIndex, i);
            }
#if UNITY_EDITOR
            UnityEngine.Debug.Log(result.ToString());
#endif
            return result;
        }
    }
}
