//****************************************************************************
//***       Class ChessBoard class contains chessboard cells               ***             
//****************************************************************************
namespace Classes;
class ChessBoard
{
  internal ChessCell[,] chessCell { get; }
  internal ChessCell? activeCell { get; set; }
  internal static int activeX { get; set; }
  internal static int activeY { get; set; }
  internal static int numberMoves { get; set; } = 0;
  internal static bool isWhitesMove = true;
  

  //***   Arrangement of chess pieces at the beginning of the game   ***
  internal ChessBoard()
  {
    chessCell = new ChessCell[8, 8];

    for (int i = 0; i < 8; i++)
    {
      for (int j = 0; j < 8; j++)
      {
        Chess? currChess = null;
        chessCell[i, j] = new ChessCell { color = ((i + j) % 2 == 0 ? "white" : "black"), 
                          chess = null };
      }
    }
  }
  
  internal void StartPosition()
  { 
    for (int i = 0; i < 8; i++)
    {
      ChessType figure = Chess.OptionalFigure(i);
      ChessSet(0, i, "white", true, figure);
      ChessSet(7, i, "black", true, figure);
      ChessSet(6, i, "black", true, Chess.pawn);
      ChessSet(1, i, "white", true, Chess.pawn);
    }
  }

  internal void RandomPosition(int n)
  {
    for (int i = 0; i < n; i++ )
    {
      ChessSet(new Random().Next(0, 8), new Random().Next(0, 8), ((i%2==0)?"black":"white"),
              false, Chess.OptionalFigure(new Random().Next(0, 6)));
    }
  }
  internal void SetActiveCell(params int[] coord)
  { 
    if (coord.Length <2)
    {
      coord = new int[2];
      int x = 45, y = 7;
      Console.SetCursorPosition(x, y);
      if (!ReadPosition("Активная фигура(введите клетку, например E2): ", ref coord[0], ref coord[1]))
      {
        throw new ArgumentException(nameof(coord), "Неверный ввод");
      }
    }
    else if (!IsRange(coord[0], coord[1]))
    {
      throw new ArgumentOutOfRangeException(nameof(activeX), "Выход за пределы шахматной доски");
    }

    if (chessCell[coord[1], coord[0]].chess == null)
    {
      throw new ArgumentNullException(nameof(chessCell), "Пустая клетка не может быть активна");
    }

    if (((isWhitesMove)?"white":"black") != chessCell[coord[1], coord[0]].chess.color)
    {
      throw new ArgumentNullException(nameof(chessCell), "Невозможно играть фигурой противника");
    }
    activeX = coord[1];
    activeY = coord[0];
    PossibleMove();
    if (numberMoves == 0)
    {
      activeX = -1;
      activeY = -1;
      throw new ArgumentNullException(nameof(chessCell), "Отсутствуют варианты ходов");
    }
  }

  internal string GetActiveCell() => chessCell[activeX, activeY].GetShortName();

  internal void ChessSet(int x, int y, string colorChess, bool startPos, ChessType figure)
  {
    chessCell[x, y].chess = new Chess { color = colorChess, startPos = true, chessType = figure };
  }

  bool IsRange(int x, int y) => (x >= 0 && x < 8 && y >= 0 && y < 8);
  bool IsActive() => (activeX >= 0);
  
  internal void ChessMove(params int[] coord)
  {
    if (coord.Length < 2)
    {
      coord = new int[2];
      int x = 45, y = 11;
      Console.SetCursorPosition(x, y);
      if (!ReadPosition("Выполнить ход (введите клетку, например E4): ", ref coord[0], ref coord[1]))
      {
        throw new ArgumentException(nameof(coord), "Неверный ввод");
      }
    }

    if (!IsRange(coord[0], coord[1]))
    {
      throw new ArgumentOutOfRangeException(nameof(activeX), "Ход невозможен: выход за пределы шахматной доски"); 
    }
    
    if (!chessCell[coord[1], coord[0]].possibleMove)
    {
      throw new ArgumentOutOfRangeException(nameof(coord), $"Ход не допустим для данной фигуры");
    }
    ChessCell tmp = chessCell[coord[1], coord[0]];
    chessCell[coord[1], coord[0]] = chessCell[activeX, activeY]; ;
    chessCell[activeX, activeY] = tmp;
    chessCell[activeX, activeY].chess = null;

    PrintCell(coord[1], coord[0]);
    PrintCell(activeX, activeY);
    activeX = -1;
    activeY = -1;
    numberMoves = 0;

    for (int i = 0; i < 8; i++)
    {
      for (int j = 0; j < 8; j++)
      {
        if (chessCell[i, j].possibleMove)
        {
          chessCell[i, j].SetPossibleMove(false);
          PrintCell(i, j);
        }
      }
    }
  }

