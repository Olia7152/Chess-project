//****************************************************************************
//***              Сlass defines the cells of the chessboard               ***
//****************************************************************************
class ChessCell
{
  internal string color = "White";
  internal Chess? chess;
  internal string GetShortName()=>((chess == null)?"_ ":
                                  ""+chess.chessType.letter+chess.color[0]);
  internal void PrintName() => Console.WriteLine((chess == null) ? "Пустая клетка" : 
                                  $"{chess.chessType.figure} {chess.color}");
}