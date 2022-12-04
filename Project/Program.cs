ChessBoard chessBoard = new ();

chessBoard.InitChesCells();
chessBoard.PrintCells();

Console.WriteLine(chessBoard.chessCell[1,5].GetShortName());
chessBoard.chessCell[1, 5].PrintName();

//перемещение 
chessBoard.ChessPieceMove(1,5,3,5);
chessBoard.PrintCells();