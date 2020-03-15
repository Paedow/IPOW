extends Spatial


var front;
var side;

# Called when the node enters the scene tree for the first time.
func _ready():
	front = get_node("Front").get_surface_material(0).duplicate();
	side = get_node("Left").get_surface_material(0).duplicate();
	
	get_node("Front").set_surface_material(0, front);
	get_node("Back").set_surface_material(0, front);
	get_node("Left").set_surface_material(0, side);
	get_node("Right").set_surface_material(0, side);

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	var sizeX = self.scale.x;
	var sizeY = self.scale.z;
	
	front.set_shader_param("SurfaceSize", Vector2(sizeX,4));
	side.set_shader_param("SurfaceSize", Vector2(sizeY,4));
