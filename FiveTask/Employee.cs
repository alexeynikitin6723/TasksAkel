using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveTask
{
  public class Employee
  {
    public string Name { get; }
    public List<DateTime> Vacations { get; private set; }

    public Employee(string name)
    {
      Name = name;
      Vacations = new List<DateTime>();
    }

    public void AddVacation(DateTime start, int duration)
    {
      for (int i = 0; i < duration; i++)
      {
        Vacations.Add(start.AddDays(i));
      }
    }
  }
}
