extends PanelContainer


var health
var score


# Called when the node enters the scene tree for the first time.
func _ready():
    health = $VBoxContainer.get_node("Health")
    score = $VBoxContainer.get_node("Score")

    # Connect signals
    var _r = get_parent().get_parent().connect("health_changed", self, "change_health")
    _r = get_parent().get_parent().connect("score_changed", self, "change_score")


func change_health(new_health: int):
    change_data(new_health, health)


func change_score(new_score: int):
    change_data(new_score, score)


func change_data(new_data: int, control: Control):
    var box = control.get_node("Box")
    var label: Label = box.get_node("Label")
    var texture: TextureRect = box.get_node("Texture")

    label.text = str(new_data)
    # Make the texture "Jump"
    texture.rect_size = Vector2(23, 23)
    yield(get_tree().create_timer(0.5), "timeout")

    # Make the texture "Fall"
    texture.rect_size = Vector2(18, 18)
