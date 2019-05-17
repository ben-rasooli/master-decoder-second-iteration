namespace Project
{
    public class CodeGenerator
    {
        int _codeCount;
        int _symbolCount;
        int _colorCount;

        public CodeGenerator(int codeCount, int symbolCount, int colorCount)
        {
            _codeCount = codeCount;
            _symbolCount = symbolCount;
            _colorCount = colorCount;
        }

        public Code Generate()
        {
            Code result = new Code(_codeCount);

            for (int i = 0; i < _codeCount; i++)
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
