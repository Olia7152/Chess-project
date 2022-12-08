//****************************************************************************
//***           Сlass defines a collection of chess pieces                 ***
//****************************************************************************
namespace Classes;
class Chess
{
  internal string color { get; set; } = "white";
  internal bool startPos = true;
  internal ChessType chessType { get; set; } = pawn;
  internal static ChessType pawn = new ChessType("Pawn", 'P', "(1,0),(1,-1),(1,1))");
  internal static ChessType knight = new ChessType("Knight", 'N', "(2,1),(2,-1),(-1,-2),(-1,2),(-2,1),(-2,-1),(1,-2),(1,2)");
  internal static ChessType king = new ChessType("King", 'K', "(1,0),(1,1),(0,1),(-1,1),(-1,0),(-1,-1),(0,-1),(1,-1)");
  static ChessType bishop = new ChessType("Bishop", 'B', "(1,1),(1-1),(-1,-1),(-1,1)");
  static ChessType queen = new ChessType("Queen", 'Q', "(1,0),(1,1),(0,1),(-1,1),(-1,0),(-1,-1),(0,-1),(1,-1)");
  static ChessType rook = new ChessType("Rook", 'R', "(1,0),(0,1),(-1,0),(0,-1),(1,-1)");

  internal static ChessType OptionalFigure(int opt) => opt switch
  {
    0 => rook,
    7 => rook,
    1 => knight,
    6 => knight,
    2 => bishop,
    5 => bishop,
    3 => king,
    4 => queen,
    _ => pawn
  };
  internal void SetStartPosition(bool isStartPos) => this.startPos = isStartPos;
  internal void Print() => Console.WriteLine($"{chessType.figure} {color}");
}