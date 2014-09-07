module.exports = function (components) {
    var io = components.socket,
        motor = new components.motor(io),
        sensor = new components.sensor(io),
        tilt = new components.tilt(io);

    console.log('[[STARTING MARS EXPLORER]]');
    var chosen = false,
        lastChoice = '';

    this.run = function () {
        var distance = sensor.GetDistance();
        var tiltVector = tilt.GetTilt();
        var angle = GetAngle(tiltVector.z);

        if (distance == -1 || angle < 20 && distance > 5) {
            motor.move("forward");

            lastChoice = "";
        } else if (distance < 2) {
            motor.move("backward");

            lastChoice = "";
            setTimeout(function () {
                motor.stop();
            }, 1000);
        } else if (!chosen) {
            var choiceDirection = Math.floor((Math.random() * 2) + 1);
            if (lastChoice != "") {
                motor.turn(lastChoice);
            } else if (choiceDirection == 1) {
                motor.turn("right");
                lastChoice = "right";
            } else {
                motor.turn("left");
                lastChoice = "left";
            }
            chosen = true;
            setTimeout(function () {
                chosen = false;
            }, 2000);
        }
    };
};

function GetAngle(z) {
    var angle = z;
    if (z > 180) {
        angle = 360 - z;
    }
    return angle;
}