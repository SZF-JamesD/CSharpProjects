private void InjectFakeLogicPuzzle(LogicPuzzle fakeLogicPuzzle)
{
    var field = typeof(PuzzleService)
        .GetField("_logicPuzzle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
    field.SetValue(_puzzleService, fakeLogicPuzzle);
}
