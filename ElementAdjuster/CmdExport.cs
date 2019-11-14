#region Namespaces
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using PickObjectsCanceled = Autodesk.Revit.Exceptions.OperationCanceledException;
#endregion

namespace ElementAdjuster
{
  [Transaction( TransactionMode.ReadOnly )]
  public class CmdExport : IExternalCommand
  {
    /// <summary>
    /// JSON output file path
    /// </summary>
    public const string FilePath
      = "C:/tmp/exported_element_info.json";

    public Result Execute(
      ExternalCommandData commandData,
      ref string message,
      ElementSet elements )
    {
      UIApplication uiapp = commandData.Application;
      UIDocument uidoc = uiapp.ActiveUIDocument;
      Application app = uiapp.Application;
      Document doc = uidoc.Document;
      Selection sel = uidoc.Selection;

      List<ElementId> ids = new List<ElementId>(
        sel.GetElementIds().Where<ElementId>( 
          id => HasValidLocationPoint( id ) );

      if( 0 == ids.Count )
      {
        try
        {
          IList<Reference> refs = sel.PickObjects(
            ObjectType.Element,
            new LocationPointSelectionFilter(),
            "Please select adjusted elements to export" );

          ids = new List<ElementId>(
            refs.Select<Reference, ElementId>(
              r => r.ElementId ) );
        }
        catch( PickObjectsCanceled )
        {
          return Result.Cancelled;
        }
      }

      JavaScriptSerializer serializer
        = new JavaScriptSerializer();

      //Dictionary<int, ElementAdjustmentData> d
      //  = new Dictionary<int, ElementAdjustmentData>(
      //    ids.Count );
      //
      // Maybe previous data already exists?
      // If so, read it it first.
      // No, use append instead, and write individual 
      // dictionary records instead of the whole thing.
      //
      //if( File.Exists( _filepath ) )
      //{
      //  d = serializer
      //    .Deserialize<Dictionary<int, ElementAdjustmentData>>(
      //      File.ReadAllText( _filepath ) );
      //}

      int n = ids.Count;

      List<string> lines = new List<string>( n );

      foreach( ElementId id in ids )
      {
        int i = id.IntegerValue;

        ElementAdjustmentData data
          = new ElementAdjustmentData(
            doc.GetElement( id ) );

        //if( d.ContainsKey( i ) )
        //{
        //  d[ i ] = data;
        //}
        //else
        //{
        //  d.Add( id.IntegerValue, data );
        //}

        lines.Add( serializer.Serialize( data ) );
      }

      //File.WriteAllText( _filepath,
      //  serializer.Serialize( d ) );

      File.AppendAllLines( FilePath, lines );

      return Result.Succeeded;
    }
  }
}
