using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Resources;

namespace RevitAPIITOGZAD
{
    public sealed class ExternalCommandAvailability : IExternalCommandAvailability
    {
        bool IExternalCommandAvailability.IsCommandAvailable(
          UIApplication applicationData,
          CategorySet selectedCategories)
        {
            ResourceManager resourceManager1 = new ResourceManager(this.GetType());
            ResourceManager resourceManager2 = new ResourceManager(typeof(Resources));
            bool flag = false;
            try
            {
                if (applicationData.ActiveUIDocument != null)
                    flag = true;
            }
            catch (Exception ex)
            {
                TaskDialog.Show(resourceManager2.GetString("_Error"), ex.Message);
                flag = false;
            }
            finally
            {
                resourceManager1.ReleaseAllResources();
                resourceManager2.ReleaseAllResources();
            }
            return flag;
        }
    }
}

