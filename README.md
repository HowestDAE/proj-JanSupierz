# Bloons TD Tower Viewer

This project is an assignment for the course "Tool Development" at Howest DAE. It consists of two XAML pages that display information and statistics for all the towers in the game Bloons TD 6.

The project uses two repositories:
 - Local: The project reads a local JSON file and uses local images.
 - API: The project retrieves data from an online database: https://statsnite.com/btd/api

I think it matches the Game Development profile because this game is enjoyable, and creating a viewer like this could be useful for a game company to showcase the game and attract potential players.

# Main Page

On this page, you can see an overview of all towers. 

![image](https://user-images.githubusercontent.com/114002276/230404818-63b3964b-cd0c-4b50-ad1c-4b872731d734.png)

- You can view details of a certain tower by selecting it and pressing a button at the bottom of the window. 
- There is also a button for selecting the type of displayed towers. 
- You can also change the repository by clicking the button on the right side.

# Detail Page


On this page, you can view details such as the name, description, stats, costs (based on difficulty level), and other info about the selected tower.

![image](https://user-images.githubusercontent.com/114002276/230405119-d4d3d706-a00d-4147-b065-bd8568d85f71.png)

At the bottom of the page, there is a list of all upgrades. You can only see one upgrade path at a time, so use the "previous" and "next" buttons to switch between them.

Once you select an upgrade, it will replace the base tower and the displayed info will be updated accordingly.
You can reset the displayed info by clicking the "restart" button at the top.

You can return to the overview page by clicking the "GO BACK" button.

# Important Info

Download the latest release of this project and compile/run in visual studio. 

To switch between repositories:
1. Start the project.
2. Click on the appropriate button.

