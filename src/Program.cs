class Program
{
	private bool changesMade = false;

	public static void Main(string[] args)
	{
		// Check for if there is already a todo list
		// present in the current directory
		if (Directory.GetFiles("./", "*.td").Length == 0)
		{
			// Create a new TODO file
			// TODO: Only do this if the list is modified
			File.Create("./todo.td");
		}
		
		TodoList todoList = new TodoList("Test Hello World among us trust 123!!");
		todoList.Items.Add(new Item("Hello, world!", true));
		todoList.Items.Add(new Item("Lorem Ipsum si dolor amiet I fogeoten the rest!!", true));
		todoList.Items.Add(new Item("Become king of englan", false));

		// TODO: Rename class
		while (true)
		{
			Handler.Frame(todoList);
		}



		//! debug. remove for production/actual terminal use
		Console.WriteLine("Press any key to continue...");
		Console.ReadKey(true);
	}
}