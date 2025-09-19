namespace FlyweightDesignPattern
{
    public class ChessPiece
    {
        public string Name { get; }
        public string Color { get; }

        public ChessPiece(string name, string color)
        {
            Name = name;
            Color = color;
        }

        public void DisplayPosition(string position)
        {
            Console.WriteLine($"Piece: {Name}, Color: {Color}, Position: {position}");
        }
    }
}
