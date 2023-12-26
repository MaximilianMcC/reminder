class Handler
{
	private static TodoList TodoList;
	private static int index = 0; // TODO: Set to 0
	private static int Top;

	// TODO: Don't calculate whitespace, use cursor positions
	public static void Frame(TodoList todoList)
	{
		TodoList = todoList;
		Top = Console.CursorTop;

		// Draw the list
		PrintList();

		// Get input
		while (true)
		{
			// Set the cursors position just before the checkbox contents
			//? +5 is for the title heading thing
			//? +2 is to account for the space between the rows
			Console.SetCursorPosition(2, (Top + (2 * index) + 5));



			// Get input from the console
			ConsoleKeyInfo input = Console.ReadKey(true);

			// Check for if they want the index to
			// go upwards, or downwards
			if (input.Key == ConsoleKey.UpArrow) index--;
			if (input.Key == ConsoleKey.DownArrow) index++;

			// Check for if the index is outside of the bounds
			if (index > todoList.Length - 1) index = 0;
			if (index < 0) index = todoList.Length - 1;
		}
	}

	private static void PrintList()
	{
		// Get measurements
		int consoleWidth = Console.WindowWidth - 1;
		//? x / 10 gives us 8-% of the screen
		//? x - 3 makes room for the checkbox
		//? x - 2 allows room for the edges
		int listWidth = consoleWidth - (((consoleWidth / 10) * 2) - 3) - 2;

		// Draw the title and a line under it
		Console.ForegroundColor = ConsoleColor.Blue;
		Console.WriteLine($"\n    {TodoList.Name}");
		Console.ResetColor();
		Console.WriteLine(new string('═', consoleWidth));

		// Draw the top of the actual list
		Console.WriteLine();
		Console.WriteLine("╓───╥" + new string('─', listWidth) + '╖');

		// Draw all of the list items, plus 3 additional so
		// that the user understands that they can be filled
		for (int i = 0; i < (TodoList.Length); i++)
		{
			string value = "";
			bool done = false;

			// Check for if we are still using the real items
			if (i < TodoList.Items.Count)
			{
				// Apply values
				value = TodoList.Items[i].Value;
				done = TodoList.Items[i].Done;
			}

			// Draw the current item
			// TODO: Change the checked symbols color to green/red
			// TODO: Add multiple lines for if the value is super long
			char checkedSymbol = done ? 'x' : ' ';
			int whitespace = listWidth - 1 - value.Length;
			Console.WriteLine($"║ {checkedSymbol} ║ {value}{new string(' ', whitespace)}║");

			// Draw the bottom line
			if (i == TodoList.Length - 1)
			{
				// Draw the end line
				Console.WriteLine("╙───╨" + new string('─', listWidth) + '╜');
			}
			else
			{
				// Draw a standard line
				Console.WriteLine("╟───╫" + new string('─', listWidth) + '╢');
			}
		}
	}
}