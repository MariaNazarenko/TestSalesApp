using Newtonsoft.Json;
using SalesApi.Models;

string url = "https://localhost:7324";
Console.WriteLine("Отправляем запрос на отчет");
try
{
    string responseContent;
    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage message = client.GetAsync($"{url}/api/muffin/report").GetAwaiter().GetResult();
        responseContent = message.Content.ReadAsStringAsync().GetAwaiter().GetResult();
    }

    var result = JsonConvert.DeserializeObject<List<Muffin>>(responseContent);
    if (result != null)
    {
        Console.WriteLine("Отчет");
        Console.WriteLine("Ид\tВремя создания\tСостояние");
        foreach (var muffin in result)
        {
            Console.Write(muffin.Id + "\t" + muffin.DateCreate + "\t");
            if (muffin.Status == StatusMaffin.Supplied)
                Console.WriteLine("Поставлена");
            else if (muffin.Status == StatusMaffin.Overdue)
                Console.WriteLine("Просрочена");
            else if (muffin.Status == StatusMaffin.Sold)
                Console.WriteLine("Продана");
        }
    }
    else
    {
        Console.WriteLine("Нет данных для отчета");
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

ConsoleKey key = ConsoleKey.Enter;
while (key != ConsoleKey.Q)
{
    Console.WriteLine("Для начала работы введите любой символ, для выхода Q");
    key = Console.ReadKey().Key;
    Console.WriteLine();
    Console.WriteLine("Введите кол-во маффинов:");
    var answer = Console.ReadLine();
    try
    {
        int countMuffin = int.Parse(answer);
        if (countMuffin > 0)
            try
            {
                string responseContent;
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage message = client.PostAsync($"{url}/api/muffin?countMuffin={countMuffin}", null).GetAwaiter().GetResult();
                    responseContent = message.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    if (message.StatusCode == System.Net.HttpStatusCode.BadRequest)
                        Console.WriteLine(responseContent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        else
            Console.WriteLine("Количество должно быть > 0");
    }
    catch 
    {
        Console.WriteLine("Неккоректно введено кол-во маффикнов");
    }
}
