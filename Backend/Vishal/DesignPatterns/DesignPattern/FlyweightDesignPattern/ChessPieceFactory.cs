namespace FlyweightDesignPattern
{
    // Flyweight Factory: Manages and creates flyweight objects.
    public class ChessPieceFactory
    {
        private readonly Dictionary<string, ChessPiece> _pieces = new();

        public ChessPiece GetChessPiece(string name, string color)
        {
            string key = $"{name}_{color}";

            if (!_pieces.ContainsKey(key))
            {
                _pieces[key] = new ChessPiece(name, color);
                Console.WriteLine($"Created new ChessPiece: {name}, Color: {color}");
            }
            else
            {
                Console.WriteLine($"Reusing existing ChessPiece: {name}, Color: {color}");
            }

            return _pieces[key];
        }
    }
}
