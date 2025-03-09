using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Prism.Commands;
using RevitAPITrainingLibrary;
//using RevitAPITrainingViewsSchedules.Wrappers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Module_5_7_1
{
    public class MainViewViewModel
    {
        private ExternalCommandData _commandData;
        private Document _doc;
        public List<FamilySymbol> TitleBlocks { get; }
        public FamilySymbol SelectedTitleBlock { get; set; }
        public int SheetsCount { get; set; }
        public string DesignedBy { get; set; }
        public List<ViewWrapper> Views { get; }
        
        public DelegateCommand CreateSheetsCommand { get; }


        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commandData = commandData;
            _doc = _commandData.Application.ActiveUIDocument.Document;
            TitleBlocks = FamilySymbolUtils.GetFamilySymbolsTitleBlock(commandData);
            SheetsCount = 0;
            DesignedBy = null;
            CreateSheetsCommand = new DelegateCommand(OnCreateSheetsCommand);
            Views =SelectionUtils.GetElementsByType<View>(_doc).Select(s=> new ViewWrapper(s)).ToList();
        }

        private void OnCreateSheetsCommand()
        {

            List<ViewWrapper> selectedViewWrapper = Views.Where(s => s.IsSelected).ToList();
            List<View> selectedView = selectedViewWrapper.Select(s=>  s.Views).ToList();

            if (selectedView == null)
                return;

            SheetUtils.CreateSheets(_commandData, SelectedTitleBlock, SheetsCount, selectedView, DesignedBy);
            RaiseCloseRequest();
        }

        public event EventHandler CloseRequest;
        private void RaiseCloseRequest()
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }
    }
}
