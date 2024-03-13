using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Verse;

namespace RimConnection.Settings
{
    class CommandOptionSettings : Window
    {
        private static readonly string CONFIG_PATH = Path.Combine(GenFilePaths.ConfigFolderPath, "RimConnect");

        private Vector2 scrollPosition = Vector2.zero;
        private CommandOptionList cachedCommandOptionList = new CommandOptionList();
        private List<CommandOption> commandOptions = new List<CommandOption>();
        private List<CommandOption> filteredRows = new List<CommandOption>();
        private List<string> actionCategories = new List<string>();

        public CommandOptionSettings()
        {
            this.doCloseButton = true;

            // Create a copy of the global CommandOptionList
            this.cachedCommandOptionList = (CommandOptionList)CommandOptionListController.commandOptionList.Clone();
            this.commandOptions = cachedCommandOptionList.commandOptions.Select(item => (CommandOption)item.Clone()).ToList();
            this.actionCategories = commandOptions.Select(category => category.Action().Category).Distinct().Prepend("All").ToList();
            this.filteredRows = FilteredRows();

            // Sort the actions
            this.SortFilteredRows(true);
        }

        public override void Close(bool doCloseSound = true)
        {
            base.Close(doCloseSound);

            // Check each CommandOption if it's been modified, add modified options to new CommandOptionList object

            CommandOptionList updatedCommandOptions = new CommandOptionList();

            updatedCommandOptions.commandOptions = commandOptions.Where(
                commandOption =>
                // has price changed
                commandOption.costSilverStore != CommandOptionListController.commandOptionList.commandOptions.Where(option => commandOption.actionHash == option.actionHash).First().costSilverStore ||
                // have cooldowns changed
                commandOption.localCooldownMs != CommandOptionListController.commandOptionList.commandOptions.Where(option => commandOption.actionHash == option.actionHash).First().localCooldownMs ||
                commandOption.globalCooldownMs != CommandOptionListController.commandOptionList.commandOptions.Where(option => commandOption.actionHash == option.actionHash).First().globalCooldownMs
              )
              .ToList();

            if (updatedCommandOptions.commandOptions.Count <= 0)
            {
                return;
            }

            RimConnectAPI.PostUpdatedCommandOptions(updatedCommandOptions);

            // Update the global CommandOptionList with the modified CommandOptionList
            this.cachedCommandOptionList.commandOptions = this.commandOptions;
            CommandOptionListController.commandOptionList = this.cachedCommandOptionList;
        }

        public override void DoWindowContents(Rect inRect)
        {
            // Update Rows in Case any filters were applied
            UpdateFilteredRows();

            // Header Section
            GUI.BeginGroup(inRect);
            Rect rect2 = new Rect(inRect.width - 590f, 0f, 590f, 58f);
            GameFont old = Text.Font;
            Text.Font = GameFont.Medium;
            Widgets.Label(rect2, "<b>Loyalty Store</b> Settings");
            Text.Font = old;

            // Draw search and sort filters
            Rect filterRow = new Rect(0f, 40f, inRect.width, 30f);
            DrawFiltersRow(filterRow);

            // Draw tab row
            Rect tabRow = new Rect(80f, 80f, inRect.width, 30f);
            DrawTabsRow(tabRow);

            // Draw column header row
            Rect headerRow = new Rect(0f, 180f, inRect.width, 30f);
            DrawHeaderRow(headerRow);

            // Check if sort/filters/search should be used

            // Draw main list of items
            Rect mainRect = new Rect(0f, 198f, inRect.width, inRect.height - 58f - 38f - 20f - 120f);
            this.FillMainRect(mainRect);

            GUI.EndGroup();
        }

        private void UpdateFilteredRows(bool force = false)
        {
            if (searchQuery != lastSearchQuery || currentCategory != lastCategory || force)
            {
                filteredRows = FilteredRows();
                SortFilteredRows(true);

                lastSearchQuery = searchQuery;
                lastCategory = currentCategory;
            }
        }

