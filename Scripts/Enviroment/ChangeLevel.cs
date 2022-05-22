using Godot;
using System;

namespace Enviroment;

public class ChangeLevel : Area2D
{
    public void _on_ChangeLevel_body_entered(Node body)
    {
        if (body is Player.Base)
        {
            int currentLevel = GetTree().CurrentScene.Name.Split("_")[1].ToInt();

            // Find out how many levels we have in the levels directory
            int numberOfLevels = 0;

            Directory dir = new Directory();
            if (dir.Open("res://Scenes/Levels") == Error.Ok)
            {
                dir.ListDirBegin();
                string fileName = dir.GetNext();
                while (fileName != "")
                {
                    if (fileName.Contains("Level_") && fileName.EndsWith(".tscn"))
                    {
                        numberOfLevels++;
                    }
                    fileName = dir.GetNext();
                }
            }

            if (numberOfLevels >= currentLevel + 1)
            {
                // Change to next level
                GetTree().ChangeScene("res://Scenes/Levels/Level_" + (currentLevel + 1).ToString() + ".tscn");
            }
        }
    }
}
