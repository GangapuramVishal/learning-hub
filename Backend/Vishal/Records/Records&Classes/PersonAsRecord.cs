using System;

public record PersonAsRecord(string ForstName, string LastName);

public record Employee(string FirstName, string LastName, int EmployeeId)
    : PersonAsRecord(FirstName, LastName);


/*Key Features of Records
 * Concise Syntax: Records provide a shorthand way of declaring data objects. The example above automatically creates properties,
 * a constructor, ToString(), Equals(), and GetHashCode() methods.
 * Value Equality: Unlike classes, which use reference equality by default, records use value equality. This means that two record
 * instances with the same data are considered equal.
 * Immutability: By default, records are immutable, meaning once they are created, their data cannot be changed. However, you can create mutable 
   records by using the init accessor or explicitly declaring properties with setters.
Deconstruction: Records support deconstruction, allowing you to extract property values easily.
*/
