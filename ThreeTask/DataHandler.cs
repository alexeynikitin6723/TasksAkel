using ClosedXML.Excel;
using ConsoleApp4;
using System;
using System.Collections.Generic;
using System.Linq;

public class DataHandler
{
  private List<Product> products = new List<Product>();
  private List<Client> clients = new List<Client>();
  private List<Order> orders = new List<Order>();

  public void LoadData(string path)
  {
    using (var workbook = new XLWorkbook(path))
    {
      LoadProducts(workbook);
      LoadClients(workbook);
      LoadOrders(workbook);
    }
  }

  private void LoadProducts(XLWorkbook workbook)
  {
    var worksheet = workbook.Worksheet("Товары");
    foreach (var row in worksheet.RowsUsed().Skip(1))
    {
      products.Add(new Product
      {
        Code = (int)row.Cell(1).GetDouble(),
        Name = row.Cell(2).GetString(),
        Unit = row.Cell(3).GetString(),
        Price = Convert.ToDecimal(row.Cell(4).GetDouble())
      });
    }
  }

  private void LoadClients(XLWorkbook workbook)
  {
    var worksheet = workbook.Worksheet("Клиенты");
    foreach (var row in worksheet.RowsUsed().Skip(1))
    {
      clients.Add(new Client
      {
        Code = (int)row.Cell(1).GetDouble(),
        OrganizationName = row.Cell(2).GetString(),
        Address = row.Cell(3).GetString(),
        ContactPerson = row.Cell(4).GetString()
      });
    }
  }

  private void LoadOrders(XLWorkbook workbook)
  {
    var worksheet = workbook.Worksheet("Заявки");
    foreach (var row in worksheet.RowsUsed().Skip(1))
    {
      orders.Add(new Order
      {
        Code = (int)row.Cell(1).GetDouble(),
        ProductCode = (int)row.Cell(2).GetDouble(),
        ClientCode = (int)row.Cell(3).GetDouble(),
        OrderNumber = row.Cell(4).GetString(),
        Quantity = (int)row.Cell(5).GetDouble(),
        OrderDate = row.Cell(6).GetDateTime()
      });
    }
  }

  public IEnumerable<string> GetProductInfo(string productName)
  {
    var res = products.Select(p => p.Name = productName);
    
    return orders.Where(o => products.Any(p => p.Code == o.ProductCode && p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase)))
        .Select(o => new
        {
          Client = clients.First(c => c.Code == o.ClientCode),
          o.Quantity,
          o.OrderDate,
          Product = products.First(p => p.Code == o.ProductCode)
        })
        .Select(order => $"Клиент: {order.Client.OrganizationName}, Количество: {order.Quantity}, Цена: {order.Product.Price}, Дата заказа: {order.OrderDate}");
  }

  public string UpdateContactPerson(string organizationName, string newContactPerson)
  {
    var client = clients.FirstOrDefault(c => c.OrganizationName.Equals(organizationName, StringComparison.OrdinalIgnoreCase));
    if (client != null)
    {
      client.ContactPerson = newContactPerson;
      return "Контактное лицо обновлено.";
    }
    return "Клиент не найден.";
  }

  public string DetermineGoldClient(int year, int month)
  {
    var goldClient = orders.Where(o => o.OrderDate.Year == year && o.OrderDate.Month == month)
        .GroupBy(o => o.ClientCode)
        .OrderByDescending(g => g.Count())
        .FirstOrDefault();

    if (goldClient != null)
    {
      var bestClient = clients.First(c => c.Code == goldClient.Key);
      return $"Золотой клиент: {bestClient.OrganizationName}, Заказов: {goldClient.Count()}";
    }
    return "Заказов не найдено.";
  }

  public void SaveClientContact(string path, Client client)
  {
    using (var workbook = new XLWorkbook(path))
    {
      var worksheet = workbook.Worksheet("Клиенты");
      var row = worksheet.RowsUsed().FirstOrDefault(r => r.Cell(2).GetString() == client.OrganizationName);
      if (row != null)
      {
        row.Cell(4).Value = client.ContactPerson;
        workbook.Save();
      }
    }
  }
}
