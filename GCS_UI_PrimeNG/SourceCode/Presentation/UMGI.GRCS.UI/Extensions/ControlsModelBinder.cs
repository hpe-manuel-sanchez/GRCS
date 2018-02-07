using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DynamicControlsCreation.ViewModels;

namespace DynamicControlsCreation.Classes
{
    public class ControlsModelBinder : DefaultModelBinder
    {
        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            var type = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + ".Type");
            object model = Activator.CreateInstance("UMGI.GRCS.UI", "DynamicControlsCreation.ViewModels." + type.AttemptedValue + "ViewModel").Unwrap();
            bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, model.GetType());
            return model;
        }
    }
}