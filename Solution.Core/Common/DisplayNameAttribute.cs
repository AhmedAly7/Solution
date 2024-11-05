namespace Solution.Core.Common;

public sealed class DisplayNameAttribute : Attribute
{
	/// <summary>
	/// Standard <see cref="DisplayNameAttribute"/> constructor.
	/// </summary>
	/// <param name="resourceName">Name of the resource string for enum member.</param>
	public DisplayNameAttribute(Type resourceType, string resourceName)
	{
		ResourceType = resourceType;
		ResourceName = resourceName;
	}

	/// <summary>
	/// Name of the resource string for enum member.
	/// </summary>
	public string ResourceName
	{
		get;
		private set;
	}

	public Type ResourceType
	{
		get;
		private set;
	}
}