#region Namespaces
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
  public class Command : IExternalCommand
  {
    /// <summary>
    /// JSON output file path
    /// </summary>
    const string _filepath
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
        sel.GetElementIds() );

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

      using( StreamWriter s = new StreamWriter( 
        _filepath, true ) )
      {
        foreach( ElementId id in ids )
        {
          Element e = doc.GetElement( id );

          // id
          // unique id
          // x, y, z coordinates
          // direction facing
          // element type
          // host element

          s.WriteLine(
          "{{\"name\":\"{0}\", \"id\":\"{1}\", "
          + "\"uid\":\"{2}\", \"svg_path\":\"{3}\"}}",
          e.Name, e.Id, e.UniqueId );
        }
        s.Close();
      }
      return Result.Succeeded;
    }
  }
}
