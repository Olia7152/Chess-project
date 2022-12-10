using Classes;
ChessBoard chessBoard = new();
chessBoard.StartPosition();
//chessBoard.RandomPosition(10);
chessBoard.PrintCells();

while(true)
{
  int x = 60, y = 3;
  Console.SetCursorPosition(x, y);
  Console.WriteLine("Х О Д   " + ((ChessBoard.isWhitesMove) ? "Б Е Л Ы Х   " : "Ч Е Р Н Ы Х"));
  Console.SetCursorPosition(x, y+15);
  Console.WriteLine("Выход из игры <Ctrl + C>");

  // Set Active Cell
  while(ChessBoard.numberMoves == 0)
  {
    try
    { 
      chessBoard.SetActiveCell();
    }
    catch (Exception e)
    {
      x = 45; y = 17;
      Console.SetCursorPosition(x, y);
      Console.WriteLine(e.Message);
    }
  }

  // Moving a chess piece 
  while(ChessBoard.numberMoves > 0)
  {
    try
    {
      chessBoard.ChessMove();
    }
    catch (Exception e)
    {
      x = 45; y = 17;
      Console.SetCursorPosition(x, y);
      Console.WriteLine(e.Message);
    }
    ChessBoard.isWhitesMove = !ChessBoard.isWhitesMove;
  }
}