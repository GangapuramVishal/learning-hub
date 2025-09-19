namespace PrototypeDesignPattern
{
    /// <summary>
    /// The Client class that demonstrates the use of the Prototype pattern.
    /// It clones existing characters and modifies their properties.
    /// </summary>
    public class Game
    {
        public static void Main(string[] args)
        {
            // Creating an original character (Prototype)
            GameCharacter warrior = new GameCharacter("Warrior", "Sword", "Shield Bash");
            warrior.ShowCharacterInfo();

            // Cloning the warrior character and modifying its properties
            GameCharacter rogue = (GameCharacter)warrior.Clone();
            rogue.Name = "Rogue";
            rogue.Weapon = "Daggers";
            rogue.SpecialAbility = "Backstab";
            rogue.ShowCharacterInfo();

            // Another clone with different modifications
            GameCharacter mage = (GameCharacter)warrior.Clone();
            mage.Name = "Mage";
            mage.Weapon = "Staff";
            mage.SpecialAbility = "Fireball";
            mage.ShowCharacterInfo();
        }
    }
}
