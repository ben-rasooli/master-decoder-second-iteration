using System.Collections.Generic;

namespace Project
{
    public struct Feedback
    {
        public List<int> ReferencingPieces;
        public int CorrectPieces;
        public int MisplacedPieces;
        public int SimilarPieces;
    }
}
