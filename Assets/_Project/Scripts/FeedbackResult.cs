namespace Project
{
    public struct FeedbackResult
    {
        public int CorrectPieces;
        public int MisplacedPieces;
        public int SimilarPieces;

        public override bool Equals(object obj)
        {
            return 
                CorrectPieces == ((FeedbackResult)obj).CorrectPieces &&
                MisplacedPieces == ((FeedbackResult)obj).MisplacedPieces &&
                SimilarPieces == ((FeedbackResult)obj).SimilarPieces;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
