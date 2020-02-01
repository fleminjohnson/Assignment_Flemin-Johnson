# SnakeGame

This is a snake game made in Unity.There are a main menu scene and Snake game scene. 
Main Menu contains Start Game for starting Game and High Score which store High Score. Score is stored in Local Computer by playerpref functionality of Unity
In Snake Game snake grows by eating Fruits whose properties are termed by Food.csv file.

In this game, you can manually set the properties of food items. We can define our food item by color and points gained while eating it. You can define as much as food items as you want in the 'Food.csv' file under Assets/CSVData. The CSV file is read by the script called CSVReader.

Color of food Items are mapped into integers as follows:
<br/>1 = Red
2 = Magenta
3 = Black
4 = Yellow
5 = Green
6 = Cyan
7 = Blue