        private void SortFilteredRows(bool force = false)
        {
            if (sortMethod != lastSortMethod || force)
            {
                switch (sortMethod)
                {
                    case SortMethod.Name:
                        filteredRows = filteredRows.OrderBy(commandOption => commandOption.Action().Name).ToList();
                        break;
                    case SortMethod.Cost:
                        filteredRows = filteredRows.OrderBy(commandOption => commandOption.costSilverStore).ToList();
                        break;
                    case SortMethod.LocalCooldown:
                        filteredRows = filteredRows.OrderBy(commandOption => commandOption.localCooldownMs).ToList();
                        break;
                    case SortMethod.GlobalCooldown:
                        filteredRows = filteredRows.OrderBy(commandOption => commandOption.globalCooldownMs).ToList();
                        break;
                }

                lastSortMethod = sortMethod;
            }

            if (sortOrder == SortOrder.Descending)
            {
                filteredRows.Reverse();
            }
        }

        private void ReverseSortOrder()
        {
            filteredRows.Reverse();
        }

        private void ExposeData()
        {
            Scribe_Collections.Look(ref commandOptions, "ItemConfigData");
        }

        private void DrawFiltersRow(Rect rect)
        {
            Rect searchBar = new Rect(-70f, rect.y, 400f, rect.height);
            searchQuery = Widgets.TextEntryLabeled(searchBar, "Search: ", searchQuery);

            Rect clearSearch = new Rect(searchBar);
            clearSearch.x += clearSearch.width + 10f;
            clearSearch.width = 40f;

            if (Widgets.ButtonText(clearSearch, "X", false))
            {
                searchQuery = "";
            }

            Rect sortMethodButton = new Rect(rect.x + rect.width - 240f, rect.y, 200f, rect.height);

            if (Widgets.ButtonText(sortMethodButton, "Sort By " + sortMethod.ToString()))
            {  
                int sortMethodIndex = 0;
                if ((int)sortMethod != 3)
                {
                    sortMethodIndex = (int)sortMethod + 1;
                }

                sortMethod = (SortMethod)Enum.Parse(typeof(SortMethod), sortMethodIndex.ToString());

                SortFilteredRows();
            }

            Rect loadButton = new Rect(sortMethodButton.x - 100f, rect.y, 100f, rect.height);

            if (Widgets.ButtonText(loadButton, "Load File"))
            {
                if (!Directory.Exists(CONFIG_PATH))
                {
                    Directory.CreateDirectory(CONFIG_PATH);
                }

                var floatMenuOptions = Directory.GetFiles(CONFIG_PATH)
                    .Select((filePath) => {
                        string fileName = Path.GetFileNameWithoutExtension(filePath);
                        return new FloatMenuOption(fileName, () => {
                            Verse.Log.Message("Clicked on: " + fileName);
                            Scribe.loader.InitLoading(filePath);
                            Scribe.EnterNode("ModOptions");
                            ExposeData();
                            Scribe.loader.FinalizeLoading();
                            UpdateFilteredRows(true);
                        });
                    })
                    .ToList();

                Find.WindowStack.Add(new FloatMenu(floatMenuOptions));
            }

            Rect saveButton = new Rect(loadButton.x - 100f, rect.y, 100f, rect.height);

            if (Widgets.ButtonText(saveButton, "Save File"))
            {
                Find.WindowStack.Add(new Dialog_Save_ModOptions(commandOptions));
            }

            Rect sortOrderbutton = new Rect(sortMethodButton.x + sortMethodButton.width, rect.y, 40f, rect.height);

            string arrow = sortOrder == SortOrder.Ascending ? "↑" : "↓";

            if (Widgets.ButtonText(sortOrderbutton, arrow))
            {
                if (sortOrder == SortOrder.Ascending)
                {
                    sortOrder = SortOrder.Descending;
                }
                else
                {
                    sortOrder = SortOrder.Ascending;
                }

                ReverseSortOrder();
            }
        }

