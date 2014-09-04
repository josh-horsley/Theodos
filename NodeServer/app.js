var io = require('socket.io').listen(5000, { log: false }),
    robot = require('./robot.js'),
    ultrasonic = require('./ultrasonic.js'),
    motor = require('./motor.js'),
    explorer = require('./explorer.js');

io.sockets.on('connection', function (socket) {
    var hardware = robot.create({
        'socket': socket,
        'sensor': ultrasonic,
        'motor': motor
    });

    hardware.runEvery(0.1).boot(explorer);
});