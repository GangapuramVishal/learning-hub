namespace ValuesFromAppSettings.Options
{
    //Binding AppSettings Values To Object
    public class EmployeeOptions
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public bool IsJunior { get; set; }
        public CategoryOptions Category { get; set; } 
    }
}
