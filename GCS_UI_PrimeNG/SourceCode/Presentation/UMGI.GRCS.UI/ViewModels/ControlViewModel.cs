using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamicControlsCreation.ViewModels
{
    public abstract class ControlViewModel
    {
        public abstract string Type { get; }
        public string Label { get; set; }
        public string Name { get; set; }
    }

    public class LabelViewModel : ControlViewModel
    {
        public override string Type
        {
            get { return "Label"; }
        }
        public string Value { get; set; }
    }

    public class TextBoxViewModel : ControlViewModel
    {
        public override string Type
        {
            get { return "TextBox"; }
        }
        public string Value { get; set; }
    }

    public class TextAreaViewModel : ControlViewModel
    {
        public override string Type
        {
            get { return "TextArea"; }
        }
        public string Value { get; set; }
    }

    public class CheckBoxViewModel : ControlViewModel
    {
        public override string Type
        {
            get { return "CheckBox"; }
        }
        public bool Value { get; set; }
    }

    public class DropDownViewModel : TextBoxViewModel
    {
        public override string Type
        {
            get { return "DropDown"; }
        }
        public SelectList Values { get; set; }
    }
}