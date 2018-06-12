module.exports = function (socket) {
    var io = socket,
        tilt = { x: 0, y: 0, z: 0 };

    //Returns the distance it currently has and makes a call to get an update on the distance
    this.GetTilt = function () {
        io.emit("GetTilt", "");
        return tilt;
    };

    //Listens to 'distance' being returned from Unity
    io.on('tilt', function (data) {
        tilt = data;
    });
};