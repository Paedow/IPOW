extends Spatial

const BUTTON_WHEEL = 4;
const camW = 3;

var lpos = Vector2.INF;
var pos= Vector2.INF;
var posPress = false;
var lrot = 0;
var rot = 0;
var rotPress = false;

var lheight = 3;
var height = 3;

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if(posPress):
		var dp = -(pos-lpos)/100;
		var shift = Vector3(dp.x,0,dp.y);
		translate(shift);
	if(rotPress):
		var dr = -(rot-lrot)/360;
		global_rotate(Vector3.UP, dr);
	lpos = pos;
	lrot = rot;
	
	if(height < 3):
		height = 3;
	
	global_translate(Vector3(0,height-lheight,0));
	lheight = height;
	
	get_node("Camera").transform = Transform(Vector3(1,0,0),Vector3(0,1,0),Vector3(0,0,1),Vector3(0,0,0));
	get_node("Camera").rotate_x(-atan(1.0 * height / camW));
	
	pass

func _input(event):
	if(event is InputEventMouseMotion):
		if(event.button_mask == BUTTON_RIGHT):
			pos = event.global_position;
		if(event.button_mask == BUTTON_WHEEL):
			rot = event.global_position.x;
		pass;
	
	if(event is InputEventMouseButton):
		if(event.button_mask == BUTTON_RIGHT):
			pos = event.global_position;
			lpos = event.global_position;
			posPress = true;
		if(event.button_mask == BUTTON_WHEEL):
			rot = event.global_position.x;
			lrot = event.global_position.x;
			rotPress = true;
		if(event.button_mask == 0):
			posPress = false;
			rotPress = false;
		if(event.button_mask == 16):
			height += 1;
		if(event.button_mask == 8):
			height -= 1;
		pass;
