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
	}

	public void DrawList()
	{
		foreach (TodoItem item in Items)
		{
			Console.WriteLine($"{item.Done}\t | {item.Text}");
		}
	}
}





struct TodoItem
{
	public bool Done { get; set; }
	public string Text { get; set; }

	// Parse the line from the td file
	public TodoItem(string line)
	{
		string[] sections = line.Split(';');
		Done = sections[0] == "1";
		Text = sections[1].Trim();
	}
}