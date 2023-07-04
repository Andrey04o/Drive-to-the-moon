using Godot;
using System;

public class Player_Car_Mov_v2 : Node
{
    RigidBody Player_car;
    float forward = 0f;
    float left = 0f;
    [Export]
    float Steering_multiplyer = 0.4f;
    [Export]
    float EngineForce_multiplyer = 100f;
    [Export]
    public float speed = 15f;
    public float speedRotation = 5f;
    Vector3 moveDir;
    RayCast rDown;
    Camera cPlayer;
    Vector3 v_PlayerPos;
    bool isRigidbody = false;
    Spatial DirMove;
    Vector3 lookForward;
    Vector3 inclineForward;
    //public bool rigidbody_mode = false;
    public override void _Ready()
    {
        Player_car = GetParent<RigidBody>();
        Player_car.CustomIntegrator = true;
        rDown = GetParent().GetNode<RayCast>("RayCastDown");
        cPlayer = GetParent().GetNode<Camera>("Camera");
        DirMove = GetParent().GetNode<Spatial>("DirMove");
    }

    public override void _Process(float delta)
    {
        lookForward = Player_car.GlobalTransform.basis.z;
        //GD.Print(rDown.IsColliding());
        checkButtons();
        CustomPhys(delta);
    }

    public override void _PhysicsProcess(float delta)
    {
        control();
    }

    void CustomPhys(float delta) {
        GD.Print(rDown.IsColliding());
        CheckIsFall();
        if (Player_car.CustomIntegrator == true) {
            Rotate(delta);
            Drive(delta);
        }
    }

    void Fall(float delta) {
        
        if (rDown.IsColliding() == false) {
            Player_car.CustomIntegrator = false;
            Player_car.Mode = RigidBody.ModeEnum.Rigid;
        }
        //Player_car.
    }

    void CheckIsFall() {
        if (isRigidbody == false) {
            if (rDown.IsColliding() == false) {
                isRigidbody = true;
                Player_car.CustomIntegrator = false;
                Player_car.Mode = RigidBody.ModeEnum.Rigid;
            }
        }
        if (rDown.IsColliding() == true) {
            isRigidbody = false;
            Player_car.CustomIntegrator = true;
            Player_car.Mode = RigidBody.ModeEnum.Kinematic;
        }
    }
    
    void Drive(float delta) {
        //Player_car.Rotation.Project(Vector3.Forward);
        //GD.Print(Player_car.Rotation.Project(Vector3.Forward));
        //moveDir = Player_car.GlobalTranslation.MoveToward(DirMove.GlobalTranslation, delta * speed);
        //GD.Print(Player_car.Translation.DirectionTo(Vector3.Forward));
        
        v_PlayerPos = Player_car.Translation;
        //v_PlayerPos.x += speed * delta;
        v_PlayerPos += lookForward * forward * speed * delta;
        Player_car.Translation = v_PlayerPos;
    }
    void Rotate(float delta) {
        Vector3 normal;
        Transform tr;
        //Basis basis = new Basis(normal, 0f);
        normal = rDown.GetCollisionNormal();
        GD.Print(inclineForward);
        inclineForward = normal.Cross(lookForward).Cross(normal);
        GD.Print(normal.SignedAngleTo(Player_car.Transform.basis.y, Vector3.Left));
        //normal.SignedAngleTo(Vector3.Up, Vector3.Up);
        //Player_car.Transform.basis.Quat().
        Player_car.RotateObjectLocal(normal, left * speedRotation * delta);
        tr = Player_car.Transform;
        GD.Print(tr.basis.Quat());
        //normal.
        //tr.basis = tr.basis.Rotated(-tr.basis.x, normal.AngleTo(tr.basis.y));
        
        GD.Print(normal.AngleTo(tr.basis.y));
        //normal.SignedAngleTo(rDown.CastTo,Vector3.Up);
        //Player_car.GlobalRotation = rDown.
        //Player_car.Rotate(Vector3.Left, normal.SignedAngleTo(Player_car.Transform.basis.y, Vector3.Up));
        //Player_car.RotateObjectLocal(Vector3.Left, normal.AngleTo(tr.basis.y) * delta);
        
        //Player_car.Rotation
        //tr.basis.y = normal;
        Player_car.Transform = tr;
        //tr = Player_car.Transform;
        //tr.basis = new Basis(normal, 0f);
        //Player_car.Transform = tr;
        //Player_car.rotation
        //Transform tsdf;
        //tsdf.basis.
        //Player_car.Transform.
        //GD.Print(Player_car.GlobalTransform.basis.z);
        
        //Player_
        //rDown.GetCollisionNormal;
        //Vector3 td;
        //td.Cro
    }

    void checkButtons() {
        forward = Input.GetAxis("b_back", "b_forward");
        left = Input.GetAxis("b_right", "b_left");
    }

    void control() {
        //Player_car.Steering = left * Steering_multiplyer;
        //Player_car.EngineForce = forward * EngineForce_multiplyer;
    }

}
