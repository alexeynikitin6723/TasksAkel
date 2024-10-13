using ConsoleApp4;
using System;

public class Program
{
  private static DataHandler dataHandler = new DataHandler();

  public static void Main(string[] args)
  {
    string filePath = GetFilePath();
    dataHandler.LoadData(filePath);

    while (true)
    {
      Console.WriteLine("Выберите команду: 1 - Информация о товарах, 2 - Изменить контактное лицо, 3 - Золотой клиент, 0 - Выход");
      string command = Console.ReadLine();

      switch (command)
      {
        case "1":
          GetProductInfo();
          break;
        case "2":
          UpdateContactPerson(filePath);
          break;
        case "3":
          DetermineGoldClient();
          break;
        case "0":
          return;
        default:
          Console.WriteLine("Некорректная команда.");
          break;
      }
    }
  }

  private static string GetFilePath()
  {
    Console.Write("Введите путь к файлу: ");
    return Console.ReadLine();
  }

  private static void GetProductInfo()
  {
    Console.Write("Введите наименование товара: ");
    string productName = Console.ReadLine();
    var result = dataHandler.GetProductInfo(productName);
    if (result != null)
    {
      foreach (var info in result)
      {
        Console.WriteLine(info);
      }
    }
    else
    {
      Console.WriteLine("Данных нет");
    }
  }

  private static void UpdateContactPerson(string path)
  {
    Console.Write("Введите название организации: ");
    string organizationName = Console.ReadLine();
    Console.Write("Введите ФИО нового контактного лица: ");
    string newContactPerson = Console.ReadLine();

    var message = dataHandler.UpdateContactPerson(organizationName, newContactPerson);
    Console.WriteLine(message);

    if (message.Contains("обновлено"))
    {
      var client = new Client { OrganizationName = organizationName, ContactPerson = newContactPerson };
      dataHandler.SaveClientContact(path, client);
    }
  }

  private static void DetermineGoldClient()
  {
    Console.Write("Введите год: ");
    if (!int.TryParse(Console.ReadLine(), out int year))
    {
      Console.WriteLine("Некорректный ввод года.");
      return;
    }

    Console.Write("Введите месяц: ");
    if (!int.TryParse(Console.ReadLine(), out int month) || month < 1 || month > 12)
    {
      Console.WriteLine("Некорректный ввод месяца.");
      return;
    }

    var message = dataHandler.DetermineGoldClient(year, month);
    Console.WriteLine(message);
  }
}
