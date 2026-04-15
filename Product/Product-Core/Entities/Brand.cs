using System;

namespace ProductCore.Entities;

public class Brand
{
	public Guid Id { get; private set; }

	/// <summary>The official manufacturer name (e.g., "Trek", "Apple").</summary>
	public string Name { get; private set; }

	/// <summary>Detailed history or focus of the brand. Useful for AI when a user asks for "a reliable brand known for durability."</summary>
	public string Description { get; private set; }

	public Brand() { }
	public Brand(string name, string description)
	{
		Name = name;
		Description = description;
	}
}
