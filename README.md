# TIEconomyMod
This is a fan mod for the game Terra Invicta. It modifies the way national economies work, to better represent the way countries of different sizes compare.


### Philosophy
In vanilla Terra Invicta, the way country size, country utility, and country control cost change relative to each other is problematic. A country that is twice the size is not twice as powerful, nor twice as expensive to control. Small countries are arbitrarily good at space programs and military, and large countries are arbitrarily good at welfare and education. These trade-offs are purely the result of game mechanic interactions, and do not capture any reasonable understanding of real life effects of national size, nor create any interesting game strategy trade-offs.

The primary cause of these bad relationships has to do with the cube-root relationship between country GDP and investment points, and with the up to 6 times multiplier on the control cost of a nation its number of control points.

As such, the primary function of this mod is to remove both of those issues. Investment points is a linear function of GDP, as is the control cost of a nation. Surrounding these core changes are a huge number of adjustments to the way national investments work, to account for this change and to otherwise even out the rate that a country's utility changes with its size. In this mod, a country that is twice as large is twice as good, but twice as expensive to control.


### Summary of Effects
-National monthly investment points are equal to 1 per 10 Billion GDP. Countries with a per capita GDP less than $15,000 recieve up to a 30% penalty to investment points, however.

-The control cap cost of a nation is equal to its investment points / 10, which is then split across each control point. Control cost  of a nation is no longer dependent on the number of control points, as in vanilla.

-Investment effects that impact demographic stats such as education, inequality, or cohesion are scaled inversely based on population size. You need 1000 times as many knowledge investments to increase education by 0.1 in a country with 1 billion people compared to a country with 1 million population.

-Economy investments give a constant amount of gdp, plus a small amount per resource or core economic region, which is then modified by several demographics then split across the population.

-Countries with <$15,000 gdp per capita recieve up to 4 times the gdp growth from economy investments as those over this level.

-Small adjustments to the relationships between things such as education and gdp growth, broadly maintaining vanilla levels of impact.

-Other changes to investment effects such as spoils and funding amount to flatten out the amount gained per investment to be constant regardless of country size.

-Adjusted costs of spaceflight, boost, mission control, build army, build navy, build nuclear weapon, and build space defense to reflect the greatly increased investment points available to most nations.

-Adjusted upkeep cost of armies to be dependent on the host country's tech level. Armies cost 15 IP per base tech level above 3 (minimum of 1 IP if at or below tech 3), doubled when away from home or in combat.

-Research output of a nation rebalanced. A nation no longer recieves a flat 7.5 + education monthly research, however its research also increases linearly with population, not at a ^1.1 rate as vanilla.


### State of the Mod
This mod is a very early stage, and bugs, unintended behaviors, and imbalances are all but guaranteed. If you encounter a bug, or feel strongly that something should be adjusted, either open an issue in this github repo, or contact Verdiss on the Pavonis Interactive Discord server.

Currently, no changes to AI have been made, and as such AI will behave less intelligently with this mod than without. AI adjustments will be made in future updates.


### Installation Info
Version 0.1.0 of this mod is built for Terra Invicta version 0.3.23

This mod requires [Unity Mod Manager version 0.25.0](https://www.nexusmods.com/site/mods/21/?tab=description) to be installed on your Terra Invicta executable with the DoorstopProxy installation method.

This mod should not be added to an in-progress campaign

To install the mod:

1: Find the [Releases](https://github.com/Verdiss/TIEconomyMod/releases) page on this repository and download the appropriate version's zip file (not source).

EITHER 2A: Unpack the TIEconomyMod folder inside this zip into your Terra Invicta\Mods\Enabled folder.

OR 2B: Open the Unity Mod Manager executable, select Terra Invicta, go to the Mods tab, and drag the zip file into the directed box.

3: You should now have a Terra Invicta\Mods\Enabled\TIEconomyMod folder containing a ModInfo.json file, among other things. If so, the mod is correctly installed.

4: Now open the game and start a new game. If the investment point cost of the lower half investments appears unchanged (I.e. boost costs a vanilla 2 investment points, not 40), the game must be restarted until these values change.


### Version History
Mod 0.1.0 - Game Version 0.3.23 - 2022-10-15 - Initial Release


### License
Do whatever you want with the code or files appearing in this repository.