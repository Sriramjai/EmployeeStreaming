namespace EmployeeInfoAPI.Data
{
    public class CursorParams
    {
        public int Count { get; set; } = 50; // setting the number of records to 50 as default
        public int Cursor { get; set; } = 0;
    }
}
