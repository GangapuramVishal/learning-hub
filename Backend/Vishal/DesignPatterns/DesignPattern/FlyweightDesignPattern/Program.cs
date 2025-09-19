namespace FlyweightDesignPattern
{
    public class Program
    {
        static void Main(string[] args)
        {
            ChessPieceFactory factory = new ChessPieceFactory();

            // Get shared chess pieces (intrinsic state) and manage their unique positions (extrinsic state)
            ChessPiece whitePawn1 = factory.GetChessPiece("Pawn", "White");
            whitePawn1.DisplayPosition("A2");

            ChessPiece blackPawn = factory.GetChessPiece("Pawn", "Black");
            blackPawn.DisplayPosition("A7");

            ChessPiece whitePawn2 = factory.GetChessPiece("Pawn", "White");
            whitePawn2.DisplayPosition("B2");

            ChessPiece whiteKnight = factory.GetChessPiece("Knight", "White");
            whiteKnight.DisplayPosition("G1");

            // Note: "whitePawn1" and "whitePawn2" share the same flyweight object.
        }
    }
}
