using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace RimConnection.Settings
{
    class ResetCommandOptionsModal : Window
    {
        public ResetCommandOptionsModal()
        {
            this.doCloseButton = true;
        }

        public override void Close(bool doCloseSound = true)
        {
            base.Close(doCloseSound);
        }

        public override void DoWindowContents(Rect rect)
        {
            Rect header = new Rect(0, 32f, rect.width, 64f);
            Widgets.Label(header, "<size=32>Reset events/items</size>");

            GUI.BeginGroup(new Rect(0, header.y + header.height + 10f, rect.width, 200f));
            Rect warningLabel = new Rect(0, 0, 400f, 60f);
            Widgets.Label(warningLabel, "Warning: by pressing the below reset button you will be resetting all of the shop prices and cooldowns to their defaults. This cannot be undone");
            warningLabel.y += warningLabel.height + 20f;
            Widgets.Label(warningLabel, "<color=red>This cannot be undone</color>");

            if (Widgets.ButtonText(new Rect(0, 120f, 80f, 40f), "<color=red>Reset</color>"))
            {
                List<ValidCommand> validCommands = ActionList.ActionListToApi().validCommands;
                CommandOptionList commandOptionList = new CommandOptionList();
                commandOptionList.commandOptions = validCommands.Select(validCommand => validCommand.toCommandOption()).ToList();
                RimConnectAPI.PostUpdatedCommandOptions(commandOptionList);

                // Update the global CommandOptionList with the modified CommandOptionList
                CommandOptionListController.commandOptionList = commandOptionList;
                Log.Message("RimConnect items have been reset");
            }

            GUI.EndGroup();


        }
    }
}
