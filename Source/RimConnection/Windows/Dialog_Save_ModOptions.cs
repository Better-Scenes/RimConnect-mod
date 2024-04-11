using RimConnection.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Verse;

namespace RimConnection.Settings
{
    public class Dialog_Save_ModOptions : TextInputWindow
    {
        private List<CommandOption> commandOptions;

        public Dialog_Save_ModOptions(List<CommandOption> commandOptions)
        {
            title = "Enter File Name (will be saved as an xml)";
            curText = "";
            this.commandOptions = commandOptions;
        }

        protected override bool Accept()
        {
            if(curText.Length == 0)
                return false;

            return Save(curText);
        }
        protected override AcceptanceReport Validate(string name)
        {
            if (name.Length < 1)
            {
                return "name too short";
            }

            // check invalid characters
            char[] invalidChars = Path.GetInvalidFileNameChars();
            foreach (char invalidChar in invalidChars)
            {
                if (name.Contains(invalidChar))
                {
                    return name + " contains one or more of the following invalid characters: " + new string(invalidChars);
                }
            }

            return true;
        }

        private bool Save(string name, bool force = false)
        {
            if (name.NullOrEmpty())
            {
                return false;
            }

            string path = Path.Combine(GenFilePaths.ConfigFolderPath, "RimConnect");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filePath = Path.Combine(path, name + ".xml");
            if (File.Exists(filePath) && !force)
            {
                void okCallback()
                {
                    Save(name, true);
                }

                Dialog_MessageBox confirmation = new Dialog_MessageBox("Are you sure you want to overwrite " + name + "?", "OK",
                    okCallback,
                    "Cancel", null, null, true, okCallback,
                    null);
                Find.WindowStack.Add(confirmation);
                return false;
            }

            try
            {
                Scribe.saver.InitSaving(filePath, "ModOptions");
                ExposeData();
                Scribe.saver.FinalizeSaving();
                Dialog_MessageBox savedDialog = new Dialog_MessageBox("Saved to " + filePath, "OK");
                Find.WindowStack.Add(savedDialog);
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return false;
            }
        }

        private void ExposeData()
        {
            Scribe_Collections.Look(ref commandOptions, "ItemConfigData");
        }
    }
}
