# RimConnect mod for RimWorld

This repository is the Mod portion of RimConnect. It contains the code that handles events and what is available for streamers to use in their game. If you want to know more about how it works as a streamer or viewer, you can check out the attached wiki articles [over here](https://github.com/Better-Scenes/RimConnect-mod/wiki)

# Contributing

Read the following if you're interested in adding functionality to RimConnect. If you just want to use it as a streamer or viewer, check out the wiki [over here](https://github.com/Better-Scenes/RimConnect-mod/wiki)

## Setup

- Install Visual Studio (your choice which version, or other IDE if you know how to setup)
- Clone project
- Open the project from `source/RimConnection/RimConnection.sln`
- Setup the output directory to be your RimWorld mod folder (currently no way to actually do this, needs fixing)
- Build using VS by hitting `f6` or using `build -> build Solution`
- Start Rimworld normally
- Navigate to the Mods menu
- Enable the mod by ticking the red X (should become a green tick)

## Running

To run RimWorld quickly, there is a quicktest paramater you can pass to the exectuable from a command line or through steam launch options.

- run `./RimWorldWin64 -quicktest` from a console in your RimWorld folder, OR
- add `-quicktest` to steam launch options, instructions [here](https://support.steampowered.com/kb_article.php?ref=1040-JWMT-2947)

## Project structure and how things hook up

There are 3 major component that make RimConnect work. The server and extension are currently private repositories.

### Overview of classes

- Actions: These are the things that are actually run to make things happen in the game
- API: Functions and data classes that interact with the RimConnect server
- IncidentWorkers: New events that are custom made should go in here before being turned into an action


## Useful tools and links

- https://marketplace.visualstudio.com/items?itemName=pragmatrix.BuildOnSave
  - Build the project every time you save
- Original modding tutorial setup from Rimworld wiki
  - https://rimworldwiki.com/wiki/Modding_Tutorials/Setting_up_a_solution
