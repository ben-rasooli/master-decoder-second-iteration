using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Project;

namespace Tests
{
    public class FeedbackGeneratorTests_
    {
        [Test]
        public void Calculate_throws_exception_if_guess_count_not_match_puzzle_count()
        {
            int puzzleCount = 4;
            FeedbackGenerator sut = new FeedbackGenerator(new Puzzle(puzzleCount), new List<int>());
            var nonMatchingGuessCount_lower = new Guess(puzzleCount - 1);
            var nonMatchingGuessCount_higher = new Guess(puzzleCount + 1);

            Assert.Throws<InvalidOperationException>(() => sut.Calculate(nonMatchingGuessCount_lower));
            Assert.Throws<InvalidOperationException>(() => sut.Calculate(nonMatchingGuessCount_higher));
        }

        [Test]
        public void Calculate_returns_CorrectPiece_when_guss_piece_is_same_as_puzzle_piece()
        {
            var combination = new List<int[]>{
                new[] { 1, 1 }
            };
            Puzzle puzzle = createPuzzle(combination);
            Guess guess = createGuess(combination);
            FeedbackGenerator sut = new FeedbackGenerator(puzzle, new List<int> { 0 });

            var actualFeedback = sut.Calculate(guess);

            Assert.AreEqual(1, actualFeedback.CorrectPieces);
            Assert.AreEqual(0, actualFeedback.MisplacedPieces);
        }

        [Test]
        public void Calculate_returns_MisplacedPiece_when_guss_piece_is_same_as_puzzle_piece_but_in_wrong_place()
        {
            Puzzle puzzle = createPuzzle(new List<int[]>{
                new[] { 1, 1 },
                new[] { 2, 1 }
            });
            Guess guess = createGuess(new List<int[]>{
                new[] { 2, 1 },
                new[] { 1, 1 }
            });
            FeedbackGenerator sut = new FeedbackGenerator(puzzle, new List<int> { 0, 1 });

            var actualFeedback = sut.Calculate(guess);

            Assert.AreEqual(0, actualFeedback.CorrectPieces);
            Assert.AreEqual(2, actualFeedback.MisplacedPieces);
        }

        [Test]
        public void Calculate_returns_SimilarPiece_when_guss_piece_has_same_symbol_as_puzzle_piece_but_with_different_color()
        {
            Puzzle puzzle = createPuzzle(new List<int[]>{
                new[] { 1, 2 }
            });
            Guess guess = createGuess(new List<int[]>{
                new[] { 1, 1 }
            });
            FeedbackGenerator sut = new FeedbackGenerator(puzzle, new List<int> { 0 });

            var actualFeedback = sut.Calculate(guess);

            Assert.AreEqual(0, actualFeedback.CorrectPieces);
            Assert.AreEqual(1, actualFeedback.SimilarPieces);
        }

        [Test]
        public void a_piece_can_produce_multiple_feedbacks()
        {
            Puzzle puzzle = createPuzzle(new List<int[]>{
                new[] { 1, 2 },
                new[] { 5, 3 } //null piece
            });
            Guess guess = createGuess(new List<int[]>{
                new[] { 1, 1 },
                new[] { 1, 2 }
            });
            FeedbackGenerator sut = new FeedbackGenerator(puzzle, new List<int> { 0, 1 });

            var actualFeedback = sut.Calculate(guess);

            Assert.AreEqual(0, actualFeedback.CorrectPieces);
            Assert.AreEqual(1, actualFeedback.MisplacedPieces);
            Assert.AreEqual(1, actualFeedback.SimilarPieces);
        }

