//****************************************************************************
//***           Сlass defines a collection of chess pieces                 ***
//****************************************************************************
class Chess
{
  internal string color = "white";
  internal ChessType chessType= pawn;
  internal static ChessType pawn = new ChessType ("Pawn", 'P',"10");
  static ChessType knight = new ChessType ("knight", 'N', "10" );
  static ChessType bishop = new ChessType ("bishop", 'B', "10" );
  static ChessType queen  = new ChessType ("queen",  'Q', "10" );
  static ChessType king   = new ChessType ("king",   'K', "10" );
  static ChessType rook   = new ChessType ("rook",   'R', "10" );

  internal static ChessType OptionalFigure(int opt) => opt switch
  {
    0 => rook,  7 => rook,
    1 => knight,6 => knight,
    2 => bishop,5 => bishop,
    3 => king,  4 => queen,
    _ => pawn
  };
  internal void Print() => Console.WriteLine($"{chessType.figure} {color}");
}