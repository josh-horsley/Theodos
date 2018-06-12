module.exports = function (components) {
    var io = components.socket,
        motor = new components.motor(io),
        sensor = new components.sensor(io);

    console.log('[[STARTING ROBOT]]');
    var chosen = false,
        lastChoice = '',
        backupThreshold = 0,
        forwardThreshold = 0;

    this.run = function () {
        var distance = sensor.GetDistance()

        if (distance == -1 || distance > 4) {
            if (forwardThreshold > 200) {
                motor.turn("right");
            } else {
                motor.move("forward");
            }

            lastChoice = "";
            forwardThreshold++;
            backupThreshold = 0;
        } else if (distance < 1) {
            if (backupThreshold > 5) {
                motor.turn("right");
            } else {
                motor.move("backward");
            }

            lastChoice = "";
            backupThreshold++;
            forwardThreshold = 0;
            setTimeout(function () {
                motor.stop();
            }, 1000);
        } else if (!chosen) {
            backupThreshold = 0;
            forwardThreshold = 0;
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