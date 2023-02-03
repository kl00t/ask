using Newtonsoft.Json;
using System.Text;
using TextCopy;

if (args.Length > 0)
{
    var apiKey = "";

    HttpClient httpClient = new ();

    httpClient.DefaultRequestHeaders.Add("authorization", $"Bearer {apiKey}");

    var content = new StringContent("{\"model\": \"text-davinci-001\", \"prompt\": \"" + args[0] + "\",\"temperature\": 1,\"max_tokens\": 100}",
        Encoding.UTF8, "application/json");

    Uri requestUri = new("https://api.openai.com/v1/completions");
    
    HttpResponseMessage response = await httpClient.PostAsync(requestUri, content);

    string responseString = await response.Content.ReadAsStringAsync();

    try
    {
        var dynamicData = JsonConvert.DeserializeObject<dynamic>(responseString);

        GuessCommand(dynamicData!.choices[0].text);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Could not deserialize the JSON: {ex.Message}");
    }
}
else
{
    Console.WriteLine("You need to provide some input.");
}

static void GuessCommand(string raw)
{
    var lastIndex = raw.LastIndexOf('\n');

    string guess = raw.Substring(lastIndex + 1);

    Console.ForegroundColor = ConsoleColor.Green;

    Console.WriteLine(guess);

    Console.ResetColor();

    ClipboardService.SetText(guess);
}
