public class ExceptionHandler
{
    public static void HandleException(Exception ex)
    {
        Console.WriteLine("An error occurred: " + ex.Message);
    }
}