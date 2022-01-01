# GTA-V-Ped-Prop-Align-Tool
![Image Not Available](https://i.ibb.co/Zh1DHZw/Screenshot-2022-01-01-163939.png)

![Image Not Available](https://i.ibb.co/85gtGzX/Screenshot-2022-01-01-164148.png)

GTA V Ped Prop Align Tool is a tool for anyone that needs to properly align a prop to a ped for playing an animation or creating a synchronized scene.
It allows the user to fine tune both the position and rotation of the desired prop in relation to the ped through a simple user interface.
After the user aligns the prop to their desired level of percision, they can press **NumPad0** and the script will automatically copy the floats to the user's clipboard.
This tool uses the native function ENTITY::ATTACH_ENTITY_TO_ENTITY to attach the prop to the ped and you should use this function when using the generated values in your own projects for best results.

## Requirements 
- [Script Hook V](http://www.dev-c.com/gtav/scripthookv/)
- [Script Hook V .NET](https://github.com/crosire/scripthookvdotnet)
- [LemonUI](https://github.com/justalemon/LemonUI)

## Installation
- Download and install the latest versions of Script Hook V, Script Hook V .NET, and LemonUI
- Download the latest version of **Ped Prop Align Tool.dll** and place it into your scripts folder
- .pdb files are optional and are only used for giving error reports if the script were to crash

## Useage
- The key to open the menu is **Subtract**(NumPadMinus)
- Input the model name of the prop you wish to import (leave out any quotation marks)
- Set the desired bone index (**NOT ID**) to attach the prop to
- Attach the prop to the ped and adjust its position and rotation
- Press **NumPad0** to copy the current floats to your clipboard

## Example
#### By default when you attach the prop gr_prop_gr_drill_01a, it is misaligned with the player's hand:

![Image Not Available](https://i.ibb.co/nrFXK5X/Screenshot-2022-01-01-175444.png)

#### After aligning it, you can make it look like the player is actually holding it:

![Image Not Available](https://i.ibb.co/rQSSB92/Screenshot-2022-01-01-170033.png)

In this case the values were X Pos: 0.15, Prop Y Pos: -0.1, Prop Z Pos: -0.1, Prop X Rot: 346, Prop Y Rot: 46, Prop Z Rot: 100

Therefore to implement this using the native function ENTITY::ATTACH_ENTITY_TO_ENTITY in ScriptHookVDotNet you would write:

Function.Call(Hash.ATTACH_ENTITY_TO_ENTITY, prop, Game.Player.Character, 90, 0.15f, -0.1f, -0.1f, 346f, 46f, 100f, 0, 0, 0, 0, 2, 1);

See the source code if any further clarification is needed.

## Notes
- The default values for the checkboxes work most of the time, especially for attaching props to peds
- The default vertex of 2 does not need to be changed in most cases
- The default bone is the player's right hand (index 90)
- Make sure you remove the prop before reloading your scripts because if you don't it will stay there until you restart your game

## Helpful Resources
- A database of game objects (props) can be found at [gta-objects.xyz](https://gta-objects.xyz/objects)
- A database of ped bone id's and indexes can be found at the [Rage MP Wiki](https://wiki.rage.mp/index.php?title=Bones)
- For more information on how to implement the native function ENTITY::ATTACH_ENTITY_TO_ENTITY, please visit [Native DB](https://alloc8or.re/gta5/nativedb/)
