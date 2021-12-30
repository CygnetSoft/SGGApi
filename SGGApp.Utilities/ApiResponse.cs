namespace SGGApp.Utilities
{
    public class ApiResponse<T> where T : class
    {
        public string Status { get; set; }
        public T Result { get; set; }
    }
}
