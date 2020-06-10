# Rimworld Connection mod

## Setup

- Install Visual Studio (your choice which version)
- Clone project
- Open the project from `source/RimConnection/RimConnection.sln`
- Build using VS by hitting `f6` or using `build -> build Solution`
- (WIP) Put some step here around how to make sure it builds to the correct location
- Start Rimworld normally
- Navigate to the Mods menu
- Enable the mod by ticking the red X (should become a green tick)

## Running

To run rimworld to test, you can run it through the exe and add a param to make it quick start

- `./RimWorldWin64 -quicktest`

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
