# Bloons TD Tower Viewer

This project is an assignment for the course "Tool Development" at Howest DAE. It consists of two XAML pages that display information and statistics for all the towers in the game Bloons TD 6.

The project uses two repositories:
 - Local: The project reads a local JSON file and uses local images.
 - API: The project retrieves data from an online database: https://statsnite.com/btd/api

# Main Page

On this page, you can see an overview of all towers. You can view details of a certain tower by selecting it and pressing a button at the bottom of the window. There is also a button for selecting the type of displayed towers. You can also change the repository by clicking the button on the right side.

# Detail Page

On this page, you can view details such as the name, description, stats, costs (based on difficulty level), and other info about the selected tower.

At the bottom of the page, there is a list of all upgrades. You can only see one upgrade path at a time, so use the "previous" and "next" buttons to switch between them.

Once you select an upgrade, it will replace the base tower and the displayed info will be updated accordingly.
You can reset the displayed info by clicking the "restart" button at the top.

# Important Info

Download the latest release of this project and compile/run in visual studio. 

To switch between repositories:
1. Start the project.
2. Click on the appropriate button.

