class Handler
{
	// TODO: Don't calculate whitespace, use cursor positions
	public static void Frame(TodoList todoList)
	{
		// Get measurements
		int consoleWidth = Console.WindowWidth - 1;
		//? x / 10 gives us 8-% of the screen
		//? x - 3 makes room for the checkbox
		//? x - 2 allows room for the edges
		int listWidth = consoleWidth - (((consoleWidth / 10) * 2) - 3) - 2;

		// Draw the title and a line under it
		Console.ForegroundColor = ConsoleColor.Blue;
		Console.WriteLine($"\n    {todoList.Name}");
		Console.ResetColor();
		Console.WriteLine(new string('═', consoleWidth));

		// Draw the top of the actual list
		Console.WriteLine();
		Console.WriteLine("╓───╥" + new string('─', listWidth) + '╖');

		// Draw all of the list items, plus 3 additional so
		// that the user understands that they can be filled
		for (int i = 0; i < (todoList.Items.Count + 3); i++)
		{
			string value = "";
			bool done = false;

			// Check for if we are still using the real items
			if (i < todoList.Items.Count)
			{
				// Apply values
				value = todoList.Items[i].Value;
				done = todoList.Items[i].Done;
			}

			// Draw the current item
			// TODO: Change the checked symbols color to green/red
			char checkedSymbol = done ? 'x' : ' ';
			int whitespace = listWidth - 1 - value.Length;
			Console.WriteLine($"║ {checkedSymbol} ║ {value}{new string(' ', whitespace)}║");

			// Draw the bottom line
			if (i == (todoList.Items.Count + 3) - 1)
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

		//! debug
		Console.ReadLine();
	}
}

/*
┌─┬┐  ╔═╦╗  ╓─╥╖  ╒═╤╕
│ ││  ║ ║║  ║ ║║  │ ││
├─┼┤  ╠═╬╣  ╟─╫╢  ╞═╪╡
└─┴┘  ╚═╩╝  ╙─╨╜  ╘═╧╛
┌───────────────────┐
│  ╔═══╗ Some Text  │▒
│  ╚═╦═╝ in the box │▒
╞═╤══╩══╤═══════════╡▒
│ ├──┬──┤           │▒
│ └──┴──┘           │▒
└───────────────────┘▒
 ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
*/