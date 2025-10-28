namespace ConsoleApp1;

public class DateTimeUtils
{
    public static DateTime ConvertToDateTime(String date)
    {
        if (DateTime.TryParse(date, out DateTime birthdate))
        {
            return  birthdate;
        }
        else
        {
            Console.WriteLine($"La date de P1 est mal renseignée");
            return DateTime.Now;
        }
    }
}