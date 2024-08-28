
public class VacationPlanner
{
    private readonly ICollection<string> _workerList;
    private readonly int _year;

    public VacationPlanner(ICollection<string> workerList, int year)
    {
        _workerList = workerList;
        _year = year;
    }

    public Dictionary<string, List<Vacation>> MakePlan()
    {
        Random random = new Random();

        //vacationDictionary -
        //класс Vacation cо startDate и endDate  
        //string - работник, (можно еще добавить класс работника, но остановимся на данном варианте)

        Dictionary<string, List<Vacation>> vacationDictionary = new Dictionary<string, List<Vacation>>();

        DateTime yearStart = new DateTime(_year, 1, 1);
        DateTime yearEnd = new DateTime(_year, 12, 31);
        string[] vacationSteps = { "7", "14" };

        //заполняем vacationDictionary работниками и пустым листом отпусков
        foreach (var worker in _workerList)
        {
            vacationDictionary.Add(worker, new List<Vacation>());
        }

        foreach (var plan in vacationDictionary)
        {
            int vacationCount = 28;
            int range = (yearEnd - yearStart).Days;

            while (vacationCount > 0)
            {
                var vacationStartDate = yearStart.AddDays(random.Next(range));

                //проверяем что отпуск не начинается в выходной
                if (Enum.IsDefined(typeof(AviableWorkingDays), vacationStartDate.DayOfWeek.ToString()))
                {
                    int vacIndex = random.Next(vacationSteps.Length);

                    var vacationEndDate = new DateTime(_year, 12, 31);
                    int difference = 0;

                    if (vacationCount <= 7)
                    {
                        vacationEndDate = vacationStartDate.AddDays(7);
                        difference = 7;
                    }
                    else if (vacationSteps[vacIndex] == "7")
                    {
                        vacationEndDate = vacationStartDate.AddDays(7);
                        difference = 7;
                    }
                    else if (vacationSteps[vacIndex] == "14")
                    {
                        vacationEndDate = vacationStartDate.AddDays(14);
                        difference = 14;
                    }

                    // Проверка условий по отпуску
                    bool canCreateVacation = false;
                    bool existStart = false;
                    bool existEnd = false;

                    //проверяем нет ли заранее запланированного отпуска в планируемое время 
                    if (!plan.Value.Any(element => element.StartDate >= vacationStartDate && element.EndDate <= vacationEndDate))
                    {
                        //Принимаем за условие, что отпуск не может быть раньше чем через месяц от прошлого отпуска

                        //проверяем нет ли начала отпуска позже в течении месяца от планируемого конца отпуска
                        existStart = plan.Value.Any(element => element.StartDate >= vacationEndDate && element.StartDate <= vacationEndDate.AddMonths(1));

                        //проверяем нет ли конца отпуска ранее в течении месяца от планируемого начала отпуска
                        existEnd = plan.Value.Any(element => element.EndDate <= vacationStartDate && element.EndDate >= vacationStartDate.AddMonths(-1));
                        if (!existStart && !existEnd)
                            canCreateVacation = true;
                    }

                    if (canCreateVacation)
                    {
                        //добавляем отпуск данному работнику
                        plan.Value.Add(new Vacation() { 
                            Id = Guid.NewGuid(),
                            StartDate = vacationStartDate, 
                            EndDate = vacationEndDate });
                       
                        vacationCount = vacationCount - difference;
                    }
                }
            }
        }

        return vacationDictionary;
    }
}
