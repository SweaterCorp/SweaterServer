using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommonLibraries.Infrastructures
{
  public class Enumeration : IComparable, IEquatable<Enumeration>
  {
    public string Value { get; private set; }
    public int Id { get; private set; }

    protected Enumeration()
    {
    }

    protected Enumeration(int id, string name)
    {
      Id = id;
      Value = name;
    }

    public int CompareTo(object other)
    {
      return Id.CompareTo(((Enumeration) other).Id);
    }

    public bool Equals(Enumeration other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return Id == other.Id;
    }

    public override string ToString()
    {
      return Value;
    }

    public static IEnumerable<T> GetAll<T>() where T : Enumeration
    {
      var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
      return fields.Select(field => field.GetValue(null)).Cast<T>();
    }

    public override bool Equals(object obj)
    {
      if (!(obj is Enumeration otherValue)) return false;

      var typeMatches = GetType() == obj.GetType();
      var valueMatches = Id == otherValue.Id;
      return typeMatches && valueMatches;
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return ((Value != null ? Value.GetHashCode() : 0) * 397) ^ Id;
      }
    }

    public static bool operator ==(Enumeration left, Enumeration right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(Enumeration left, Enumeration right)
    {
      return !Equals(left, right);
    }
  }
}