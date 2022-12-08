//****************************************************************************
//***      Class ChessType defines the types of chess pieces               ***
//****************************************************************************
namespace Classes;
class ChessType
{
  internal string figure;
  internal char letter;
  internal string scheme;
  internal ChessType(string figure = "Pawn", char letter = 'P', string scheme = "10")
  {
    this.figure = figure;
    this.letter = letter;
    this.scheme = scheme;
  }
  //internal void Print() => Console.WriteLine($"Имя: {figure}, Литера: {letter}");
}