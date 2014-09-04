Theodos
=======

Robot simulator using Node.js and Unity3D.  Can be used for creating and testing your robot AI in a virtual environment before hooking it up to your physical robot.  Credit to the guys at NetEase for the use of their socket.io - Unity project [UnitySocketIO](https://github.com/NetEase/UnitySocketIO).


Version
----

0.1

Requirements
-----------

You will need a couple of things to get up and running:

* [Unity3D] - awesome game development software
* [node.js] - evented I/O for sending commands to your robot

Usage
--------------

```sh
var io = require('socket.io').listen(5000),
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
```

License
----

MIT

[Unity3D]:http://unity3d.com/
[node.js]:http://nodejs.org