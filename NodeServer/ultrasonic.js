module.exports = function (socket) {
    var io = socket,
        distance = 5;

    //Returns the distance it currently has and makes a call to get an update on the distance
    this.GetDistance = function () {
        io.emit("GetDistance", "get it");
        return distance;
    };

    //Listens to 'distance' being returned from Unity
    io.on('distance', function (data) {
        distance = data;
    });
};