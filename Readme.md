# Maui Xaml HotReload Issues

## 1 
We got issue on github like "datatemplate not updating in custom controls".
https://github.com/dotnet/maui/issues/10066
Actually when xaml hotreload finds a datatemplate and changes for it it's gonna update 
standart maui controlslike listview, collectionview etc ignoring custom controls.
The issue is deeper, it's like 
"any xaml change that doesn't have a link to an element in the visual tree 
will never be applied as we'll never be notified about it"

possible solutions:

A - create an interface like IHotReloadAware and call a method like
OnHotRead(changed element) for the parent element if it implements it.
This interface will likely be used by custom controls only, standart controls will act as usual.

B - Another solution would be to use in a more enhanced way the existing hotreload interface as described here
https://dev.to/davidortinau/c-ui-and-net-hot-reload-a-match-made-in-net-maui-243f

C - At the same time Andrei Misiukevich had a very nice custom xaml hotreload 
https://github.com/AndreiMisiukevich/HotReload that was calling OnHotReloaded() when updating.
When MS created it's own hotreload they didn't do such thing and Misiukevich' hotreload remained to be better but unfortunately became obsolete.

## 2
When hotreload fails for some reason you might never know about it witout opening "XAML Binding Failures" window etc.
Could show some feedback on fail, maybe change toolbar icon color to orange or something.

## 3
When hotreload fails randomly you can forget about it working again until you restart VS and uninstall app from simulator completely.


_Maui code-behind HotReload issues remain a totally different story but could be hopefully solved at the same time as this issue no.1 using same concept._
