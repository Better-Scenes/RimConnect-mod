# Rimworld Connection mod

## Setup

- Visual Studio 2017
- Clone project
- Open the project from `source/RimConnection/RimConnection.sln`
- follow steps: https://rimworldwiki.com/wiki/Modding_Tutorials/Setting_up_a_solution#Option_1_.28Manual_Method.29:
- Build using VS by hitting `f6`
- Create a symlink in your rimworld mods folder to the top level project folder
  - Do this from command prompt, not powershell or git bash
  - eg `mklink /D D:\games\Steam\steamapps\common\RimWorld\Mods\RimConnect D:\Projects\twitchIntegration\RimConnection-mod`
  - N.B. You may have to open command prompt as admin
- Enable the mod
- Start Rimworld normally
- Navigate to the Mods menu
- Enable the mod by ticking the red X (should become a green tick)

## Running

To run rimworld to test, you can run it through the exe and add a param to make it quick start

- `./RimWorldWin64 -quicktest`

## Useful tools and links

- https://marketplace.visualstudio.com/items?itemName=pragmatrix.BuildOnSave
  - Build the project every time you save
- Original modding tutorial setup from Rimworld wiki
  - https://rimworldwiki.com/wiki/Modding_Tutorials/Setting_up_a_solution
