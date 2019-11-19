
using Autodesk.Revit.DB;
using System;

namespace ElementAdjuster
{
  class ElementAdjustmentData
  {
    public int Id;
    public string UniqueId;
    public int X;
    public int Y;
    public int Z;
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
      X = Util.FootToMmInt( p.X );
      Y = Util.FootToMmInt( p.Y );
      Z = Util.FootToMmInt( p.Z );
    }

    /// <summary>
    /// Return a point in the Revit database unit, 
    /// imperial feet.
    /// </summary>
    public XYZ Point
    {
      get
      {
        return new XYZ( 
          Util.MmToFeet( X ), 
          Util.MmToFeet( Y ), 
          Util.MmToFeet( Z ) );
      }
    }
  }
}
