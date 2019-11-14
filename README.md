# ElementAdjuster

Revit C# .NET add-in to apply slight adjustment to `Element` instance location.

The goal is to execute minor changes in the model, export and import element changes.

Two copies of an identical model exist.

Slight adjustments are mode to the locations of certain elements in one of them.

These adjustments are to be duplicated in the second copy of the model.

A JSON file storing specific data for selected elements is generated in the modified model.

We pick an element and click "export info". 
That creates a new JSON file `C:/tmp/exported_element_info.json` containing the following info about each selected element:

- id
- unique id
- x, y, z coordinates
- direction facing
- element type
- host element

The JSON file is human readable and can also be manually updated.

The same add-in implements a command "import info" that retrieves the element and updates its position accordingly.

Simplifications: currently, there is no need to support changes to "direction facing", "element type", "host element" (or id/uid obviously).
These fields need to be exported.

Changes to linked models should also be supported.

Re. linked model support:
I assume the modified element lives in the main project, not in a linked model.
If it lives in a linked model, the changes should be exported from there and applied there as well.

Currently, ElementAdjuster only supports adjustment of elements that have a valid `Location` with a `LocationPoint` value.

ElementAdjuster implementes two external commands:

- [CmdExport](#CmdExport)
- [CmdImport](#CmdImport)

## <a name="CmdExport"></a> CmdExport

CmdExport exports the adjustment data listed above for a set of elements to the hard-coded JSON file path specified above.

They can be pre-selected before launching the command.

If no elements have been preselected, the command prompts the user to select them.

Elements that have no valid `LocationPoint` are ignored.

## <a name="CmdImport"></a> CmdImport

CmdImport imports and applies the adjustment data for a set of elements from the hard-coded JSON file path specified above and reports the number of adjusted elements.


## Author

Jeremy Tammik, [The Building Coder](http://thebuildingcoder.typepad.com), [ADN](http://www.autodesk.com/adn) [Open](http://www.autodesk.com/adnopen), [Autodesk Inc.](http://www.autodesk.com)


## License

This sample is licensed under the terms of the [MIT License](http://opensource.org/licenses/MIT).
Please see the [LICENSE](LICENSE) file for full details.

