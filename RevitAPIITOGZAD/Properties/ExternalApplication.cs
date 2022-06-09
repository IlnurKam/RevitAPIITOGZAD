using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using System.Reflection;
using System.Resources;

namespace RevitAPIITOGZAD
{
    public sealed class ExternalApplication : IExternalApplication
    {
        Result IExternalApplication.OnStartup(UIControlledApplication uic_app)
        {
            ResourceManager resourceManager1 = new ResourceManager(this.GetType());
            ResourceManager resourceManager2 = new ResourceManager(typeof(Resources));
            Result result = (Result)0;
            try
            {
                this.Initialize(uic_app);
            }
            catch (Exception ex)
            {
                TaskDialog.Show(resourceManager2.GetString("_Error"), ex.Message);
                result = (Result)1;
            }
            finally
            {
                resourceManager1.ReleaseAllResources();
                resourceManager2.ReleaseAllResources();
            }
            return result;
        }

        private void Initialize(UIControlledApplication uic_app)
        {
            RevitPatches.PatchCultures(uic_app.ControlledApplication.Language);
            UIBuilder.BuildUI(uic_app, Assembly.GetExecutingAssembly(), typeof(Resources));
        }

        Result IExternalApplication.OnShutdown(UIControlledApplication uic_app)
        {
            ResourceManager resourceManager1 = new ResourceManager(this.GetType());
            ResourceManager resourceManager2 = new ResourceManager(typeof(Resources));
            Result result = (Result)0;
            try
            {
            }
            finally
            {
                resourceManager1.ReleaseAllResources();
                resourceManager2.ReleaseAllResources();
            }
            return result;
        }
    }
}