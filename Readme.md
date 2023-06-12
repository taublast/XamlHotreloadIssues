# Maui Xaml HotReload Issues

## 1 
We got issue on github like "datatemplate not updating in custom controls".
Actually when xaml hotreload finds a datatemplate and changes for it it's gonna update 
standart maui controlslike listview, collectionview etc ignoring custom controls.
The issue is deeper, it's like 
"any xaml change that doesn't have a link to an element in the visual tree 
will never be applied as we'll never be notified about it"

possible solutions:
a - create an interface like IHotReloadAware and call a method like
OnHotRead(changed element) for the parent element if it implements it.
This interface will likely be used by custom controls only, standart controls will act as usual.
b - Another solution would be to use in a more enhanced way the existing hotreload interface as described here
https://dev.to/davidortinau/c-ui-and-net-hot-reload-a-match-made-in-net-maui-243f
c - At the same time Andrei Misiukevich had a very nice custom xaml hotreload 
https://github.com/AndreiMisiukevich/HotReload but MS created it's own hotreload they didn't do such.
So Misiukevich' hotreload remained to be better but became obsolete.

## 2
when hotreload fails for some reason you might never know about it witout opening "xaml binding failuers" etc.
show some feedback on fail, maybe change icon color to orange or something

## 3
when hotreload fails sometimes you can forget about it working again until you restart VS, 
uninstall app from simulator completely.
