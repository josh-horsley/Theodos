var io = require('socket.io').listen(5000, { log: false }),
    explorer = require('./explorer.js');

io.sockets.on('connection', function (socket) {
    var robot = new explorer(socket);
    robot.Run();
});