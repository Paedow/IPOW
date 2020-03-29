extends Node

var mesh;
var t = 0;
var red;
var blue;

# Called when the node enters the scene tree for the first time.
func _ready():
	mesh = get_node("Mesh");
	red = get_node("Mesh/Red");
	blue = get_node("Mesh/Blue");


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	t += delta;
	var h = sin(t) * .05;
	mesh.translation.y = h;

func red():
	blue.visible = false;
	red.visible = true;
	
func blue():
	blue.visible = true;
	red.visible = false;