        [Test]
        public void a_CorrectPiece_cannot_be_reevaluated_by_a_Subset_feedback_generator_if_it_is_ouside_of_its_scope()
        {
            Puzzle puzzle = createPuzzle(new List<int[]>{
                new[] { 1, 1 },
                new[] { 1, 1 },
                new[] { 2, 1 },
                new[] { 2, 1 },
                new[] { 3, 1 },
                new[] { 3, 1 }
            });
            FeedbackGenerator sut = new FeedbackGenerator(puzzle, new List<int> { 1, 2, 3, 4 });
            Guess guess = createGuess(new List<int[]>{
                    new []{4, 1},
                    new []{4, 1},
                    new []{4, 1},
                    new []{3, 1},
                    new []{3, 1},
                    new []{3, 1}
                });

            var actualFeedback = sut.Calculate(guess);

            Assert.AreEqual(1, actualFeedback.CorrectPieces);
            Assert.AreEqual(0, actualFeedback.MisplacedPieces);
        }

        [TestCaseSource(typeof(ArbitraryTestsData), "TestCases")]
        public void arbitraryTests(ArbitraryTestData data)
        {
            List<Feedback> actualFeedbacks = new List<Feedback>();
            for (int i = 0; i < data.FeedbackGenerators.Count; i++)
                actualFeedbacks.Add(data.FeedbackGenerators[i].Calculate(data.Guess));

            for (int i = 0; i < data.ExpectedFeedbacks.Count; i++)
            {
                Assert.AreEqual(data.ExpectedFeedbacks[i].CorrectPieces, actualFeedbacks[i].CorrectPieces);
                Assert.AreEqual(data.ExpectedFeedbacks[i].MisplacedPieces, actualFeedbacks[i].MisplacedPieces);
                Assert.AreEqual(data.ExpectedFeedbacks[i].SimilarPieces, actualFeedbacks[i].SimilarPieces);
            }
        }

        #region details
        public class ArbitraryTestsData
        {
            public static IEnumerable TestCases
            {
                get
                {
                    initializeData();

                    foreach (var data in arbitraryTestDataList)
                        yield return new TestCaseData(data).SetName(string.Format("{0}  {1}", data.Puzzle, data.Guess));
                }
            }
            static List<ArbitraryTestData> arbitraryTestDataList;

