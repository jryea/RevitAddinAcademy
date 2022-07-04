#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

#endregion

namespace RevitAddinAcademy
{
    [Transaction(TransactionMode.Manual)]
    public class Command01Challenge : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            
            double offset = 0.02;
            double offsetCalc = offset * doc.ActiveView.Scale;
            XYZ curPoint = new XYZ(0,0,0);
            XYZ offsetPoint = new XYZ(0, offsetCalc, 0);
            string text = "";

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(TextNoteType));    

            Transaction t = new Transaction (doc, "Create FizzBuzz Text");

            t.Start();

            for (int i = 1; i <= 100; i++)
            {
                if (i % 3 == 0 && i % 5 == 0)
                {
                    text = "FizzBuzz"; 
                }
                else if (i % 3 == 0)
                {
                    text = "Fizz";
                }
                else if (i % 5 == 0)
                {
                    text = "Buzz";
                }
                else
                {
                    text = i.ToString();
                }

                TextNote textNote = TextNote.Create(doc, doc.ActiveView.Id, curPoint, text, collector.FirstElementId());
                curPoint = curPoint - offsetPoint;
            }
            
            t.Commit();
            t.Dispose();    

            return Result.Succeeded;
        }
    }
}
