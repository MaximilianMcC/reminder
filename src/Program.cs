using System.Diagnostics;

class Program
{
	public static void Main(string[] args)
	{
		
		// Check for if there is already an existing td file in the current directory.
		// If there isn't one then ask them if they wanna make a new one.
		if (Directory.GetFiles("./", "*.td").Length == 0)
		{
			// Say that there isn't a file
			Console.WriteLine("There isn't a todo file (*.td) in the current directory.\nCreate one? (y/n)");

			// Get input
			while (true)
			{
				ConsoleKeyInfo input = Console.ReadKey(true);
				if (input.Key == ConsoleKey.Y)
				{
					// Make a new td file
					File.Create("./todo.td");
					Console.WriteLine("Created 'todo.td' in the current directory.");
					return;
				}
				else if (input.Key == ConsoleKey.N) return;
			}
		}

		// Load the file, then display the list
		TodoList todoList = new TodoList(Directory.GetFiles("./", "*.td")[0]);
		while (true)
		{
			todoList.DrawList();
			todoList.ModifyList();
			todoList.SyncList();
		}
	}
}