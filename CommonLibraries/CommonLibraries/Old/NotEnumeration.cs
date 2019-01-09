using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommonLibraries.Old
{
  public class NotEnumeration : IComparable, IEquatable<NotEnumeration>
  {
    public string Key { get; }
    public int Id { get; }

    protected NotEnumeration()
    {
    }

    protected NotEnumeration(int id, string name)
    {
      Id = id;
      Key = name;
    }

    public int CompareTo(object other)
    {
      return Id.CompareTo(((NotEnumeration) other).Id);
    }

    public override string ToString()
    {
      return Key;
    }

    protected static IEnumerable<T> GetAll<T>() where T : NotEnumeration
    {
      var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
      return fields.Select(field => field.GetValue(null)).Cast<T>();
    }

    public override bool Equals(object obj)
    {
      if (!(obj is NotEnumeration otherValue)) return false;

      var typeMatches = GetType() == obj.GetType();
      var valueMatches = Id == otherValue.Id;
      return typeMatches && valueMatches;
    }

    public bool Equals(NotEnumeration other)
    {
      if (ReferenceEquals(null, other)) return false;
      return ReferenceEquals(this, other) || Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
      return Id;
    }

    public static bool operator ==(NotEnumeration left, NotEnumeration right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(NotEnumeration left, NotEnumeration right)
    {
      return !Equals(left, right);
    }

    protected static T FromString<T>(string name, IEnumerable<T> list) where T : NotEnumeration
    {
      return list.Single(r => string.Equals(r.Key, name, StringComparison.OrdinalIgnoreCase));
    }

    protected static T FromValue<T>(int id, IEnumerable<T> list) where T : NotEnumeration
    {
      return list.Single(r => r.Id == id);
    }

    protected static bool IsValid<T>(int id, IEnumerable<T> list) where T : NotEnumeration
    {
      return list.Any(r => r.Id == id);
    }
  }
}