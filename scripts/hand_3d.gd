@tool extends Node3D

var _card_space_z:float = 0.001
@export_range(0.001,0.1,0.001) var card_space_z:float:
	set(value):
		_card_space_z = value
		_layout_cards()
	get:
		return _card_space_z

var _card_space_x:float = 0.2
@export_range(0.1,2.0,0.01) var card_space_x:float:
	set(value):
		_card_space_x = value;
		_layout_cards()
	get:
		return _card_space_x

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	child_entered_tree.connect(_on_child_entered_tree)
	_layout_cards()

func _on_child_entered_tree(node: Node):
	print('calculating position')
	_layout_cards()
	
func _layout_cards():
	var children = get_children()
	var hand_width = card_space_x * float(children.size())
	print('hand width', hand_width)
	var half_hand_width = hand_width / 2.0
	for child_index in children.size():
		var child = children[child_index]
		if child is Card3D:
			child.position.z = child_index * card_space_z
			child.position.x = position.x + child_index * card_space_x - half_hand_width

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