  // Search for possible moves
  void PossibleMove()
  {
    string scheme = chessCell[activeX, activeY].GetScheme();
    int[] delta = Array.ConvertAll(chessCell[activeX, activeY].GetScheme()
                        .Split(new char[] { ' ', ',', '.', ',', '(', ')' },
                        System.StringSplitOptions.RemoveEmptyEntries), int.Parse);
    numberMoves = 0;
    for (int k = 0; k < delta.Length; k += 2)
    {
      TryStep(delta[k], delta[k + 1]);
    }
  }

  void TryStep(int deltaI, int deltaJ)
  {
    Chess piece = chessCell[activeX, activeY].chess;
    ChessType figure = piece.chessType;

    bool isPawn = Equals(figure, Chess.pawn),
         isOneStep = Equals(figure, Chess.king) 
                    || Equals(figure, Chess.knight) 
                    || !piece.startPos && isPawn;

    if (isPawn & Equals(piece.color, "black")) deltaI = -deltaI;

    int i = activeX + deltaI,
        j = activeY + deltaJ;
    
    while (IsRange(i, j))
    {
      if (chessCell[i, j].chess is null)
      {
        if (isPawn & deltaJ != 0) return;
        chessCell[i, j].SetPossibleMove(true);
        numberMoves++;
        PrintCell(i, j);
        piece.startPos=false;
        if (isOneStep || (isPawn &  i == activeX + deltaI * 2)) return;
      }
      else if (chessCell[i, j].chess.color != piece.color)
      {
        if (isPawn & deltaJ == 0) return;
        else
        {
          chessCell[i, j].SetPossibleMove(true);
          PrintCell(i, j);
          numberMoves++;
          return;
        }
      }
      else  return;

      i += deltaI;
      j += deltaJ;
    } 
  }

  internal bool ReadPosition(string title, ref int x, ref int y)
  {
    Console.Write(title);
    string numCell = (Console.ReadLine() ?? String.Empty).ToUpper();
    if (numCell.Length != 2) 
    {
      return false;
    }

    if (numCell[0] < 'A' | numCell[0] > 'H' | numCell[1] > '8' | numCell[1] < '1')
    {
      return false;
    }
    x = (int)numCell[0] - 65;
    y = (int)numCell[1] - 49;
    return true;
  }

  internal void PrintCells()
  { 
    int x = 5, y = 1;
    Console.Clear();
    Console.SetCursorPosition(x, y);
    Console.Write("    A   B   C   D   E   F   G   H   ");
    for (int i = 0; i < 8; i++)
    {
      y+=2;
      Console.SetCursorPosition(x, y);
      Console.Write($"{8 - i}  ");
      for (int j = 0; j < 8; j++)
      {
        Console.Write($"{chessCell[7 - i, j].GetShortName()} " + (j == 7 ? $"{8 - i} " : ""));
      }
    }
    y += 2;
    Console.SetCursorPosition(x, y);
    Console.Write("   A   B   C   D   E   F   G   H   ");
    y += 2;
    Console.SetCursorPosition(x, y);
  }

  internal void PrintCell(int i, int j)
  {
    Console.SetCursorPosition(4 + (j+1)  * 4, 1 + (8 - i) * 2);
    Console.Write($"{chessCell[i, j].GetShortName()} "); 
  }
}