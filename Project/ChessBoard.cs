//****************************************************************************
//***       Class ChessBoard class contains chessboard cells               ***             
//****************************************************************************
class ChessBoard
{
  internal ChessCell[,] chessCell = new ChessCell[8, 8];

  //***   Arrangement of chess pieces at the beginning of the game   ***
  internal void InitChesCells()
  {
    for (int i=0; i<8; i++)
      for (int j=0; j<8; j++)
      {
        string colorCell = ((i+j) % 2==0 ? "white" : "black"),
        colorChess = (i<2) ? "white" : "black";

        Chess? currChess = null;
        if (i<2 || i>5)
        {
          ChessType figure = (i==0 || i==7) ? Chess.OptionalFigure(j) : Chess.pawn;
          currChess = new Chess {color=colorChess, chessType=figure};
        }
        chessCell[i, j] = new ChessCell {color=colorCell, chess=currChess};
      }
  }

  internal void PrintCells()
  {
    Console.WriteLine("\n        Новая игра");
    Console.WriteLine("   A  B  C  D  E  F  J  H   ");
    for (int i = 0; i < 8; i++)
    {
      Console.Write($"{8-i}  ");
      for (int j = 0; j < 8; j++)
        Console.Write($"{chessCell[7-i, j].GetShortName()} " + (j == 7 ? $"{8-i}  \n" : ""));
    }
    Console.WriteLine("   A  B  C  D  E  F  J  H   ");
  }

  internal void ChessPieceMove(int x1,int y1,int x2, int y2)
  {
    if (IsRange(x1, y1) && IsRange(x2, y2))
    {
      ChessCell tmpCell=chessCell[x2, y2];
      chessCell[x2,y2]=chessCell[x1, y1];;
      chessCell[x1,y1] = tmpCell;
    }
    else Console.WriteLine("Ход невозможен: выход за пределы доски");
  }

  bool IsRange(int x, int y) => (x>=0 && x<8 && y>=0 && y<8);
}