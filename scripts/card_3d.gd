@tool
extends Node3D

var CARDS_COLS: int = 13
var CARDS_ROWS: int = 4

var card_width = 1.0/CARDS_COLS
var card_height = 1.0/CARDS_ROWS

var current_card_index: int = 0

@onready var mesh_instance_3d: MeshInstance3D = $CardFront

@export_range(0, 51) var card_index: int :
		set(value):
			current_card_index = value
			if !mesh_instance_3d:
				return
			var material = mesh_instance_3d.get_active_material(0) as StandardMaterial3D
	
			var offset_x = float(current_card_index % CARDS_COLS) * card_width
			var offest_y = float(current_card_index / CARDS_COLS) * card_height
			
			var offset = Vector3(offset_x,offest_y,0)
			var scale = Vector3(card_width,card_height,0)
			print(offset)
			material.uv1_scale = scale
			material.uv1_offset = offset
		get:
			return current_card_index
		



# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
