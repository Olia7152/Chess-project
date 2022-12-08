using Classes;
ChessBoard chessBoard = new();

//chessBoard.PrintCells();

//Console.WriteLine(chessBoard.chessCell[1,5].GetShortName());
//chessBoard.chessCell[1, 5].PrintName();

// Set Active Cell
try
{
chessBoard.SetActiveCell(1,4);
Console.WriteLine($"{chessBoard.GetActiveCell()}");
}
catch (ArgumentNullException e)
{
  Console.WriteLine("Пустая клека не может быть активна");
  Console.WriteLine(e.ToString);
}

chessBoard.PrintCells();

Console.Write("Выход пешки. Нажмите кнопку:");
Console.ReadLine();

// Moving a chess piece 
chessBoard.ChessMove(3, 4);
chessBoard.PrintCells();
