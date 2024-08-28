var year = DateTime.Now.Year;
var workerList = new List<string>
{
    "Иванов Иван Иванович",
    "Петров Петр Петрович",
    "Юлина Юлия Юлиановна",
    "Сидоров Сидор Сидорович",
    "Павлов Павел Павлович",
    "Георгиев Георг Георгиевич"
};


var vacationPlanner = new VacationPlanner(workerList, year);
var vacationDictionary = vacationPlanner.MakePlan();

var planConsoleSender = new PlanConsoleSender(vacationDictionary);
planConsoleSender.Send();




