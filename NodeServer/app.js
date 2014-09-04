var io = require('socket.io').listen(5000, { log: false }),
    theodos = require('theodos'),
    ultrasonic = require('components/ultrasonic'),
    motor = require('components/motor'),
    explorer = require('software/explorer');

io.sockets.on('connection', function (socket) {
    var robot = theodos.create({
        'socket': socket,
        'sensor': ultrasonic,
        'motor': motor
    });

    robot.runEvery(0.1).boot(explorer);
});