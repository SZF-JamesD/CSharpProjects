private void InjectFakeMathPuzzle(MathPuzzle fakeMathPuzzle)
{
    var field = typeof(PuzzleService)
        .GetField("_mathPuzzle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
    field.SetValue(_puzzleService, fakeMathPuzzle);
}