var robot = require('./robot.js'),
    ultrasonic = require('./ultrasonic.js'),
    motor = require('./motor.js');

module.exports = function Explorer(socket) {
    var io = socket,
        r = new robot(),
        m = new motor(io),
        sensor = new ultrasonic(io);

    this.Run = function () {
        console.log('[[STARTING ROBOT]]');
        var chosen = false,
            lastChoice = '',
            backupThreshold = 0,
            forwardThreshold = 0;
        r.every(0.1).run(function () {
            var distance = sensor.GetDistance()

            if (distance == -1 || distance > 10) {
                if (forwardThreshold > 200) {
                    m.Turn("right");
                } else {
                    m.Move("forward");
                }

                lastChoice = "";
                forwardThreshold++;
                backupThreshold = 0;
            } else if (distance < 2) {
                if (backupThreshold > 5) {
                    m.Turn("right");
                } else {
                    m.Move("backward");
                }

                lastChoice = "";
                backupThreshold++;
                forwardThreshold = 0;
                setTimeout(function () {
                    m.Stop();
                }, 1000);
            } else if (!chosen) {
                backupThreshold = 0;
                forwardThreshold = 0;
                var choiceDirection = Math.floor((Math.random() * 2) + 1);
                if (lastChoice != "") {
                    m.Turn(lastChoice);
                } else if (choiceDirection == 1) {
                    m.Turn("right");
                    lastChoice = "right";
                } else {
                    m.Turn("left");
                    lastChoice = "left";
                }
                chosen = true;
                setTimeout(function () {
                    chosen = false;
                }, 2000);
            }
        });
    };
};