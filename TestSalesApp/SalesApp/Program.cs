using Newtonsoft.Json;
using SalesApi.Models;
using System.Net.Http.Headers;

string url = "https://localhost:7324";
string userName = "user";
string userPwd = "qwerty";

Console.WriteLine("Отправляем запрос на отчет");
try
{
    string responseContent;
    using (HttpClient client = new HttpClient())
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
        "Basic", Convert.ToBase64String(
            System.Text.ASCIIEncoding.ASCII.GetBytes(
               $"{userName}:{userPwd}")));
        HttpResponseMessage message = client.GetAsync($"{url}/api/muffin/report").GetAwaiter().GetResult();
        responseContent = message.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        if (message.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            Console.WriteLine("Ошибка аунтефикации");
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

Console.WriteLine("Для начала работы введите любой символ, для выхода Q");
ConsoleKey key = Console.ReadKey().Key;
while (key != ConsoleKey.Q)
{
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
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Basic", Convert.ToBase64String(
                        System.Text.ASCIIEncoding.ASCII.GetBytes(
                           $"{userName}:{userPwd}")));
                    HttpResponseMessage message = client.PostAsync($"{url}/api/muffin?countMuffin={countMuffin}", null).GetAwaiter().GetResult();
                    responseContent = message.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    if (message.StatusCode == System.Net.HttpStatusCode.BadRequest)
                        Console.WriteLine(responseContent);
                    else if (message.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        Console.WriteLine("Ошибка аунтефикации");
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
    Console.WriteLine("Для продолжения работы введите любой символ, для выхода Q");
    key = Console.ReadKey().Key;
}
