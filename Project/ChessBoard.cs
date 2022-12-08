//****************************************************************************
//***       Class ChessBoard class contains chessboard cells               ***             
//****************************************************************************
namespace Classes;
class ChessBoard
{
  internal ChessCell[,] chessCell { get; } //= new ChessCell[8, 8];
  internal ChessCell? activeCell { get; set; }
  internal static int ActiveX { get; set; }
  internal static int ActiveY { get; set; }

  //***   Arrangement of chess pieces at the beginning of the game   ***
  //internal void InitChesCells()
  internal ChessBoard()
  {
    chessCell = new ChessCell[8, 8];
    for (int i = 0; i < 8; i++)
      for (int j = 0; j < 8; j++)
      {
        string colorCell = ((i + j) % 2 == 0 ? "white" : "black"),
        colorChess = (i < 2) ? "white" : "black";

        Chess? currChess = null;
        if (i < 2 || i > 5)
        {
          ChessType figure = (i == 0 || i == 7) ? Chess.OptionalFigure(j) : Chess.pawn;
          currChess = new Chess { color = colorChess, startPos = true, chessType = figure };
        }
        chessCell[i, j] = new ChessCell { color = colorCell, chess = currChess };
      }
  }

  internal void SetActiveCell(int activeX, int activeY)
  {
    if (!IsRange(activeX, activeY))
    {
      throw new ArgumentOutOfRangeException(nameof(ActiveX), "Выход за пределы шахматной доски");
    }
    if (chessCell[activeX, activeY] == null)
    {
      throw new ArgumentNullException(nameof(chessCell), "Пустая клетка не может быть активна");
    }
    //activeCell = 
    ActiveX = activeX;
    ActiveY = activeY;
    PossibleMove();
  }

  internal string GetActiveCell() => chessCell[ActiveX, ActiveY].GetShortName();
  internal void PrintCells()
  {
    //Console.WriteLine("\n        Новая игра");
    Console.WriteLine("\n    A   B   C   D   E   F   J   H   \n");
    for (int i = 0; i < 8; i++)
    {
      Console.Write($"{8 - i}  ");
      for (int j = 0; j < 8; j++)
      {
        Console.Write($"{chessCell[7 - i, j].GetShortName()} " + (j == 7 ? $"{8 - i}  \n" : ""));
      }
      Console.WriteLine();
    }
    Console.WriteLine("   A   B   C   D   E   F   J   H   \n");
  }

  internal void ChessMove(int moveToX, int moveToY)
  {
    if (IsRange(moveToX, moveToY))
    {
      Console.WriteLine($"{chessCell[ActiveX, ActiveY].possibleMove}");
      if (chessCell[moveToX, moveToY].possibleMove)
      {
        ChessCell tmpCell = chessCell[moveToX, moveToY];
        chessCell[moveToX, moveToY] = chessCell[ActiveX, ActiveY]; ;
        chessCell[ActiveX, ActiveY] = tmpCell;
        ActiveX = -1;
        ActiveY = -1;
        for (int i = 0; i < 8; i++)
        {
          for (int j = 0; j < 8; j++)
          {
            chessCell[i, j].SetPossibleMove(false);
            //chessCell[i, j].chess.SetStartPosition(false);
          }
        }
      }
      else Console.WriteLine("Херня");
    }
    else Console.WriteLine("Ход невозможен: выход за пределы шахматной доски");
  }

  // Search for possible moves
  void PossibleMove()
  {
    Console.WriteLine(chessCell[ActiveX, ActiveY].GetScheme()); //chess.chessType.scheme);
    string scheme = chessCell[ActiveX, ActiveY].GetScheme();
    int[] delta = Array.ConvertAll(chessCell[ActiveX, ActiveY].GetScheme()
                        .Split(new char[] { ' ', ',', '.', ',', '(', ')' },
                        System.StringSplitOptions.RemoveEmptyEntries), int.Parse);

    for (int k = 0; k < delta.Length; k += 2)
    {
      TryStep(delta[k], delta[k + 1]);
    }
  }

  void TryStep(int deltaI, int deltaJ)
  {
    Chess piece = chessCell[ActiveX, ActiveY].chess;
    ChessType figure = piece.chessType;

    bool isPawn = Equals(figure, Chess.pawn),
         isOneStep = Equals(figure, Chess.king) | Equals(figure, Chess.knight);

    if (isPawn & Equals(piece.color, "black")) deltaI = -deltaI;

    //Console.WriteLine(figure);
    int i = ActiveX + deltaI,
        j = ActiveY + deltaJ;

    while (IsRange(i, j))
    {
      //Console.WriteLine($"{i} {j} {deltaI} {deltaJ} {chessCell[i, j].chess}");
      if (chessCell[i, j].chess is null)
      {
        if (isPawn & deltaJ != 0) return;
        chessCell[i, j].SetPossibleMove(true);
        //Console.WriteLine("OO!");
        if (isOneStep | isPawn & i == ActiveX + deltaI * 2) return;
      }
      else if (chessCell[i, j].chess.color != piece.color)
      {
        if (isPawn & deltaJ == 0) return;
        else
        {
          chessCell[i, j].SetPossibleMove(true);
          //Console.WriteLine("OOO!!!");
          return;
        }
      }

      i += deltaI;
      j += deltaJ;
    }
  }

  bool IsRange(int x, int y) => (x >= 0 && x < 8 && y >= 0 && y < 8);
}