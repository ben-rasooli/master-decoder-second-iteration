using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Project;

namespace Tests
{
    public class FeedbackGeneratorTests
    {
        [Test]
        public void Calculate_throws_exception_if_guess_count_not_match_puzzle_count()
        {
            int puzzleCount = 4;
            FeedbackGenerator sut_1 = new FeedbackGenerator(new Puzzle(puzzleCount), new List<int>());
            var nonMatchingGuessCount_lower_1 = new Guess(puzzleCount - 1);
            var nonMatchingGuessCount_higher_1 = new Guess(puzzleCount + 1);

            Assert.Throws<InvalidOperationException>(() => sut_1.Calculate(nonMatchingGuessCount_lower_1));
            Assert.Throws<InvalidOperationException>(() => sut_1.Calculate(nonMatchingGuessCount_higher_1));

            OverallFeedbackGenerator sut_2 = new OverallFeedbackGenerator(new Puzzle(puzzleCount));
            var nonMatchingGuessCount_lower_2 = new Guess(puzzleCount - 1);
            var nonMatchingGuessCount_higher_2 = new Guess(puzzleCount + 1);

            Assert.Throws<InvalidOperationException>(() => sut_2.Calculate(nonMatchingGuessCount_lower_2));
            Assert.Throws<InvalidOperationException>(() => sut_2.Calculate(nonMatchingGuessCount_higher_2));
        }

        class FeedbackTypes
        {
            [Test]
            public void A_guess_piece_is_Correct_when_it_is_the_same_as_its_corresponding_puzzle_piece()
            {
                var combination = new List<string>{
                "One-White"
            };
                Puzzle puzzle = createPuzzle(combination);
                Guess guess = createGuess(combination);
                OverallFeedbackGenerator sut = new OverallFeedbackGenerator(puzzle);

                var actualFeedback = sut.Calculate(guess);

                Assert.AreEqual(1, actualFeedback.CorrectPieces);
                Assert.AreEqual(0, actualFeedback.MisplacedPieces);
            }

            [Test]
            public void A_guess_piece_is_Misplaced_when_it_matches_a_puzzle_piece_but_their_places_do_not_match()
            {
                Puzzle puzzle = createPuzzle(new List<string>{
                "One-White",
                "Two-White"
            });
                Guess guess = createGuess(new List<string>{
                "Two-White",
                "One-White"
            });
                OverallFeedbackGenerator sut = new OverallFeedbackGenerator(puzzle);

                var actualFeedback = sut.Calculate(guess);

                Assert.AreEqual(0, actualFeedback.CorrectPieces);
                Assert.AreEqual(2, actualFeedback.MisplacedPieces);
            }

            [Test]
            public void A_guess_piece_is_Similar_when_it_has_same_symbol_as_its_corresponding_puzzle_piece_but_with_different_color()
            {
                Puzzle puzzle = createPuzzle(new List<string>{
                "One-Black"
            });
                Guess guess = createGuess(new List<string>{
                "One-White"
            });
                FeedbackGenerator sut = new FeedbackGenerator(puzzle, new List<int> { 0 });

                var actualFeedback = sut.Calculate(guess);

                Assert.AreEqual(0, actualFeedback.CorrectPieces);
                Assert.AreEqual(1, actualFeedback.SimilarPieces);
            }

            [Test]
            public void A_guess_piece_can_produce_Similar_and_Misplaced_feedbacks_simultaneously()
            {
                Puzzle puzzle = createPuzzle(new List<string>{
                "One-Black",
                "None-None" //null piece
            });
                Guess guess = createGuess(new List<string>{
                "One-White",
                "One-Black"
            });
                OverallFeedbackGenerator sut = new OverallFeedbackGenerator(puzzle);

                var actualFeedback = sut.Calculate(guess);

                Assert.AreEqual(0, actualFeedback.CorrectPieces);
                Assert.AreEqual(1, actualFeedback.MisplacedPieces);
                Assert.AreEqual(1, actualFeedback.SimilarPieces);
            }

            [Test]
            public void A_guess_piece_can_not_produce_Similar_or_Misplaced_feedback_when_it_is_Correct()
            {
                Puzzle puzzle = createPuzzle(new List<string>{
                "One-White",
                "None-None" //null piece
            });
                Guess guess = createGuess(new List<string>{
                "One-White",
                "One-White"
            });
                OverallFeedbackGenerator sut = new OverallFeedbackGenerator(puzzle);

                var actualFeedback = sut.Calculate(guess);

                Assert.AreEqual(1, actualFeedback.CorrectPieces);
                Assert.AreEqual(0, actualFeedback.MisplacedPieces);
                Assert.AreEqual(0, actualFeedback.SimilarPieces);
            }
        }

