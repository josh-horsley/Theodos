module.exports = function (socket) {
    var io = socket,
        busy = false,
        stopRequested = false,
        moving = false,
        turning = false,
        motor = this,
        currentTask = '',
        queue = [];

    //Call to send a move request to Unity.  Takes 'forward' or 'backward' as a parameter
    this.move = function (direction) {
        motor.whenAvailable(direction, function () {
            motor.checkForStop(direction);
            motor.setBusy();

            if (stopRequested && moving) {
                io.emit('motor', 'stop');
                stopRequested = false;
                moving = false;
            } else if (!moving) {
                if (direction == 'forward') {
                    io.emit('motor', 'forward');
                    moving = true;
                } else if (direction == 'backward') {
                    io.emit('motor', 'backward');
                    moving = true;
                }
            }
        });
    };

    //Call to send a turn request to Unity.  Takes 'left' or 'right' as a parameter
    this.turn = function (direction) {
        motor.whenAvailable(direction, function () {
            motor.checkForStop(direction);
            motor.setBusy();

            if (stopRequested && turning) {
                io.emit('motor', 'stop');
                stopRequested = false;
                turning = false;
            } else if (!turning) {
                if (direction == 'left') {
                    io.emit('motor', 'left');
                    turning = true;
                } else if (direction == 'right') {
                    io.emit('motor', 'right');
                    turning = true;
                }
            }
        });
    };

    //Sets the stopRequested property to true
    this.stop = function () {
        stopRequested = true;
    };

    //checks if this is a new direction and, if so, sends a stop request
    this.checkForStop = function (direction) {
        if (direction != currentTask) {
            motor.stop();
        }
        currentTask = direction;
    }

    //artificial timeout, indicating the motor is busy
    this.setBusy = function () {
        setTimeout(function () {
            busy = false;
        }, 100);
    }

    //checks if the motor is busy and, if so, stores the task in the queue until it's not busy
    this.whenAvailable = function (taskName, task) {
        //check if there's a task in the queue first
        if (queue.length > 0 && !busy) {
            var queuedTask = queue.splice(0, 1)[0];
            busy = true;
            queuedTask.t();
        }

        var needsTask = queue.filter(function (element) {
            return element.name == taskName;
        });
        
        //if it's not busy, then run the current task
        if (!busy) {
            busy = true;
            task();
        } else if (needsTask.length == 0) {
            //currently busy, so store in the queue
            queue.push({ name: taskName, t: task});
        }
    };
};