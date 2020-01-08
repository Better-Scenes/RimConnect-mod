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

            Rect mainRect = new Rect(0f, 58f, inRect.width, inRect.height - 58f - 38f - 20f);
            this.FillMainRect(mainRect);

            GUI.EndGroup();
        }

        private void FillMainRect(Rect mainRect)
        {
            Text.Font = GameFont.Small;
            float height = 6f + (float)items.Count * 30f;
            Rect viewRect = new Rect(0f, 0f, mainRect.width - 16f, height);
            Widgets.BeginScrollView(mainRect, ref this.scrollPosition, viewRect, true);
            float num = 6f;
            float num2 = this.scrollPosition.y - 30f;
            float num3 = this.scrollPosition.y + mainRect.height;
            int index = 0;
            for (int i = 0; i < items.Count; i++)
            {
                if (num > num2 && num < num3)
                {
                    Rect rect = new Rect(0f, num, viewRect.width, 30f);
                    DrawItemRow(rect, items.ElementAt(i), index);
                }
                num += 30f;
                index++;
            }
            Widgets.EndScrollView();
        }

        private void DrawItemRow(Rect rect, KeyValuePair<string, IAction> keyValuePair, int index)
        {
            if (index % 2 == 1)
            {
                Widgets.DrawLightHighlight(rect);
            }

            Text.Font = GameFont.Small;
            GUI.BeginGroup(rect);
            float num = rect.width;

            Rect rect1 = new Rect(num - 100f, 0f, 100f, rect.height);
            rect1 = rect1.Rounded();
            int newPrice = items.ElementAt(index).Value.costSilverStore;
            string label = newPrice.ToString();
            rect1.xMax -= 5f;
            rect1.xMin += 5f;
            if (Text.Anchor == TextAnchor.MiddleLeft)
            {
                rect1.xMax += 300f;
            }
            if (Text.Anchor == TextAnchor.MiddleRight)
            {
                rect1.xMin -= 300f;
            }

            Rect rect2 = new Rect(num - 560f, 0f, 240f, rect.height);


            Widgets.IntEntry(rect2, ref newPrice, ref label, 50);
            items.ElementAt(index).Value.costSilverStore = newPrice;



            Text.Anchor = TextAnchor.MiddleLeft;
            Rect rect4 = new Rect(80f, 0f, rect.width - 80f, rect.height);
            Text.WordWrap = false;
            GUI.color = Color.white;
            Widgets.Label(rect4, items.ElementAt(index).Value.name);
            Text.WordWrap = true;

            GenUI.ResetLabelAlign();
            GUI.EndGroup();
        }

        public override Vector2 InitialSize
        {
            get
            {
                return new Vector2(1024f, (float)UI.screenHeight * 0.9f);
            }
        }

        private Vector2 scrollPosition = Vector2.zero;

        // Create a copy in case we make changes
        static private Dictionary<string, IAction> items = new Dictionary<string, IAction>(ActionList.generateActionLookup());
    }
}
