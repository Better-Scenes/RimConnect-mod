rmdir /S /Q "D:\games\Steam\steamapps\common\RimWorld\Mods\RimConnect\About"
rmdir /S /Q "D:\games\Steam\steamapps\common\RimWorld\Mods\RimConnect\Languages"
rmdir /S /Q "D:\games\Steam\steamapps\common\RimWorld\Mods\RimConnect\Defs"
rmdir /S /Q "D:\games\Steam\steamapps\common\RimWorld\Mods\RimConnect\v1.1"


mkdir "D:\games\Steam\steamapps\common\RimWorld\Mods\RimConnect"

robocopy "%cd%/Assemblies" "D:\games\Steam\steamapps\common\RimWorld\Mods\RimConnect\v1.1\Assemblies" /s
robocopy "%cd%/About" "D:\games\Steam\steamapps\common\RimWorld\Mods\RimConnect\About" /s
robocopy "%cd%/Languages" "D:\games\Steam\steamapps\common\RimWorld\Mods\RimConnect\Languages" /s
robocopy "%cd%/Defs" "D:\games\Steam\steamapps\common\RimWorld\Mods\RimConnect\Defs" /s
COPY LoadFolders.xml "D:\games\Steam\steamapps\common\RimWorld\Mods\RimConnect"

start D:\games\Steam\steamapps\common\RimWorld\RimWorldWin64.exe -quicktest