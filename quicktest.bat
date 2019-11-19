rmdir /S /Q "D:\games\Steam\steamapps\common\RimWorld\Mods\RimConnect"
mkdir "D:\games\Steam\steamapps\common\RimWorld\Mods\RimConnect"

robocopy "%cd%/Assemblies" "D:\games\Steam\steamapps\common\RimWorld\Mods\RimConnect\Assemblies" /s
robocopy "%cd%/About" "D:\games\Steam\steamapps\common\RimWorld\Mods\RimConnect\About" /s
robocopy "%cd%/Languages" "D:\games\Steam\steamapps\common\RimWorld\Mods\RimConnect\Languages" /s
robocopy "%cd%/Defs" "D:\games\Steam\steamapps\common\RimWorld\Mods\RimConnect\Defs" /s

start D:\games\Steam\steamapps\common\RimWorld\RimWorldWin64.exe -quicktest