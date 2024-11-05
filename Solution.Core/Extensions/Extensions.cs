using System.Collections;
using System.Text;

namespace Solution.Core.Extensions;

public static class Extensions
{
	public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
	{
		foreach (T element in source)
			action(element);
	}
	public static string Flatten(this IEnumerable elems, string separator)
	{
		if (elems == null)
		{
			return null;
		}

		StringBuilder sb = new StringBuilder();
		foreach (object elem in elems)
		{
			if (sb.Length > 0)
			{
				sb.Append(separator);
			}

			sb.Append(elem);
		}

		return sb.ToString();
	}
	public static bool HasProperty(this object obj, string propertyName)
	{
		return obj.GetType().GetProperty(propertyName) != null;
	}
	public static byte[] GetImageBytes(string imageName)
	{
		string filaName = Path.Combine("/data/files", imageName);
		if (!System.IO.File.Exists(filaName))
			return null;
		return System.IO.File.ReadAllBytes(filaName);
	}

}