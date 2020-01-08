using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace RimConnection.Settings
{
    class ItemSettings : Window
    {
        public ItemSettings()
        {
            this.doCloseButton = true;
        }

        public override void DoWindowContents(Rect inRect)
        {
            GUI.BeginGroup(inRect);
            Rect rect2 = new Rect(inRect.width - 590f, 0f, 590f, 58f);
            GameFont old = Text.Font;
            Text.Font = GameFont.Medium;
            Widgets.Label(rect2, "Best Item Editor");
            Text.Font = old;

            GUI.EndGroup();
        }
    }
}