        private void DrawTabsRow(Rect rect)
        {
            float startingXValue = rect.x;
            Rect button = new Rect(rect.x, rect.y, 120f, rect.height);

            foreach (string itemCategory in this.actionCategories)
            {
                if (rect.width - 50 < button.x + button.width)
                {
                    button.y += button.height;
                    button.x = startingXValue;
                }

                if (currentCategory != itemCategory)
                {
                    if (Widgets.ButtonText(button, itemCategory))
                    {
                        currentCategory = itemCategory;
                    }
                }
                else
                {
                    Widgets.ButtonText(button, $"<color=yellow>{itemCategory}</color>", true, false, Verse.ColorLibrary.DarkRed, true);
                }

                button.x += button.width;
            }
        }

        private void DrawHeaderRow(Rect rect)
        {
            Rect eventLabel = new Rect(80f, rect.y, 100f, rect.height);
            Widgets.Label(eventLabel, "<b>Events</b>");

            Rect silverCostLabel = new Rect(rect.width - 580f, rect.y, 75f, rect.height);
            Widgets.Label(silverCostLabel, "<b>Silver Cost</b>");

            Rect localCooldownLabel = new Rect(silverCostLabel);
            localCooldownLabel.x += localCooldownLabel.width + 75f;
            localCooldownLabel.width = 200f;
            Widgets.Label(localCooldownLabel, "<b>Local Cooldown(s)</b>");

            Rect globalCooldownLabel = new Rect(localCooldownLabel);
            globalCooldownLabel.x += 175f;
            Widgets.Label(globalCooldownLabel, "<b>Global Cooldown(s)</b>");
        }

        private void FillMainRect(Rect mainRect)
        {
            // Make Scrolling Menu
            Text.Font = GameFont.Small;
            float height = 6f + (float)filteredRows.Count * 30f;
            Rect viewRect = new Rect(0f, 0f, mainRect.width - 16f, height);
            Widgets.BeginScrollView(mainRect, ref this.scrollPosition, viewRect, true);

            float num = 6f;
            float num2 = this.scrollPosition.y - 30f;
            float num3 = this.scrollPosition.y + mainRect.height;

            // Draw a row for every entry
            int index = 0;

            for (int i = 0; i < filteredRows.Count; i++)
            {
                if (num > num2 && num < num3)
                {
                    Rect rect = new Rect(0f, num, viewRect.width, 30f);
                    DrawItemRow(rect, filteredRows[i], index);  
                }

                num += 30f;
                index++;
            }
            Widgets.EndScrollView();
        }

        private List<CommandOption> FilteredRows()
        {
            List<CommandOption> filteredRows = new List<CommandOption>();

            foreach (CommandOption commandOption in commandOptions)
            {
                if (RowIncludedInFiltering(commandOption))
                {
                    // remove anything that is a premium event (cost -1)
                    if(commandOption.costSilverStore >= 0)
                    {
                        filteredRows.Add(commandOption);
                    }
                }
            }

            return filteredRows;
        }

        private bool RowIncludedInFiltering(CommandOption commandOption)
        {
            bool included = true;

            if (currentCategory != "All")
            {
                switch(currentCategory)
                {
                    default:
                        if (commandOption.Action().Category != currentCategory.ToString())
                        {
                            return false;
                        }
                        break;
                }
            }

            if (searchQuery != "")
            {
                // Does CommandOption fit search query

                // Minify CommandOptionName
                string commandOptionNameMinified = commandOption.Action().Name.Replace(" ", "").ToLower();

                // Get each section of text separated by spaces
                List<string> searchQueryMatches = new List<string>();
                if (searchQuery.Contains(" "))
                {
                    searchQueryMatches = searchQuery.Split(' ').OfType<string>().ToList();
                }
                else
                {
                    searchQueryMatches.Add(searchQuery);
                }                   

                foreach (string searchText in searchQueryMatches)
                {
                    // If either one of the search text sections contains or is equal to a command option name, stop checking and include in list
                    if (searchText.ToLower() == commandOptionNameMinified || commandOptionNameMinified.Contains(searchText.ToLower()))
                    {
                        return true;
                    }
                }
                
                // If no searching matched and there search query is not blank do not include
                included = false;
            }

            return included;
        }

