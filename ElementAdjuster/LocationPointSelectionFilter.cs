using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

namespace ElementAdjuster
{
  class LocationPointSelectionFilter : ISelectionFilter
  {
    static public bool HasValidLocationPoint( Element e )
    {
      Location loc = e.Location;
      return null != loc
        && loc is LocationPoint;
    }

    public bool AllowElement( Element e )
    {
      return HasValidLocationPoint( e );
    }

    public bool AllowReference( Reference r, XYZ p )
    {
      return true;
    }
  }
}
