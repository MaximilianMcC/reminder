using System.Net;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Nodes;

class Program
{
	public static void Main(string[] args)
	{
		// Check for if any arguments were supplied
		if (args.Length == 0)
		{
			Error("Insufficient arguments supplied.");
			return;
		}

		// Check for what they wanna do
		switch (args[0])
		{
			// Add a new reminder
			case "a":
			case "add":
			case "new":
			case "create":
				AddReminder(args);
				break;

			// Check for reminders
			default:
				GetReminders(args[0]).GetAwaiter().GetResult();
				return;
		}
	}

	private static void AddReminder(string[] args)
	{

	}

	private static async Task GetReminders(string gitHubUsername = null)
	{
		// Check for if they have committed to GitHub
		// TODO: Use guard clause
		//? Not using guard clause for readability
		if (gitHubUsername != null)
		{
			// Make a HTTP request to the GitHub API. Also add
			// heaps of headers to pretend to be real
			string requestUrl = @$"https://api.github.com/users/{gitHubUsername}/events?per_page=4";
			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36");
        	client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
        	client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");
        	client.DefaultRequestHeaders.Referrer = new Uri("https://www.example.com/page.html");
			HttpResponseMessage response = await client.GetAsync(requestUrl);

			// Get the response and parse it to JSON
			if (response.StatusCode != HttpStatusCode.OK)
			{
				Error($"Error while getting GitHub information: {response.StatusCode}");
				return;
			}
			string responseBody = await response.Content.ReadAsStringAsync();
			JsonDocument json = JsonDocument.Parse(responseBody);

			// Loop over every commit
			int timesCommittedToday = 0;
			foreach (JsonElement commit in json.RootElement.EnumerateArray())
			{
				// Get the time that the commit happened
				string timeString = commit.GetProperty("created_at").GetString();
				DateTime time = DateTime.Parse(timeString);

				// Check for if the commit happened today
				if (time.Date == DateTime.Today) timesCommittedToday++;
			}

			// Return a response depending on how many
			// commits they have done today
			if (timesCommittedToday == 0) Console.WriteLine("You still haven't committed today!");
			else if (timesCommittedToday == 3) Console.WriteLine("If you commit once more you can get the next stage of tile.");
		}


	}





	private static void Error(string errorMessage)
	{
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine(errorMessage);
		Console.ResetColor();
	}
}