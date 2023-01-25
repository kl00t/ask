if (args.Length > 0)
{
    var apiKey = "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";
    HttpClient httpClient = new ();
    httpClient.DefaultRequestHeaders.Add("authorization", $"Bearer {apiKey}");

}
else
{
    Console.WriteLine("You need to provide some input.");
}