        class SubsetFeedbacks
        {

            [Test]
            public void a_CorrectPiece_cannot_be_reevaluated_by_a_Subset_feedback_generator_if_it_is_ouside_of_its_scope()
            {
                Puzzle puzzle = createPuzzle(new List<string>{
                "One-White",
                "One-White",
                "Two-White",
                "Two-White",
                "Three-White",
                "Three-White"
            });
                FeedbackGenerator sut = new FeedbackGenerator(puzzle, new List<int> { 1, 2, 3, 4 });
                Guess guess = createGuess(new List<string>{
                    "Four-White",
                    "Four-White",
                    "Four-White",
                    "Three-White",
                    "Three-White",
                    "Three-White"
                });

                var actualFeedback = sut.Calculate(guess);

                Assert.AreEqual(1, actualFeedback.CorrectPieces);
            }
        }

        //[TestCaseSource(typeof(ArbitraryTestsData), "TestCases")]
        //public void arbitraryTests(ArbitraryTestData data)
        //{
        //    List<Feedback> actualFeedbacks = new List<Feedback>();
        //    for (int i = 0; i < data.FeedbackGenerators.Count; i++)
        //        actualFeedbacks.Add(data.FeedbackGenerators[i].Calculate(data.Guess));

        //    for (int i = 0; i < data.ExpectedFeedbacks.Count; i++)
        //    {
        //        Assert.AreEqual(data.ExpectedFeedbacks[i].CorrectPieces, actualFeedbacks[i].CorrectPieces);
        //        Assert.AreEqual(data.ExpectedFeedbacks[i].MisplacedPieces, actualFeedbacks[i].MisplacedPieces);
        //        Assert.AreEqual(data.ExpectedFeedbacks[i].SimilarPieces, actualFeedbacks[i].SimilarPieces);
        //    }
        //}

        //public class ArbitraryTestsData
        //{
        //    public static IEnumerable TestCases
        //    {
        //        get
        //        {
        //            initializeData();

        //            foreach (var data in arbitraryTestDataList)
        //                yield return new TestCaseData(data).SetName(string.Format("{0}  {1}", data.Puzzle, data.Guess));
        //        }
        //    }
        //    static List<ArbitraryTestData> arbitraryTestDataList;

        //    static void initializeData()
        //    {
        //        arbitraryTestDataList = new List<ArbitraryTestData>();
        //        #region Arbitrary Test Data 1
        //        var data_1 = new ArbitraryTestData();
        //        data_1.Puzzle = createPuzzle(new List<string>
        //        {
        //            "One-White",
        //            "One-White",
        //            "Two-White",
        //            "Two-White",
        //            "Three-White",
        //            "Three-White"
        //        });
        //        data_1.Guess = createGuess(new List<string>
        //        {
        //            "Two-White",
        //            "One-White",
        //            "One-White",
        //            "Three-White",
        //            "Two-White",
        //            "Two-White"
        //        });
        //        data_1.FeedbackGenerators.Add(new FeedbackGenerator(data_1.Puzzle, new List<int> { 0, 1, 2, 3, 4, 5 }));
        //        data_1.FeedbackGenerators.Add(new FeedbackGenerator(data_1.Puzzle, new List<int> { 1, 2, 3, 4 }));
        //        data_1.ExpectedFeedbacks.Add(new Feedback { CorrectPieces = 1, MisplacedPieces = 4 });
        //        data_1.ExpectedFeedbacks.Add(new Feedback { CorrectPieces = 1, MisplacedPieces = 3 });

        //        arbitraryTestDataList.Add(data_1);
        //        #endregion

        //        #region Arbitrary Test Data 2
        //        var data_2 = new ArbitraryTestData();
        //        data_2.Puzzle = createPuzzle(new List<string>
        //        {
        //            "One-White",
        //            "One-White",
        //            "Two-White",
        //            "Two-White",
        //            "Three-White",
        //            "Three-White"
        //        });
        //        data_2.Guess = createGuess(new List<string>
        //        {
        //            "Three-White",
        //            "Four-White",
        //            "One-White",
        //            "Three-White",
        //            "Three-White",
        //            "Three-White"
        //        });
        //        data_2.FeedbackGenerators.Add(new FeedbackGenerator(data_2.Puzzle, new List<int> { 0, 1, 2, 3, 4, 5 }));
        //        data_2.FeedbackGenerators.Add(new FeedbackGenerator(data_2.Puzzle, new List<int> { 1, 2, 3, 4 }));
        //        data_2.ExpectedFeedbacks.Add(new Feedback { CorrectPieces = 2, MisplacedPieces = 1 });
        //        data_2.ExpectedFeedbacks.Add(new Feedback { CorrectPieces = 1, MisplacedPieces = 1 });

