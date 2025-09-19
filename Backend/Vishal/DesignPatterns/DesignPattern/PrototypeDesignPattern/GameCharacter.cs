namespace PrototypeDesignPattern
{
    /// <summary>
    /// A concrete implementation of the CharacterPrototype class.
    /// Represents a character in the game with various properties.
    /// </summary>
    public class GameCharacter : CharacterPrototype
    {
        public string Name { get; set; }
        public string Weapon { get; set; }
        public string SpecialAbility { get; set; }

        /// <summary>
        /// Constructor to initialize the character with default properties.
        /// </summary>
        /// <param name="name">The name of the character</param>
        /// <param name="weapon">The character's weapon</param>
        /// <param name="specialAbility">The special ability of the character</param>
        public GameCharacter(string name, string weapon, string specialAbility)
        {
            Name = name;
            Weapon = weapon;
            SpecialAbility = specialAbility;
        }

        /// <summary>
        /// Creates a shallow copy of the current character instance.
        /// </summary>
        /// <returns>A new GameCharacter instance with the same properties</returns>
        public override CharacterPrototype Clone()
        {
            Console.WriteLine($"Cloning character: {Name}");
            return (CharacterPrototype)MemberwiseClone();
        }

        /// <summary>
        /// Displays the character's details.
        /// </summary>
        public void ShowCharacterInfo()
        {
            Console.WriteLine($"Character Name: {Name}, Weapon: {Weapon}, Special Ability: {SpecialAbility}");
        }
    }
}
