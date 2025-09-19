using GradeBook;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace GradeBookTests
{
    public delegate string writeLogDelegate(string logMessage);
    public class TypeTests
    {
        // value type test cases

        [Fact]
        public void ValueTypeAlsoPassByValue()
        {
            var x = GetInt(); // it will take the value from the return of GetInt()
            Assert.Equal(3, x);
        }

        private int GetInt()
        {
            return 3;
        }

        //we are trying to pass data of int my using ref type 

        [Fact]
        public void ValueTypeAlsoPassByReference()
        {
            var x = GetInt();
            SetInt(ref x);

            Assert.Equal(42, x);
        }

        private void SetInt(ref int z)
        {
            z = 42;
        }

        int count = 0;

        [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
            writeLogDelegate log = ReturnMessage;
            log += ReturnMessage;
            log += IncrementCount; 

            var result = log("Hello!");
            Assert.Equal(3, count);
        }

        string IncrementCount(string message)
        {
            count++;
            return message.ToUpper();
        }
        string ReturnMessage(string message)
        {
            count++;
            return message;
        }

        // Reference type test cases
        [Fact]
        public void CSharpCanPassByRef()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(ref book1, "New Name");
                                                   // in the place of "ref" we can also use "out" keyword which also pass by reference 
            Assert.Equal("New Name", book1.Name);
        }

        private void GetBookSetName(ref InMemoryBook book, string name) // ref means it wont receive copy of a value insted it get a reference to the memeory location of where the variable is stored
        {
            book = new InMemoryBook(name); 
        }

        [Fact]
       public void CSharpIsPassByValue()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(book1, "New Name");

            Assert.Equal("Book 1", book1.Name); // At this point name of the book is still Book 1 bcoz when we invoked GetBookSetName we made a copy of the value inside of variable book1
        }

        private void GetBookSetName(InMemoryBook book, string name)// the below book is parameter to this method
        {
            book = new InMemoryBook(name); // this is constructing a new book object and storing reference to that obj in book variable 
        }

        [Fact]
        public void CanSetNameFromReference()
        {
            var book1 = GetBook("Book 1"); //inside the book1 value is Book 1
            SetName(book1, "New Name"); // We can change the name by reference 

            Assert.Equal("New Name", book1.Name);
        }

        private void SetName(InMemoryBook book, string name) // we are taking the value of book1 and placing it inside the book that means it's acopy of that value
        {
            book.Name = name;  //Name is a Field
        }

        [Fact]
        public void StringBehaveLikeValueType()
        {
            string name = "vishal";
            var upper = MakeUpperCase(name);

            Assert.Equal("vishal", name);
            Assert.Equal("VISHAL", upper);
        }

        private string MakeUpperCase(string parameter)
        {
            return parameter.ToUpper();
        }

        [Fact]
        public void GetBookReturnsDifferentObjects()
        {
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");

            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 2", book2.Name);
            Assert.NotSame(book1, book2);

        }

        [Fact]
        public void TwoVarsCanReferenceSameObject()
        {
            var book1 = GetBook("Book 1");
            var book2 = book1;       //Two variables can reference same object in this book1 & book2 is referencing Book1
                                     //The above line takes the value inside of book1 that value is a pointer to reference and that value is copied to book2 variable
                                     //in simple terms taking the value inside of book1 variable and placing it inside of variable.
            Assert.Same(book1, book2);
            Assert.True(Object.ReferenceEquals(book1, book2));

        }

        InMemoryBook GetBook(string name)
        {
            return new InMemoryBook(name);
        }
    }
}