        private void DrawItemRow(Rect rect, CommandOption commandOption, int index)
        {
            // Highlight every other row

            if (index % 2 == 1)
            {
                Widgets.DrawLightHighlight(rect);
            }

            Text.Font = GameFont.Small;
            GUI.BeginGroup(rect);
            float num = rect.width;

            Rect rect1 = new Rect(num - 100f, 0f, 100f, rect.height);
            rect1 = rect1.Rounded();

            // TextAnchor Fix

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

            Rect rect2 = new Rect(num - 560f, 0f, 75f, rect.height);

            // Store value for reference
            int newPrice = commandOption.costSilverStore;
            string priceLabel = newPrice.ToString();

            int newLocalCooldown = commandOption.localCooldownMs /  1000;
            string localCooldownLabel = newLocalCooldown.ToString();

            int newGlobalCooldown = commandOption.globalCooldownMs / 1000;
            string globalCooldownLabel = newGlobalCooldown.ToString();

            // Silver Cost
            Widgets.TextFieldNumeric(rect2, ref newPrice, ref priceLabel, 0f);
            filteredRows[index].costSilverStore = newPrice;

            // Local Cooldown

            rect2.x += rect2.width + 75f;

            Widgets.TextFieldNumeric(rect2, ref newLocalCooldown, ref localCooldownLabel, 0f);
            filteredRows[index].localCooldownMs = newLocalCooldown * 1000;

            // Global Cooldown

            rect2.x += rect2.width + 100f;

            Widgets.TextFieldNumeric(rect2, ref newGlobalCooldown, ref globalCooldownLabel, 0f);
            filteredRows[index].globalCooldownMs = newGlobalCooldown * 1000;

            // Icons for ItemDefs
            Rect rect3 = new Rect(0f, 0f, 27f, 27f);
            if (commandOption.Action() is ItemAction)
            {
                ItemAction itemAction = (ItemAction)commandOption.Action();
                ThingDef thingDef = itemAction.thingDef;
                Widgets.ThingIcon(rect3, thingDef);
                Widgets.InfoCardButton(40f, 0f, thingDef);
            }
            // duplicated code for getting the tame animal images and descriptions
            if (commandOption.Action() is TameAnimalAction)
            {
                TameAnimalAction itemAction = (TameAnimalAction)commandOption.Action();
                ThingDef thingDef = itemAction.thingDef;
                Widgets.ThingIcon(rect3, thingDef);
                Widgets.InfoCardButton(40f, 0f, thingDef);
            }

            // Label for item/event

            Text.Anchor = TextAnchor.MiddleLeft;
            Rect rect4 = new Rect(80f, 0f, rect.width - 80f, rect.height);
            Text.WordWrap = false;
            GUI.color = Color.white;
            Widgets.Label(rect4, commandOption.Action().Name.CapitalizeFirst());
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

        enum ItemCategory
            {
                All = 0,
                Item = 1,
                Event = 2,
                Colonists = 3,
                Apparel = 4,
                MeleeWeapons = 5,
                RangedWeapons = 6,
                Drugs = 7,
                Meat = 8,
                Leather = 9,
                Consumables = 10,
                Metal = 12,
                Animal = 13,
                Furniture = 14
            };

        enum SortMethod
            {
                Name = 0,
                Cost = 1,
                LocalCooldown = 2,
                GlobalCooldown = 3
            };

        enum SortOrder
        {
            Ascending,
            Descending
        }

        static string searchQuery = "";

        static string currentCategory = "All";

        static SortMethod sortMethod = SortMethod.Name;

        static SortOrder sortOrder = SortOrder.Ascending;

        static string lastSearchQuery = "";

        static string lastCategory = "All";

        static SortMethod lastSortMethod = SortMethod.Name;
    }
}
