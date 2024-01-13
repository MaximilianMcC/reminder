using System.Diagnostics;

class TodoList
{
	public List<TodoItem> Items;
	private string tdFilePath;
	private int startY;
	private bool editingText;
	private int index;

	public TodoList(string filePath)
	{
		// Open the td file and extract all values
		tdFilePath = filePath;
		string[] lines = File.ReadAllLines(filePath);

		// Loop through every item and add it to the list
		Items = new List<TodoItem>();
		foreach (string line in lines)
		{
			Items.Add(new TodoItem(line));
		}

		// Add a fake empty one at the end
		Items.Add(new TodoItem(""));

		// Save the start Y position for measuring
		startY = Console.CursorTop;
	}

	// Print the list
	public void DrawList()
	{
		// Move the cursor back to the start position so we
		// Don't redraw over stuff previously edited
		// TODO: Only redraw if needed.
		Console.SetCursorPosition(0, startY);

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
			string whitespace = new string(' ', listWidth - item.Text.Length - 1); //? -1 is for padding on left of text

			// Don't print the top if we're on the first item
			if (i != 0) Console.WriteLine($"{paddingLeft}├───╫" + new string('─', listWidth) + '│');

			// Print the item
			Console.WriteLine($"{paddingLeft}│ {done} ║ {item.Text}{whitespace}│");
		}

		// Draw the bottom section
		Console.WriteLine($"{paddingLeft}└───╨" + new string('─', listWidth) + '┘');
	}

	// Get input to modify the list in the console
	public void ModifyList()
	{
		// Get all of the needed measurements
		// TODO: Don't write twice
		int consoleWidth = Console.WindowWidth;
		int padding = consoleWidth / 10;

		// Get the position according to the Y and what we're editing
		int x = padding + 2;
		if (editingText) x += 3;
		int y = (startY + 1) + (index * 2);

		// Move the caret over if they are editing text
		if (editingText) x += 2;

		// Draw the cursor/caret
		// TODO: Recommended to use box caret. Maybe change background color otherwise.
		Console.SetCursorPosition(x, y);

		// Get console input to work with
		ConsoleKeyInfo input = Console.ReadKey(true);

		// Check for if they want to go upwards, or downwards
		if (input.Key == ConsoleKey.UpArrow) index--;
		else if (input.Key == ConsoleKey.DownArrow) index++;

		// Keep the index within the bounds
		if (index > Items.Count - 1) index = 0;
		if (index < 0) index = Items.Count - 1;

		// Check for if they want to toggle a todo thing
		if (input.Key == ConsoleKey.Enter && editingText == false)
		{
			// Get the current item, then switch its value
			Items[index].Done = !Items[index].Done;
		}

		// Check for if they want to switch between typing and not
		if (input.Key == ConsoleKey.Tab) editingText = !editingText;

		Debug.WriteLine(editingText);
	}

	// Update the td file, and md file if linked
	// TODO: Make a way to link md file
	// TODO: Only sync when changes have been made
	public void SyncList()
	{
		// Loop through every item and write it to the list
		List<string> list = Items.Select(item => item.Serialize()).ToList();

		// Update the list file
		File.WriteAllLines(tdFilePath, list);
		Debug.WriteLine("Updated file");

		// Check for if there is a markdown file, and find then
		// replace the todo list with the one from the td file
		// string mdFilePath = Directory.GetFiles("./", "*.md")[0];
		string mdFilePath = "./test.md";
		string[] mdContents = File.ReadAllLines(mdFilePath);

		// Loop through all lines and find the md list
		bool inList = false;
		int mdStart = 0;
		int mdEnd = 0;
		for (int i = 0; i < mdContents.Length; i++)
		{
			// Get the current line
			string line = mdContents[i];

			// Check for if we found the beginning of a md list
			if (line.StartsWith("- [ ]") || line.StartsWith("- [x]"))
			{
				// Say that we're in the list
				if (inList == false)
				{
					inList = true;
					mdStart = i;
				}

				// Increase the end
				mdEnd = i;
			}
		}
	}
}





class TodoItem
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

	public string Serialize()
	{
		char done = (Done) ? '1' : '0';
		return $"{done};{Text}";
	}
}