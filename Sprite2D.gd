extends Sprite2D



func _ready():
	
		
	pass # Replace with function body.



func _process(delta):
	if Input.is_action_pressed("ui_left"):
		global_position.x -= 2+delta
	if Input.is_action_pressed("ui_right"):
		global_position.x += 2+delta
	if Input.is_action_pressed("ui_up"):
		global_position.y -= 2+delta
	if Input.is_action_pressed("ui_down"):
		global_position.y += 2+delta
		
	
	pass
