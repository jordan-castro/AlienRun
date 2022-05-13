using Godot;
using System;

public class ClimbShell : Area2D
{
    public void On_ClimbShell_body_entered(Node body)
    {
        if (body is Player.Base)
        {
            // Set climbing to true
            (body as Player.Base).IsClimbing = true;
        }
    }

    public void On_ClimbShell_body_exited(Node body)
    {
        if (body is Player.Base)
        {
            // Set climbing to false
            (body as Player.Base).IsClimbing = false;
        }
    }
}
