extends ParallaxBackground


# Called when the node enters the scene tree for the first time.
func _ready():
	var parallax_background = ParallaxBackground.new()
	add_child(parallax_background)
	var parallax_layer_middle = ParallaxLayer.new()
	parallax_background.add_child(parallax_layer_middle)
	var parallax_layer_background_middle = ParallaxLayer.new()
	var parallax_layer_end = ParallaxLayer.new()
	parallax_background.add_child(parallax_layer_end)
	var parallax_layer_background_end = ParallaxLayer.new()
	
	var sprite_bg_middle = Sprite2D.new()
	sprite_bg_middle.texture = preload("res://night cities files/night cities files/backgrounds/neon-alley/layers/middle.png")
	parallax_layer_background_middle.add_child(sprite_bg_middle)
	parallax_layer_background_middle.scale = Vector2(5, 5)
	parallax_layer_background_middle.motion_mirroring = Vector2(1024, 0)
	parallax_background.add_child(parallax_layer_background_middle)
	
	var sprite_bg_end = Sprite2D.new()
	sprite_bg_end.texture = preload("res://night cities files/night cities files/backgrounds/neon-alley/layers/back.png")
	parallax_layer_background_end.add_child(sprite_bg_end)
	parallax_layer_background_middle.scale = Vector2(10, 10)
	parallax_layer_background_middle.motion_mirroring = Vector2(1980, 0)
	parallax_background.add_child(parallax_layer_background_end)
	
	


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
