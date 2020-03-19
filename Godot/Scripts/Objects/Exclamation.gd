extends Node

var mesh;
var t = 0;

# Called when the node enters the scene tree for the first time.
func _ready():
	mesh = get_node("MeshInstance");

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	t += delta;
	var h = sin(t) * .1;
	mesh.translation.y = 1.5 + h;
	mesh.rotate(Vector3.UP, delta);
	
