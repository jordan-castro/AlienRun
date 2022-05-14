using Godot;
using System;
using System.Threading.Tasks;

namespace Enviroment;

public class Launcher : StaticBody2D
{
    private Sprite idle;
    private Sprite launched;
    private RayCast2D ray;

    private bool isLaunched = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        idle = GetNode<Sprite>("Idle");
        launched = GetNode<Sprite>("Launched");
        ray = GetNode<RayCast2D>("Ray");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        UpdateState();

        // Check player is collding
        if (ray.IsColliding() && ray.GetCollider() is Player.Base)
        {
            var player = ray.GetCollider() as Player.Base;
            // Launcher should jump player quite high
            player.JumpForce -= 100;
            player.Jump();
            // Reset jump force
            player.JumpForce += 100;

            
            isLaunched = true;
        }
        if (isLaunched)
        {
            UnLaunch();
        }
    }

    // Change from idle to launched sprite and vice versa.
    private void UpdateState()
    {
        if (isLaunched)
        {
            launched.Visible = true;
            idle.Visible = false;
        }
        else
        {
            launched.Visible = false;
            idle.Visible = true;
        }
    }

    // After a moment, the launcher will return back to being idle and launchable again.
    private async void UnLaunch()
    {
        await Task.Delay(400);
        isLaunched = false;
    }
}