#### Release 3.0.3
##### Breaking Changes
<ul style="color: red">
 <li> Minimum .NET Framework version required is 4.6.1.</li>
</ul>

##### Contributors for this version
 * Ashilta
 * JimiC
 * Elliena Bulmer
 
##### New Features
 * Unfortunately, no new features are provided in this release.
 
##### Bug Fixes
 * Fixed a bug wich caused exceptions to be thrown in the skill planner when no plan was loaded
 * Fixed a bug which caused issues parsing the details of a users wallet following trades in Citadels
 * Fixed a bug where the EVEMon installer was unable to see more recent versions of the .NET Framework
 * Fixed a bug where scrolling through the skill queue would occasionally throw a 'System.DivideByZero' exception
 
##### Enhancements
 * EVEMon has been brought up-to-date with the latest version of the EVE SDE.
 
##### Misc
  * An issue where the pre-requisites of the Advanced Weapon Upgrades were listed incorrectly is resolved by updating the SDE
  
----

#### Release 3.0.0
##### Breaking Changes
<ul style="color: red">
 <li> Minimum .NET Framework version required is 4.6.1.</li>
 <li> Windows XP is no more supported.</li>
 <li> Windows Vista is no more supported.</li>
 <li> EVEMon Market Unified Uploaded (EMUU) has been removed.</li>
 <li> BattleClinic cloud storage service has been removed.</li>
 <li> Downgrading to v2 is not supported, without losing your settings.</li>
</ul>

##### Contributors for this version

 * Jimi
 * Innocent Enemy

##### New Features
 
 * Added market price provider selector, EVE-Marketdata added as provider.
 * Added ability to open a data browser from everywhere.
  
##### Bug Fixes

 * Windows OS version will now be correctly reported in the bug report window.
 * Fixed mineral prices reset on download failure.
 * Fixed Overview visual bugs when updating on settings changed.
 * Fixed a visual bug where context menu to plan a ship mastery was available for ships with no mastery.
 * Fixed a visual bug when warning label disappears when viewing a character's skills list.
 * Fixed persistence of menu bar selection.
 * Fixed an issue with the indicator bar in skill queue when switching to a partially trained skill queue.
 * Fixed an issue with the access mask of an API key when it exceeds the integer 32bit max value.
 * Fixed assembly array usage for fighters and force auxiliaries in Blueprint browser.

##### Enhancements

 * Added Dropbox as cloud storage service.
 * Added indicator to main window status bar when uploading to a cloud storage service provider.
 * Added Google Drive as cloud storage service provider.
 * Added OneDrive as cloud storage service provider.
 * Added support for Google Calendar API v3.
 * Added price updating indicator in character Assets monitor.
 * Added ability to asynchronously update the character Assets monitor.
 * Added number formatting for the amount of the BOM of a blueprint.
 * Added ability to remember users choice to show only ECUs in Planetary Colonies.
 * Added a custom cursor arrow with context menu wherever a context menu is available.
 * Reworked character header monitor to display the alliance, located and docked info directly instead of a tooltip.
 * Improved the startup time.
 * Added required skill injectors indicator for a character plan.
 * Added type group of a planetary pin which enables grouping and sorting pins by type group in character planetary monitor.
 * Added ability to export a characters comparison as CSV.
 * Added skill queue warning threshold indicator.
 * Added menu item to create a plan from a character skill queue in main window 'Plans' menu and skill planner 'Select plan' menu.
 * Changed the behavior of the Skill Planner window (one window per character).
 * Added ability to set the skill queue warning threshold in Options.
 * New data files update window.
 * Add link to Read The Docs, where EVEMon's manual resides, in Help.
 * Improved the behavior of the G15 handler. I will now detect when a compatible keyboard is available.
 * Added support for the remaining endpoints of the XML API.

##### Misc

 * Added circuit breaker when fetching images in order to avoid hammering the image server when the request fails.
 * Plan related buttons will be enabled only if the plan editor is selected.
 * Renamed "Show Owned Skillbooks" menu option to "Owned Skillbooks".
 * Corrected the base broker fee and transaction tax as described in the Citadel 1.0 patch notes.
 * Updated the crash report window.
 * Updated Help menu.
 * Updated links in the About window.
 * Several improvements have been done to the codebase to make EVEMon a better application.

----

#### Release 2.2.3
##### Contributors for this version

 * Jimi 

##### Bug Fixes

 * Fixed a bug for the custom message box check box handling.
 
##### Enhancements
 
 * Added support for the new distribution channel. 

----
For older releases visit the [wiki page](https://bitbucket.org/EVEMonDevTeam/evemon/wiki/NewFeatures)
