
using System.Collections.ObjectModel;
/// <summary>
/// Класс для отображения плана отпусков в консоли,
/// при расшерении можно сделать IPlanSender с Send() и подменять реализацию 
/// сделать например PlanEmailSender 
/// </summary>
public class PlanConsoleSender
{
    private readonly Dictionary<string, List<Vacation>> _vacationDictionary;

    public PlanConsoleSender(Dictionary<string, List<Vacation>> vacationDictionary)
    {
        _vacationDictionary = vacationDictionary;
    }

    public void Send()
    {
        foreach (var VacationList in _vacationDictionary)
        {
            var SetDateList = VacationList.Value;
            Console.WriteLine("Дни отпуска " + VacationList.Key + " : ");
            for (int i = 0; i < SetDateList.Count; i++)
            {
                Console.WriteLine($"C {SetDateList[i].StartDate} по {SetDateList[i].EndDate}");
            }
        }
    }
}