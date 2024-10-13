using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveTask
{
  public class VacationManager
  {
    private List<Employee> employees;
    private List<DayOfWeek> workingDays;

    public VacationManager(List<string> employeeNames)
    {
      employees = employeeNames.Select(name => new Employee(name)).ToList();
      workingDays = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Thursday, DayOfWeek.Friday };
    }

    public void GenerateVacations(Random random)
    {
      foreach (var employee in employees)
      {
        int vacationCount = 28;

        while (vacationCount > 0)
        {
          DateTime startDate = GetRandomWorkingDay(random);
          int vacationDays = random.Next(1, 3) * 7; // 7 or 14 days

          if (CanScheduleVacation(employee, startDate, vacationDays))
          {
            employee.AddVacation(startDate, vacationDays);
            vacationCount -= vacationDays;
          }
        }
      }
    }

    private DateTime GetRandomWorkingDay(Random random)
    {
      DateTime start = new DateTime(DateTime.Now.Year, 1, 1);
      DateTime end = new DateTime(DateTime.Now.Year, 12, 31);
      DateTime randomDate;

      do
      {
        randomDate = start.AddDays(random.Next(0, (end - start).Days));
      } while (!workingDays.Contains((DayOfWeek)randomDate.DayOfWeek));

      return randomDate;
    }

    private bool CanScheduleVacation(Employee employee, DateTime startDate, int duration)
    {
      var endDate = startDate.AddDays(duration);
      return !employee.Vacations.Any(date => date >= startDate && date < endDate);
    }

    public void ShowVacationPlan()
    {
      foreach (var employee in employees)
      {
        Console.WriteLine($"Дни отпуска {employee.Name} : ");
        foreach (var vacationDay in employee.Vacations)
        {
          Console.WriteLine(vacationDay.ToShortDateString());
        }
      }
    }
  }
}