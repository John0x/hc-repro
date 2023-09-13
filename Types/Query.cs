using System.Security.Claims;
using HotChocolate.Authorization;

namespace Api.Types;

public class Foo
{
	public int Id { get; set; }
	public string Name { get; set; }
	public DateTime Date { get; set; }
}

// create list with 30 sample Foo objects

[QueryType]
public class Query
{
	[UsePaging(MaxPageSize = 5, IncludeTotalCount = true, DefaultPageSize = 5)]
	[UseProjection]
	[UseSorting]
	public IQueryable<Foo> GetFoos()
	{
		var foos = Enumerable.Range(1, 30).Select(i => new Foo { Id = i, Name = $"Foo {i}", Date = DateTime.Now }).ToList();

		return foos.AsQueryable();
	}
}
