using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

namespace ElementAdjuster
{
  class LocationPointSelectionFilter : ISelectionFilter
  {
    public bool AllowElement( Element e )
    {
      Location loc = e.Location;
      return null != loc
        && loc is LocationPoint;
    }

    public bool AllowReference( Reference r, XYZ p )
    {
      return true;
    }
  }
}
