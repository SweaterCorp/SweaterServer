using System.Collections.Generic;
using CommonLibraries.Infrastructures;

namespace CommonLibraries.CommonTypes
{
  public class PhotoSizeType : CustomEnum
  {
    public static PhotoSizeType None { get; } = new PhotoSizeType(0, "None");
    public static PhotoSizeType Original { get; } = new PhotoSizeType(1, "Original");
    public static PhotoSizeType Small { get; } = new PhotoSizeType(2, "Small");

    public PhotoSizeType(int id, string name) : base(id, name)
    {
    }

    public static List<PhotoSizeType> AsList()
    {
      return new List<PhotoSizeType> {None, Original, Small};
    }

    public static explicit operator PhotoSizeType(int id)
    {
      return AsList().Find(x => x.Id == id);
    }
  }
}