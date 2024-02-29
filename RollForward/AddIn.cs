using BlueByte.SOLIDWORKS.PDMProfessional.SDK;
using BlueByte.SOLIDWORKS.PDMProfessional.SDK.Attributes;
using BlueByte.SOLIDWORKS.PDMProfessional.SDK.Diagnostics;
using BlueByte.SOLIDWORKS.PDMProfessional.SDK.Enums;
using BlueByte.SOLIDWORKS.PDMProfessional.Services;
using EPDM.Interop.epdm;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace RollForward
{
    public enum Commands
    {
        RollForward = 100440
    }

    [Menu((int)Commands.RollForward, "Roll forward...", 43)]
    [Name("Roll forward")]
    [Description("Checks out file, does a get on an old version and checks it back into the vault")]
    [CompanyName("Blue Byte Systems Inc.")]
    [AddInVersion(false, 1)]
    [RequiredVersion(Year_e.PDM2018,  ServicePack_e.SP0)]
    [ComVisible(true)]
    [IsTask(false)]
    [Guid("fa7ed4fa-a5e3-4f65-bc4f-d709d82d2749")]
    public partial class AddIn : AddInBase
    {

        public override void OnCmd(ref EdmCmd poCmd, ref EdmCmdData[] ppoData)
        {
            base.OnCmd(ref poCmd, ref ppoData);

            try
            {
                var commandType = poCmd.meCmdType;

                switch (commandType)
                {
                    case EdmCmdType.EdmCmd_Menu:

                        // get the affected document versions 

                        var datum = ppoData.ToList().First();

                        var file = this.Vault.GetObject(EdmObjectType.EdmObject_File, datum.mlObjectID1) as IEdmFile5;
                        var folder = this.Vault.GetObject(EdmObjectType.EdmObject_Folder, datum.mlObjectID3) as IEdmFolder10;

                        var vault7 = this.Vault as IEdmVault7;

                        var historyUtility = vault7.CreateUtility(EdmUtility.EdmUtil_History) as IEdmHistory;

                        historyUtility.AddFile(file.ID);


                        EdmHistoryItem[] ppoRetHistory = null;
                        historyUtility.GetHistory(ref ppoRetHistory, (int)EdmHistoryType.Edmhist_FileVersion);

                        var latestVersion = ppoRetHistory.Max(x => x.mlVersion);

                        var versionsList = new List<int>();
                        for (int i = 1; i < latestVersion; i++)
                            versionsList.Add(i);

                        // show ui and get user input 
                        var form = new Views.MainForm(versionsList.ToArray());

                        form.ShowDialog(new Win32Window(poCmd.mlParentWnd));

                        var selectedVersion = form.SelectedVersion;

                        if (selectedVersion == -1)
                            return;

                        // check out document 
                        file.LockFile(folder.ID, poCmd.mlParentWnd);

                        // do a get 
                        file.GetFileCopy(poCmd.mlParentWnd, selectedVersion);


                        var message = $"Rolled forward version {selectedVersion}";

                        // check docuemnt back into the vault
                        file.UnlockFile(poCmd.mlParentWnd, message);

                        
                        this.Vault.MsgBox(poCmd.mlParentWnd, message, EdmMBoxType.EdmMbt_OKOnly, this.Identity.ToCaption("Operation complete"));

                        break;
                    default:
                        break;
                }


            }
            catch (Exception e)
            {
                var errorMessage = $"An error occured. {e.Message} {e.StackTrace}";
                this.Vault.MsgBox(poCmd.mlParentWnd, errorMessage, EdmMBoxType.EdmMbt_OKOnly, this.Identity.ToCaption("Fatal Error"));
            }
        }


        protected override void OnLoggerTypeChosen(LoggerType_e defaultType)
        {
            base.OnLoggerTypeChosen(LoggerType_e.File);
        }

        protected override void OnRegisterAdditionalTypes(Container container)
        {
            // register types with the container 
        }

        protected override void OnLoggerOutputSat(string defaultDirectory)
        {
            // set the logger default directory - ONLY USE IF YOU ARE NOT LOGGING TO PDM
        }
        protected override void OnLoadAdditionalAssemblies(DirectoryInfo addinDirectory)
        {
            base.OnLoadAdditionalAssemblies(addinDirectory);
        }

        protected override void OnUnhandledExceptions(bool catchAllExceptions, Action<Exception> logAction = null)
        {
            this.CatchAllUnhandledException = false;

            logAction = (Exception e) =>
            {

                throw new NotImplementedException();
            };


            base.OnUnhandledExceptions(catchAllExceptions, logAction);
        }
    }
}