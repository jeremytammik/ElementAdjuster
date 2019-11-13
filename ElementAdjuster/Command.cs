#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
#endregion

namespace ElementAdjuster
{
  [Transaction( TransactionMode.ReadOnly )]
  public class Command : IExternalCommand
  {
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
            "Please select adjusted elements to export" );

          ids = new List<ElementId>(
            refs.Select<Reference, ElementId>(
              r => r.ElementId ) );
        }
        catch(Autodesk.Revit.Exceptions.OperationCanceledException)
        {
          return Result.Cancelled;
        }
      }

      foreach(ElementId id in ids)
      {

      }
      return Result.Succeeded;
    }
  }
}
