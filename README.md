# ElementAdjuster

Revit C# .NET add-in to apply slight adjustment to `Element` instance location.

The goal is to execute minor changes in the model, export and import element changes.

For this purpose, elements and their locations are stored in an external [JSON adjustment data file](#jsondata).

Two copies of an identical model exist.
Slight adjustments are made to the locations of certain elements in one of them.
These adjustments are to be duplicated in the second copy of the model.

Alternatively, with one single BIM, the current element positions are exported to JSON, [manually edited](#manual) to tweak the element positions, and the modified adjustment file read back in to apply these modifications to the BIM.

The same add-in implements a command "import info" that retrieves the element and updates its position accordingly.

Currently, ElementAdjuster only supports adjustment of elements that have a valid `Location` with a `LocationPoint` value.

Changes to linked models should also be supported.

Re. linked model support:
I assume the modified element lives in the main project, not in a linked model.
If it lives in a linked model, the changes should be exported from there and applied there as well.

ElementAdjuster implements two external commands:

- [CmdExport](#CmdExport)
- [CmdImport](#CmdImport)

![Fixture adjustment](img/fixture_adjustment.png "Fixture adjustment")

## <a name="jsondata"></a>JSON Adjustment Data File

A JSON file storing specific data for selected elements is generated in the modified model.

Pick an element and click "export info". 
That creates a new or appends to an existing JSON file `C:/tmp/exported_element_info.json` containing the following info about each selected element:

- id
- unique id
- x, y, z coordinates
- direction facing
- element type
- host element

The JSON file is human readable and can easily be manually edited and updated.

Simplification: currently, there is no need to support changes to "direction facing", "element type", "host element" (or id/uid obviously).
These fields need to be exported.


## <a name="manual"></a>Manual Adjustment by Editing the JSON File

Another possible use case that may be much more interesting than modifying a copy of the model:

Generate the JSON adjustment file from the BIM and edit the stored values manually by hand to automatically fix element positions using human-defined changes.


## <a name="CmdExport"></a>CmdExport

CmdExport exports the adjustment data listed above for a set of elements to the hard-coded JSON file path specified above.

They can be pre-selected before launching the command.

If no elements have been preselected, the command prompts the user to select them.

Elements that have no valid `LocationPoint` are ignored.

Note that the output file is always appended to.
In order to remove previously stored adjustments, edit or delete the pre-existing file.


## <a name="CmdImport"></a>CmdImport

CmdImport imports and applies the adjustment data for a set of elements from the hard-coded JSON file path specified above and reports the number of adjusted elements.


## Author

Jeremy Tammik, [The Building Coder](http://thebuildingcoder.typepad.com), [ADN](http://www.autodesk.com/adn) [Open](http://www.autodesk.com/adnopen), [Autodesk Inc.](http://www.autodesk.com)


## License

This sample is licensed under the terms of the [MIT License](http://opensource.org/licenses/MIT).
Please see the [LICENSE](LICENSE) file for full details.

