using Godot;
using System;

public class LoadLevelDialogue : FileDialog
{
    public override void _Ready()
    {
        this.Show();

        this.ToSignal(this, "file_selected").OnCompleted(()=>{
            
        });
    }
}
