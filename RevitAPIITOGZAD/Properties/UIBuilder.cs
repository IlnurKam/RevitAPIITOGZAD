using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Windows.UI.Xaml.Media.Imaging;

namespace RevitAPIITOGZAD
{
    public static class UIBuilder
    {
        private static Dictionary<string, string> tabs_dict = new Dictionary<string, string>();

        public static void BuildUI(
          UIControlledApplication uic_app,
          Assembly asm,
          Type default_resources_type)
        {
            if (uic_app == null)
                throw new ArgumentNullException(nameof(uic_app));
            if (asm == (Assembly)null)
                throw new ArgumentNullException(nameof(asm));
            if (default_resources_type == (Type)null)
                throw new ArgumentNullException(nameof(default_resources_type));
            string cmd_interface_name = typeof(IExternalCommand).FullName;
            foreach (Type cmd_type in ((IEnumerable<Type>)asm.GetTypes()).Where<Type>((Func<Type, bool>)(n => n.GetInterface(cmd_interface_name) != (Type)null)))
            {
                bool result = false;
                if (bool.TryParse(UIBuilder.GetResourceString(cmd_type, default_resources_type, "_auto_location"), out result) & result)
                    UIBuilder.AddButton(uic_app, cmd_type, default_resources_type);
            }
        }

        public static void AddButton(
          UIControlledApplication uic_app,
          Type cmd_type,
          Type default_resources_type)
        {
            if (uic_app == null)
                throw new ArgumentNullException(nameof(uic_app));
            if (cmd_type == (Type)null)
                throw new ArgumentNullException(nameof(cmd_type));
            if (cmd_type.GetInterface(typeof(IExternalCommand).FullName) == (Type)null)
                throw new ArgumentException(nameof(cmd_type));
            string key = !(default_resources_type == (Type)null) ? UIBuilder.GetResourceString(cmd_type, default_resources_type, "_Ribbon_tab_key") : throw new ArgumentNullException(nameof(default_resources_type));
            UIBuilder.GetResourceString(cmd_type, default_resources_type, key);
            string resourceString1 = UIBuilder.GetResourceString(cmd_type, default_resources_type, "_Ribbon_panel_key");
            string panel_name = UIBuilder.GetResourceString(cmd_type, default_resources_type, resourceString1);
            RibbonPanel ribbonPanel = ((IEnumerable<RibbonPanel>)uic_app.GetRibbonPanels()).FirstOrDefault<RibbonPanel>((Func<RibbonPanel, bool>)(n => n.Name.Equals(panel_name, StringComparison.InvariantCulture))) ?? uic_app.CreateRibbonPanel(panel_name);
            string location = cmd_type.Assembly.Location;
            ContextualHelp contextualHelp = new ContextualHelp((ContextualHelpType)2, UIBuilder.GetResourceString(cmd_type, default_resources_type, "_Help_file_name"));
            contextualHelp.HelpTopicUrl = UIBuilder.GetResourceString(cmd_type, default_resources_type, "_Help_topic_Id");
            string name = cmd_type.Name;
            string resourceString2 = UIBuilder.GetResourceString(cmd_type, default_resources_type, "_Button_caption");
            string resourceString3 = UIBuilder.GetResourceString(cmd_type, default_resources_type, "_Button_tooltip_text");
            string resourceString4 = UIBuilder.GetResourceString(cmd_type, default_resources_type, "_Button_long_description");
            string str1 = resourceString2;
            string str2 = location;
            string fullName = cmd_type.FullName;
            PushButtonData pushButtonData = new PushButtonData(name, str1, str2, fullName);
            string resourceString5 = UIBuilder.GetResourceString(cmd_type, default_resources_type, "_aviability_type");
            Type type = cmd_type.Assembly.GetType(resourceString5);
            if (type != (Type)null && type.GetInterface(typeof(IExternalCommandAvailability).FullName) != (Type)null)
                pushButtonData.AvailabilityClassName = type.FullName;
            PushButton pushButton = ribbonPanel.AddItem((RibbonItemData)pushButtonData) as PushButton;
            ((RibbonItem)pushButton).ToolTip = resourceString3;
            ((RibbonButton)pushButton).LargeImage = (ImageSource)GetResourceImage(cmd_type, default_resources_type, "_Button_image");
            ((RibbonItem)pushButton).ToolTipImage = (ImageSource)GetResourceImage(cmd_type, default_resources_type, "_Button_tooltip_image");
            ((RibbonItem)pushButton).LongDescription = resourceString4;
            ((RibbonItem)pushButton).SetContextualHelp(contextualHelp);
        }

        public static string GetResourceString(Type cmd_type, Type default_resources_type, string key)
        {
            if (cmd_type == (Type)null)
                throw new ArgumentNullException(nameof(cmd_type));
            if (default_resources_type == (Type)null)
                throw new ArgumentNullException(nameof(default_resources_type));
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException(nameof(cmd_type));
            ResourceManager resourceManager1 = new ResourceManager(cmd_type);
            ResourceManager resourceManager2 = new ResourceManager(default_resources_type);
            string resourceString = resourceManager1.GetString(key);
            if (string.IsNullOrEmpty(resourceString))
                resourceString = resourceManager2.GetString(key);
            resourceManager1.ReleaseAllResources();
            if (resourceManager2 != resourceManager1)
                resourceManager2.ReleaseAllResources();
            return resourceString;
        }

        public static System.Windows.Media.Imaging.BitmapSource GetResourceImage(
          Type cmd_type,
          Type default_resources_type,
          string key)
        {
            if (cmd_type == (Type)null)
                throw new ArgumentNullException(nameof(cmd_type));
            if (default_resources_type == (Type)null)
                throw new ArgumentNullException(nameof(default_resources_type));
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException(nameof(cmd_type));
            ResourceManager resourceManager1 = new ResourceManager(cmd_type);
            ResourceManager resourceManager2 = new ResourceManager(default_resources_type);
            if (!(resourceManager1.GetObject(key) is Bitmap bitmap))
                bitmap = resourceManager2.GetObject(key) as Bitmap;
            resourceManager1.ReleaseAllResources();
            if (resourceManager2 != resourceManager1)
                resourceManager2.ReleaseAllResources();
            return bitmap == null ? (System.Windows.Media.Imaging.BitmapSource)null : System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }
    }
}

