
using Autodesk.Revit.DB;
using System;

namespace ElementAdjuster
{
  class ElementAdjustmentData
  {
    public int Id;
    public string UniqueId;
    public double X;
    public double Y;
    public double Z;
    //public double FacingX;
    //public double FacingY;
    //public double FacingZ;
    //public string ElementType;
    //public int HostElementId;
    //public string HostElementUniqueId;

    public ElementAdjustmentData()
    {
      Id = -1;
    }

    public ElementAdjustmentData( Element e )
    {
      Location loc = e.Location;

      if( null == loc || !(loc is LocationPoint) )
      {
        throw new ArgumentException(
          "Expected valid element location point" );
      }
      Id = e.Id.IntegerValue;
      UniqueId = e.UniqueId;
      XYZ p = ((LocationPoint) loc).Point;
      X = p.X;
      Y = p.Y;
      Z = p.Z;
    }

    public XYZ Point
    {
      get { return new XYZ( X, Y, Z ); }
    }
  }
}
