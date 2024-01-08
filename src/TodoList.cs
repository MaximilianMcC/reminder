class TodoList
{
	public List<TodoItem> Items;

	public TodoList(string filePath)
	{
		// Open the td file and extract all values
		string[] lines = File.ReadAllLines(filePath);

		// Loop through every item and add it to the list
		Items = new List<TodoItem>();
		foreach (string line in lines)
		{
			Items.Add(new TodoItem(line));
		}

		// Add a fake empty one at the end
		Items.Add(new TodoItem(""));
	}

	public void DrawList()
	{
		// Get all of the needed measurements for drawing the list
		int consoleWidth = Console.WindowWidth;
		int padding = consoleWidth / 10;
		string paddingLeft = new string(' ', padding);
		int listWidth = consoleWidth - (padding * 2) - 6; //? 6 comes from -2 for sides, -4 for checkbox area

		// Draw the top section
		Console.WriteLine($"{paddingLeft}┌───╥" + new string('─', listWidth) + '┐');

		// Draw all of the items in the middle section
		for (int i = 0; i < Items.Count; i++)
		{
			// Get the current item
			TodoItem item = Items[i];

			// Calculate the whitespace, and turn the bool to a string
			char done = item.Done ? '✓' : ' ';
			string whitespace = new string(' ', listWidth - item.Text.Length);

			// Don't print the top if we're on the first item
			if (i != 0) Console.WriteLine($"{paddingLeft}├───╫" + new string('─', listWidth) + '│');

			// Print the item
			Console.WriteLine($"{paddingLeft}│ {done} ║{item.Text}{whitespace}│");
		}

		// Draw the bottom section
		Console.WriteLine($"{paddingLeft}└───╨" + new string('─', listWidth) + '┘');
	}
}





struct TodoItem
{
	public bool Done { get; set; }
	public string Text { get; set; }

	// Parse the line from the td file
	public TodoItem(string line)
	{
		// Check for if its a fake one
		if (line == "")
		{
			Done = false;
			Text = "";
		}
		else
		{
			string[] sections = line.Split(';');
			Done = sections[0] == "1";
			Text = sections[1].Trim();
		}
	}
}