            static void initializeData()
            {
                arbitraryTestDataList = new List<ArbitraryTestData>();
                #region Arbitrary Test Data 1
                var data_1 = new ArbitraryTestData();
                data_1.Puzzle = createPuzzle(new List<int[]>
                {
                    new[] { 1, 1 },
                    new[] { 1, 1 },
                    new[] { 2, 1 },
                    new[] { 2, 1 },
                    new[] { 3, 1 },
                    new[] { 3, 1 }
                });
                data_1.Guess = createGuess(new List<int[]>
                {
                    new []{2, 1},
                    new []{1, 1},
                    new []{1, 1},
                    new []{3, 1},
                    new []{2, 1},
                    new []{2, 1}
                });
                data_1.FeedbackGenerators.Add(new FeedbackGenerator(data_1.Puzzle, new List<int> { 0, 1, 2, 3, 4, 5 }));
                data_1.FeedbackGenerators.Add(new FeedbackGenerator(data_1.Puzzle, new List<int> { 1, 2, 3, 4 }));
                data_1.ExpectedFeedbacks.Add(new Feedback { CorrectPieces = 1, MisplacedPieces = 4 });
                data_1.ExpectedFeedbacks.Add(new Feedback { CorrectPieces = 1, MisplacedPieces = 3 });

                arbitraryTestDataList.Add(data_1);
                #endregion

                #region Arbitrary Test Data 2
                var data_2 = new ArbitraryTestData();
                data_2.Puzzle = createPuzzle(new List<int[]>
                {
                    new[] { 1, 1 },
                    new[] { 1, 1 },
                    new[] { 2, 1 },
                    new[] { 2, 1 },
                    new[] { 3, 1 },
                    new[] { 3, 1 }
                });
                data_2.Guess = createGuess(new List<int[]>
                {
                    new []{3, 1},
                    new []{4, 1},
                    new []{1, 1},
                    new []{3, 1},
                    new []{3, 1},
                    new []{3, 1}
                });
                data_2.FeedbackGenerators.Add(new FeedbackGenerator(data_2.Puzzle, new List<int> { 0, 1, 2, 3, 4, 5 }));
                data_2.FeedbackGenerators.Add(new FeedbackGenerator(data_2.Puzzle, new List<int> { 1, 2, 3, 4 }));
                data_2.ExpectedFeedbacks.Add(new Feedback { CorrectPieces = 2, MisplacedPieces = 1 });
                data_2.ExpectedFeedbacks.Add(new Feedback { CorrectPieces = 1, MisplacedPieces = 1 });

                arbitraryTestDataList.Add(data_2);
                #endregion

                #region Arbitrary Test Data 3
                var data_3 = new ArbitraryTestData();
                data_3.Puzzle = createPuzzle(new List<int[]>
                {
                    new[] { 1, 1 },
                    new[] { 1, 1 },
                    new[] { 2, 1 },
                    new[] { 2, 1 },
                    new[] { 3, 1 },
                    new[] { 3, 1 }
                });
                data_3.Guess = createGuess(new List<int[]>
                {
                    new []{2, 1},
                    new []{1, 1},
                    new []{3, 1},
                    new []{1, 1},
                    new []{2, 1},
                    new []{1, 1}
                });
                data_3.FeedbackGenerators.Add(new FeedbackGenerator(data_3.Puzzle, new List<int> { 0, 1, 2, 3, 4, 5 }));
                data_3.FeedbackGenerators.Add(new FeedbackGenerator(data_3.Puzzle, new List<int> { 1, 2, 3, 4 }));
                data_3.ExpectedFeedbacks.Add(new Feedback { CorrectPieces = 1, MisplacedPieces = 4 });
                data_3.ExpectedFeedbacks.Add(new Feedback { CorrectPieces = 1, MisplacedPieces = 3 });

                arbitraryTestDataList.Add(data_3);
                #endregion

                #region Arbitrary Test Data 4
                var data_4 = new ArbitraryTestData();
                data_4.Puzzle = createPuzzle(new List<int[]>
                {
                    new[] { 1, 1 },
                    new[] { 1, 1 },
                    new[] { 2, 1 },
                    new[] { 2, 1 },
                    new[] { 3, 1 },
                    new[] { 3, 1 }
                });
                data_4.Guess = createGuess(new List<int[]>
                {
                    new []{2, 1},
                    new []{1, 1},
                    new []{3, 1},
                    new []{1, 1},
                    new []{2, 1},
                    new []{1, 1}
                });
                data_4.FeedbackGenerators.Add(new FeedbackGenerator(data_4.Puzzle, new List<int> { 0, 1, 2, 3, 4, 5 }));
                data_4.FeedbackGenerators.Add(new FeedbackGenerator(data_4.Puzzle, new List<int> { 1, 2, 3, 4 }));
                data_4.ExpectedFeedbacks.Add(new Feedback { CorrectPieces = 1, MisplacedPieces = 4 });
                data_4.ExpectedFeedbacks.Add(new Feedback { CorrectPieces = 1, MisplacedPieces = 3 });

                arbitraryTestDataList.Add(data_4);
                #endregion
            }
        }

        public class ArbitraryTestData
        {
            public Puzzle Puzzle { get; set; }
            public Guess Guess { get; set; }
            public List<FeedbackGenerator> FeedbackGenerators { get; set; }
            public List<Feedback> ExpectedFeedbacks { get; set; }

            public ArbitraryTestData()
            {
                FeedbackGenerators = new List<FeedbackGenerator>();
                ExpectedFeedbacks = new List<Feedback>();
            }
        }

        static Puzzle createPuzzle(List<int[]> values)
        {
            var result = new Puzzle(values.Count);

            for (int i = 0; i < values.Count; i++)
                result.SetPiece((PieceSymbol)(values[i][0] - 1), (PieceColor)(values[i][1] - 1), i);

            return result;
        }

        static Guess createGuess(List<int[]> values)
        {
            var result = new Guess(values.Count);

            for (int i = 0; i < values.Count; i++)
                result.SetPiece((PieceSymbol)(values[i][0] - 1), (PieceColor)(values[i][1] - 1), i);

            return result;
        }
        #endregion
    }
}
