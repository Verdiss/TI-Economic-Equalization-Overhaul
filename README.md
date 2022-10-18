# Economic Equalization Overhaul
This is a fan mod for the game Terra Invicta. It modifies the way national economies work, to better represent the way countries of different sizes compare.


### Philosophy
In vanilla Terra Invicta, the way country size, country utility, and country control cost change relative to each other is problematic. A country that is twice the size is not twice as powerful, nor twice as expensive to control. Small countries are arbitrarily good at space programs and military, and large countries are arbitrarily good at welfare and education. These trade-offs are purely the result of game mechanic interactions, and do not capture any reasonable understanding of real life effects of national size, nor create any interesting game strategy trade-offs.

The primary cause of these bad relationships has to do with the cube-root relationship between country GDP and investment points, and with the up to 6 times multiplier on the control cost of a nation its number of control points.

As such, the primary function of this mod is to remove both of those issues. Investment points is a linear function of GDP, as is the control cost of a nation. Surrounding these core changes are a huge number of adjustments to the way national investments work, to account for this change and to otherwise even out the rate that a country's utility changes with its size. In this mod, a country that is twice as large is twice as good, but twice as expensive to control.

In short, the goal of the mod is to remove the unintuitive and unrealistic meta-strategizing that surrounds all choices regarding country management, country unifications, and country prioritization.


### Summary of Effects
-National monthly investment points are equal to 1 per 10 Billion GDP. Countries with a per capita GDP less than $15,000 receive up to a 30% penalty to investment points, however.

-The control cap cost of a nation is equal to its investment points / 10, which is then split across each control point. Control cost  of a nation is no longer dependent on the number of control points, as in vanilla.

-Arrival Internation Relations, Unity Movements, Great Nations, and Arrival Governance each reduce the control cost of a nation to be to the power of 0.98, 0.95, 0.90, or 0.85, depending on how many of these techs are researched. Larger nationss benefit significantly more.

-Investment effects that impact demographic stats such as education, inequality, or cohesion are scaled inversely based on population size. You need 1000 times as many knowledge investments to increase education by 0.1 in a country with 1 billion people compared to a country with 1 million population.

-Economy investments give a constant amount of GDP, plus a small amount per resource or core economic region, which is then modified by several demographics then split across the population in the form of GDP per capita.

-Countries with <$15,000 GDP per capita receive up to 5 times the GDP growth from economy investments as those over this level.

-Small adjustments to the relationships between things such as education and GDP growth, broadly maintaining vanilla levels of impact.

-Other changes to investment effects such as spoils and funding amount to flatten out the amount gained per investment to be constant regardless of country size.

-Adjusted costs of many investments, including greatly increasing the cost of flat effect investments like mission control or build army to reflect the greatly increased investment points available to most nations.

-Adjusted upkeep cost of armies to be dependent on the host country's tech level. Armies cost 15 IP per base tech level above 3 (minimum of 1 IP if at or below tech 3), doubled when away from home or in combat.

-Research output of a nation rebalanced. A nation no longer receives a flat 7.5 + education monthly research, however its research also increases linearly with population, not at a ^1.1 rate as vanilla.


### Issues & the State of the Mod
This mod is a very early stage, and bugs, unintended behaviors, and imbalances are all but guaranteed. If you encounter a bug, or feel strongly that something should be adjusted, either open an issue in this github repo or leave a comment on one of the mirror sites.

Current focuses for future updates are:

-Bug fixing

-Rebalancing numbers

-AI behavior adjustments

Known Issues:

-Ideological support impacts of the Unity and Spoils investments are temporarily disabled due to difficulties balancing their amounts


### Installation Info
Version 0.1.5 of this mod is built for Terra Invicta version 0.3.26

This mod requires [Unity Mod Manager version 0.25.0](https://www.nexusmods.com/site/mods/21/?tab=description) to be installed on your Terra Invicta executable with the DoorstopProxy installation method.

This mod should not be added to an in-progress campaign

To install the mod:

1: Find the [Releases](https://github.com/Verdiss/TIEconomyMod/releases) page on this repository and download the appropriate version's zip file (not source).

EITHER 2A: Unpack the TIEconomyMod folder inside this zip into your Terra Invicta\Mods\Enabled folder.

OR 2B: Open the Unity Mod Manager executable, select Terra Invicta, go to the Mods tab, and drag the zip file into the directed box.

3: You should now have a Terra Invicta\Mods\Enabled\TIEconomyMod folder containing a ModInfo.json file, among other things. If so, the mod is correctly installed.

4: Now open the game and start a new game. If the investment point cost of the lower half investments appears unchanged (I.e. boost costs a vanilla 2 investment points, not 40), the game must be restarted until these values change.

UPDATING: When updating this mod, completely remove the old version of the mod and replace it with the new.


### Version History
0.1.5 - 2022-10-18:

-Added effect to four global technologies (Arrival International Relations, Unity Movements, Great Nations, and Arrival Governance) that reduces the control cost of a nation to be the base to the power of 0.98, 0.95, 0.90, or 0.85 depending on the number of these techs researched


0.1.4 - 2022-10-17:

-Verified mod compatibility with game version 0.3.26

-Adjusted Ukraine's starting build army progress to reflect a vanilla change


0.1.3 - 2022-10-17:

-Changed IP costs of knowledge, unity, and military investments to 1, which fixes a math error causing these investments to be 2 or 3 times weaker than intended

-Doubled the impacts to democracy, unrest, military tech, cohesion, and inequality of all investments. This reflects a change in mod goals to mirror the behavior of larger vanilla nations as opposed to smaller

-Knowledge effects to democracy and education only improved by 33% this patch, as they were buffed 50% in an earlier patch

-Slightly increased GDP growth, and slightly increased the benefit of high education on GDP growth

-Vastly Increased yearly funding gain from the funding investment from 0.2 to 3 annual funding, which now means a funding investment gives you the same amount as a spoils investment after 20 years have passed

-Greatly increased effects of most events that grant investment completions


0.1.2 - 2022-10-16:

-Increased global research amounts by 30%, and increased impact from high education values, to better capture vanilla research values for large/developed nations

-Slightly increased the previously vastly reduced direct investment monetary cost

-Adjustments to AI evaluations of nations based on IP, should be in line with vanilla AI behavior as opposed to wildly over-valuing large nations.


0.1.1 - 2022-10-15:

-Fixed welfare and unity investments increasing, rather than decreasing, inequality and democracy, respectively

-Greatly reduced and equalized costs of direct investment

-Increased education and democracy gain from knowledge priority by about 50%

-Reduced GDP growth from economy investments by about 30%, to be less influenced by education, and greatly increased GDP growth from economy investments for nations with very low GDP per capita

-Adjusted monthly research values to be less impacted by high or low levels of GDP per capita, broadly reducing the amount of research produced worldwide


0.1.0 - Game Version 0.3.23 - 2022-10-15:

-Initial Release


### Links
[Steam Workshop](https://steamcommunity.com/sharedfiles/filedetails/?id=2876646709)

[Nexus](https://www.nexusmods.com/terrainvicta/mods/9)


### License
Do whatever you want with the code or files appearing in this repository.