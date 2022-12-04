//****************************************************************************
//***      Class ChessType defines the types of chess pieces               ***
//****************************************************************************
class ChessType
{
  internal string figure;
  internal char letter;
  string scheme;
  public ChessType(string figure = "Pawn", char letter = 'P', string scheme = "10")
  {
    this.figure = figure;
    this.letter = letter;
    this.scheme = scheme;
  }
  //internal void Print() => Console.WriteLine($"Имя: {figure}, Литера: {letter}");
}