        //        arbitraryTestDataList.Add(data_2);
        //        #endregion

        //        #region Arbitrary Test Data 3
        //        var data_3 = new ArbitraryTestData();
        //        data_3.Puzzle = createPuzzle(new List<string>
        //        {
        //            "One-White",
        //            "One-White",
        //            "Two-White",
        //            "Two-White",
        //            "Three-White",
        //            "Three-White"
        //        });
        //        data_3.Guess = createGuess(new List<string>
        //        {
        //            "Two-White",
        //            "One-White",
        //            "Three-White",
        //            "One-White",
        //            "Two-White",
        //            "One-White"
        //        });
        //        data_3.FeedbackGenerators.Add(new FeedbackGenerator(data_3.Puzzle, new List<int> { 0, 1, 2, 3, 4, 5 }));
        //        data_3.FeedbackGenerators.Add(new FeedbackGenerator(data_3.Puzzle, new List<int> { 1, 2, 3, 4 }));
        //        data_3.ExpectedFeedbacks.Add(new Feedback { CorrectPieces = 1, MisplacedPieces = 4 });
        //        data_3.ExpectedFeedbacks.Add(new Feedback { CorrectPieces = 1, MisplacedPieces = 3 });

        //        arbitraryTestDataList.Add(data_3);
        //        #endregion

        //        #region Arbitrary Test Data 4
        //        var data_4 = new ArbitraryTestData();
        //        data_4.Puzzle = createPuzzle(new List<string>
        //        {
        //            "One-White",
        //            "One-White",
        //            "Two-White",
        //            "Two-White",
        //            "Three-White",
        //            "Three-White"
        //        });
        //        data_4.Guess = createGuess(new List<string>
        //        {
        //            "Two-White",
        //            "One-White",
        //            "Three-White",
        //            "One-White",
        //            "Two-White",
        //            "One-White"
        //        });
        //        data_4.FeedbackGenerators.Add(new FeedbackGenerator(data_4.Puzzle, new List<int> { 0, 1, 2, 3, 4, 5 }));
        //        data_4.FeedbackGenerators.Add(new FeedbackGenerator(data_4.Puzzle, new List<int> { 1, 2, 3, 4 }));
        //        data_4.ExpectedFeedbacks.Add(new Feedback { CorrectPieces = 1, MisplacedPieces = 4 });
        //        data_4.ExpectedFeedbacks.Add(new Feedback { CorrectPieces = 1, MisplacedPieces = 3 });

        //        arbitraryTestDataList.Add(data_4);
        //        #endregion
        //    }
        //}

        //public class ArbitraryTestData
        //{
        //    public Puzzle Puzzle { get; set; }
        //    public Guess Guess { get; set; }
        //    public List<FeedbackGenerator> FeedbackGenerators { get; set; }
        //    public List<Feedback> ExpectedFeedbacks { get; set; }

        //    public ArbitraryTestData()
        //    {
        //        FeedbackGenerators = new List<FeedbackGenerator>();
        //        ExpectedFeedbacks = new List<Feedback>();
        //    }
        //}

        #region details
        static Puzzle createPuzzle(List<string> pieces)
        {
            var result = new Puzzle(pieces.Count);

            for (int i = 0; i < pieces.Count; i++)
            {
                var pieceElements = pieces[i].Split('-');
                Piece piece = new Piece(
                    ParseEnum<PieceSymbol>(pieceElements[0]),
                    ParseEnum<PieceColor>(pieceElements[1]));
                result.SetPiece(i, piece.Symbol, piece.Color);
            }

            return result;
        }

        static Guess createGuess(List<string> pieces)
        {
            var result = new Guess(pieces.Count);

            for (int i = 0; i < pieces.Count; i++)
            {
                var pieceElements = pieces[i].Split('-');
                Piece piece = new Piece(
                    ParseEnum<PieceSymbol>(pieceElements[0]),
                    ParseEnum<PieceColor>(pieceElements[1]));
                result.SetPiece(i, piece.Symbol, piece.Color);
            }

            return result;
        }

        static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase: true);
        }
        #endregion
    }
}
