using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace RevitAPIITOGZAD
{
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [DebuggerNonUserCode]
    [CompilerGenerated]
    internal class Resources
    {
        private static ResourceManager resourceMan;
        private static CultureInfo resourceCulture;

        internal Resources()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (Resources.resourceMan == null)
                    Resources.resourceMan = new ResourceManager("Resources", typeof(Resources).Assembly);
                return Resources.resourceMan;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get => Resources.resourceCulture;
            set => Resources.resourceCulture = value;
        }

        internal static string _auto_location => Resources.ResourceManager.GetString(nameof(_auto_location), Resources.resourceCulture);

        internal static string _aviability_type => Resources.ResourceManager.GetString(nameof(_aviability_type), Resources.resourceCulture);

        internal static string _Button_caption => Resources.ResourceManager.GetString(nameof(_Button_caption), Resources.resourceCulture);

        internal static Bitmap _Button_image => (Bitmap)Resources.ResourceManager.GetObject(nameof(_Button_image), Resources.resourceCulture);

        internal static string _Button_long_description => Resources.ResourceManager.GetString(nameof(_Button_long_description), Resources.resourceCulture);

        internal static Bitmap _Button_tooltip_image => (Bitmap)Resources.ResourceManager.GetObject(nameof(_Button_tooltip_image), Resources.resourceCulture);

        internal static string _Button_tooltip_text => Resources.ResourceManager.GetString(nameof(_Button_tooltip_text), Resources.resourceCulture);

        internal static string _command_message => Resources.ResourceManager.GetString(nameof(_command_message), Resources.resourceCulture);

        internal static string _Error => Resources.ResourceManager.GetString(nameof(_Error), Resources.resourceCulture);

        internal static string _Help_file_name => Resources.ResourceManager.GetString(nameof(_Help_file_name), Resources.resourceCulture);

        internal static string _Help_topic_Id => Resources.ResourceManager.GetString(nameof(_Help_topic_Id), Resources.resourceCulture);

        internal static string _message => Resources.ResourceManager.GetString(nameof(_message), Resources.resourceCulture);

        internal static string _Ribbon_panel_name => Resources.ResourceManager.GetString(nameof(_Ribbon_panel_name), Resources.resourceCulture);

        internal static string _Ribbon_panel_name2 => Resources.ResourceManager.GetString(nameof(_Ribbon_panel_name2), Resources.resourceCulture);

        internal static string _Ribbon_tab_name => Resources.ResourceManager.GetString(nameof(_Ribbon_tab_name), Resources.resourceCulture);

        internal static string _transaction_group_name => Resources.ResourceManager.GetString(nameof(_transaction_group_name), Resources.resourceCulture);

        internal static Bitmap BTLR => (Bitmap)Resources.ResourceManager.GetObject(nameof(BTLR), Resources.resourceCulture);

        internal static Bitmap BTRL => (Bitmap)Resources.ResourceManager.GetObject(nameof(BTRL), Resources.resourceCulture);

        internal static Bitmap cancel => (Bitmap)Resources.ResourceManager.GetObject(nameof(cancel), Resources.resourceCulture);

        internal static Bitmap Help => (Bitmap)Resources.ResourceManager.GetObject(nameof(Help), Resources.resourceCulture);

        internal static Bitmap LRBT => (Bitmap)Resources.ResourceManager.GetObject(nameof(LRBT), Resources.resourceCulture);

        internal static Bitmap LRTB => (Bitmap)Resources.ResourceManager.GetObject(nameof(LRTB), Resources.resourceCulture);

        internal static Bitmap OK => (Bitmap)Resources.ResourceManager.GetObject(nameof(OK), Resources.resourceCulture);

        internal static Bitmap RLBT => (Bitmap)Resources.ResourceManager.GetObject(nameof(RLBT), Resources.resourceCulture);

        internal static Bitmap RLTB => (Bitmap)Resources.ResourceManager.GetObject(nameof(RLTB), Resources.resourceCulture);

        internal static Bitmap TBLR => (Bitmap)Resources.ResourceManager.GetObject(nameof(TBLR), Resources.resourceCulture);

        internal static Bitmap TBRL => (Bitmap)Resources.ResourceManager.GetObject(nameof(TBRL), Resources.resourceCulture);
    }
}
