using Newtonsoft.Json;
using System.Text;
using TextCopy;

if (args.Length > 0)
{
    var apiKey = "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";

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

        string guess = GuessCommand(dynamicData!.choices[0].text);

        Console.ForegroundColor = ConsoleColor.Green;

        Console.WriteLine($"My guess is: {guess}");

        Console.ResetColor();
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

static string GuessCommand(string raw)
{
    Console.WriteLine("GPT-3 API returned text:");

    Console.ForegroundColor = ConsoleColor.Yellow;

    Console.WriteLine(raw);

    var lastIndex = raw.LastIndexOf('\n');

    string guess = raw.Substring(lastIndex + 1);

    Console.ResetColor();

    ClipboardService.SetText(guess);

    return guess;
}