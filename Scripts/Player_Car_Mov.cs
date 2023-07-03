using Godot;
using System;

public class Player_Car_Mov : Node
{
    VehicleBody Player_car;
    float forward = 0f;
    float left = 0f;
    [Export]
    float Steering_multiplyer = 0.4f;
    [Export]
    float EngineForce_multiplyer = 100f;
    public override void _Ready()
    {
        Player_car = GetParent<VehicleBody>();
    }

    public override void _Process(float delta)
    {
        checkButtons();
    }

    public override void _PhysicsProcess(float delta)
    {
        control();
    }

    void checkButtons() {
        forward = Input.GetAxis("b_back", "b_forward");
        left = Input.GetAxis("b_right", "b_left");
    }

    void control() {
        Player_car.Steering = left * Steering_multiplyer;
        Player_car.EngineForce = forward * EngineForce_multiplyer;
    }
    

}
