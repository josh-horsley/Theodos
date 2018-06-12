var io = require('socket.io').listen(5000, { log: false }),
    theodos = require('./theodos'),
    ultrasonic = require('./components/ultrasonic'),
    tilt = require('./components/tilt'),
    motor = require('./components/motor'),
    explorer = require('./software/mars-explorer');

io.sockets.on('connection', function (socket) {
    var robot = theodos.create({
        'socket': socket,
        'sensor': ultrasonic,
        'motor': motor,
        'tilt': tilt
    });

    robot.runEvery(0.1).boot(explorer);
});
console.log('waiting for connection from Unity...');