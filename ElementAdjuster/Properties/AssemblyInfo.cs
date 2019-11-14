using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle( "ElementAdjuster" )]
[assembly: AssemblyDescription( "Revit C# .NET add-in to apply slight adjustment to `Element` instance location" )]
[assembly: AssemblyConfiguration( "" )]
[assembly: AssemblyCompany( "Jeremy Tammik, The Building Coder" )]
[assembly: AssemblyProduct( "ElementAdjuster Revit C# .NET Add-In" )]
[assembly: AssemblyCopyright( "Copyright 2019 (C) Jeremy Tammik" )]
[assembly: AssemblyTrademark( "" )]
[assembly: AssemblyCulture( "" )]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible( false )]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid( "321044f7-b0b2-4b1c-af18-e71a19252be0" )]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
//
// History:
//
// 2019-11-12 conception
// 2019-11-13 2020.0.0.0 initial skeleton app
// 2019-11-13 2020.0.0.0 implemented element selection
// 2019-11-13 2020.0.0.0 implemented stream writer output
// 2019-11-13 2020.0.0.0 implemented LocationPointSelectionFilter
// 2019-11-14 2020.0.0.0 implemented ElementAdjustmentData
// 2019-11-14 2020.0.0.0 implemented ElementAdjustmentData constructor
// 2019-11-14 2020.0.0.0 implemented JSON serialization (and de-)
// 2019-11-14 2020.0.0.0 export individual line entries to json, not entire dictionary
// 2019-11-14 2020.0.0.0 successful export test
// 2019-11-14 2020.0.0.1 implemented CmdImport
//
[assembly: AssemblyVersion( "2020.0.0.1" )]
[assembly: AssemblyFileVersion( "2020.0.0.1" )]

