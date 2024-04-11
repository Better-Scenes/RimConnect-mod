using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace RimConnection.Windows
{
    public abstract class TextInputWindow : Window
    {
        public override Vector2 InitialSize
        {
            get
            {
                return new Vector2(350f, 175f);
            }
        }

        protected string curText;
        protected string title;
        protected string acceptBtnLabel;
        protected string closeBtnLabel;

        private bool opened;

        public TextInputWindow()
        {
            closeOnClickedOutside = true;
            doCloseX = true;
            absorbInputAroundWindow = true;
            closeOnAccept = true;
            acceptBtnLabel = "OK";
        }

        public override void DoWindowContents(Rect inRect)
        {
            Widgets.Label(new Rect(0, 15f, inRect.width, 35f), title);
            string text = Widgets.TextField(new Rect(0, 25 + 15f, inRect.width, 35f), curText);
            if ((curText != text || !opened) && Validate(text))
               curText = text;

            DrawExtra(inRect);

            if (!opened)
            {
                UI.FocusControl("RenameField", this);
                opened = true;
            }

            var btnsRect = new Rect(0f, inRect.height - 35f - 5f, closeBtnLabel != null ? 210 : 120, 35f).CenteredOnXIn(inRect);

            if (Widgets.ButtonText(btnsRect.LeftPartPixels(closeBtnLabel != null ? 100 : 120), acceptBtnLabel, true, false))
                OnAcceptKeyPressed();

            if (closeBtnLabel != null)
                if (Widgets.ButtonText(btnsRect.RightPartPixels(100), closeBtnLabel, true, false))
                    OnCloseButton();
        }

        public virtual void OnCloseButton()
        {
            Close();
        }

        public override void OnAcceptKeyPressed()
        {
            Accept();
            base.OnAcceptKeyPressed();
        }

        protected abstract bool Accept();

        protected virtual AcceptanceReport Validate(string str) => true;

        public virtual void DrawExtra(Rect inRect) { }
    }
}
