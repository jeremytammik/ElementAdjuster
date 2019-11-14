#region Namespaces
using System.IO;
using System.Web.Script.Serialization;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
#endregion

namespace ElementAdjuster
{
  [Transaction( TransactionMode.Manual )]
  public class CmdImport : IExternalCommand
  {
    /// <summary>
    /// Minimum distance to move an element in imperial feet
    /// </summary>
    const double _distance_threshold = 0.01;

    public Result Execute(
      ExternalCommandData commandData,
      ref string message,
      ElementSet elements )
    {
      UIApplication uiapp = commandData.Application;
      UIDocument uidoc = uiapp.ActiveUIDocument;
      Application app = uiapp.Application;
      Document doc = uidoc.Document;

      if( !File.Exists( CmdExport.FilePath ) )
      {
        message = CmdExport.FilePath + " not found.";
        return Result.Failed;
      }

      string[] lines = File.ReadAllLines( 
        CmdExport.FilePath );

      JavaScriptSerializer serializer
        = new JavaScriptSerializer();

      int n = 0;

      using( Transaction t = new Transaction( doc ) )
      {
        t.Start( "Adjusting element locations" );

        foreach(string line in lines )
        {
          ElementAdjustmentData d = serializer
            .Deserialize<ElementAdjustmentData>( 
              line );

          ElementId id = new ElementId( d.Id );
          Element e = doc.GetElement( id );
          LocationPoint lp = e.Location as LocationPoint;
          if( null != lp )
          {
            XYZ v = d.Point - lp.Point;
            if( _distance_threshold < v.GetLength() )
            {
              ElementTransformUtils.MoveElement( doc, id, v );
              ++n;
            }
          }
        }
        if( 0 < n )
        {
          t.Commit();
        }
      }
      return Result.Succeeded;
    }
  }
}
