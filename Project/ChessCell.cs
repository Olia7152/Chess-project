//****************************************************************************
//***              Сlass defines the cells of the chessboard               ***
//****************************************************************************
namespace Classes;
class ChessCell
{
  internal string color = "white";
  internal bool possibleMove { get; set; } = false;
  internal Chess? chess { get; set; }
  internal void SetPossibleMove(bool isPossible) => possibleMove = isPossible;
  internal string GetShortName() => ((chess == null) ? "__" : "" + chess.chessType.letter + chess.color[0]) +
                                    ((this.possibleMove) ? "x" : " ");
  internal string GetScheme() => ((chess == null) ? "" : chess.chessType.scheme);
  internal void PrintName() => Console.WriteLine((chess == null) ? "Пустая клетка"
                               : $"{chess.chessType.figure} {chess.color}");
}