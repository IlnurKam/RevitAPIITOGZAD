using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.Exceptions;
using Autodesk.Revit.UI;
using System.Resources;
using Prism.Services.Dialogs;
using System.Windows.Forms;
using DialogResult = System.Windows.Forms.DialogResult;
using Application = Autodesk.Revit.ApplicationServices.Application;

namespace RevitAPIITOGZAD
{
    [Transaction(TransactionMode.Manual)]
    public sealed class RoomNumerator : IExternalCommand
    {
        Result IExternalCommand.Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            ResourceManager resourceManager1 = new ResourceManager(this.GetType());
            ResourceManager resourceManager2 = new ResourceManager(typeof(Resources));
            Result result = (Result)1;
            try
            {
                UIApplication application1 = commandData?.Application;
                UIDocument activeUiDocument = application1?.ActiveUIDocument;
                if (application1 != null)
                {
                    Application application2 = application1.Application;
                }
                using (TransactionGroup transactionGroup = new TransactionGroup(activeUiDocument?.Document, UIBuilder.GetResourceString(this.GetType(), typeof(Resources), "_transaction_group_name")))
                {
                    if (transactionGroup.Start())
                    {
                        if (this.DoWork(commandData, ref message, elements))
                        {
                            if (transactionGroup.Assimilate())
                            {
                                result = (Result)1;
                            }
                        }
                        else
                            transactionGroup.RollBack();
                    }
                }
            }
            catch (System.OperationCanceledException ex)
            {
                result = (Result)1;
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

        private bool DoWork(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            if (commandData == null)
                throw new System.ArgumentNullException(nameof(commandData));
            if (message == null)
                throw new System.ArgumentNullException(nameof(message));
            if (elements == null)
                throw new System.ArgumentNullException(nameof(elements));
            ResourceManager resourceManager1 = new ResourceManager(this.GetType());
            ResourceManager resourceManager2 = new ResourceManager(typeof(Resources));
            UIApplication application = commandData.Application;
            Document document = application?.ActiveUIDocument?.Document;
            string str = resourceManager1.GetString("_transaction_name");
            try
            {
                using (Transaction transaction = new Transaction(document, str))
                {
                    if (transaction.Start())
                    {
                        RoomNum data = new RoomNum(application, document);
                        RoomNumeratorForm roomNumeratorForm = new RoomNumeratorForm(data, control);
                        DialogResult dialogResult = roomNumeratorForm.ShowDialog();
                        if (dialogResult == DialogResult.OK)
                            data.RenumRooms(document);
                        while (dialogResult == DialogResult.Retry)
                        {
                            try
                            {
                                data.SelectRooms(application, document);
                            }
                            catch (System.OperationCanceledException ex)
                            {
                                throw;
                            }
                            finally
                            {
                                dialogResult = roomNumeratorForm.ShowDialog();
                                if (dialogResult == DialogResult.OK)
                                    data.RenumRooms(document);
                            }
                        }
                        //return;
                        transaction.Commit();
                        //return Result.Succeeded;
                        //transaction.Commit();
                        
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                resourceManager1.ReleaseAllResources();
                resourceManager2.ReleaseAllResources();
            }
            return false;
        }
    }
}

