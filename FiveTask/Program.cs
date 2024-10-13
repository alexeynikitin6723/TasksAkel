namespace FiveTask
{
  class Program
  {
    static void Main(string[] args)
    {
      var employeeNames = new List<string>
            {
                "Иванов Иван Иванович", "Петров Петр Петрович",
                "Юлина Юлия Юлиановна", "Сидоров Сидор Сидорович",
                "Павлов Павел Павлович", "Георгиев Георг Георгиевич"
            };

      var vacationManager = new VacationManager(employeeNames);
      Random randomGen = new Random();

      vacationManager.GenerateVacations(randomGen);
      vacationManager.ShowVacationPlan();

      Console.ReadKey();
    }
  }
}
