struct TodoList
{
	public string Name { get; set; }
	public List<Item> Items { get; set; }
	public int Length
	{
		get { return Items.Count + 3; }
		private set { }
	}

	public TodoList(string name)
	{
		// Assign variables
		Name = name;
		Items = new List<Item>();
	}
}

struct Item
{
	public string Value { get; set; }
	public bool Done { get; set; }

	public Item(string value, bool done)
	{
		// Assign values
		Value = value;
		Done = done;
	}
}