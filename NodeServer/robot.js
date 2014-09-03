module.exports = function () {

    var r = this; //hold on to an instance of robot
    this.s; //seconds for the setTimeout call in the recall function
    this.f; //function to run every (n) seconds
    this.custom = []; //array to hold custom functions

    //Sets the time delay (in seconds)
    this.every = function (seconds) {
        r = this;
        r.s = seconds * 1000;
        return r;
    };

    //Sets the delegate and calls the recursive 'recall' function
    this.run = function (func) {
        r.f = func;
        r.recall();
    };

    //executes a timeout for a given (n) seconds, then calls the delegate and the run function
    this.recall = function () {
        setTimeout(function () {
            r.f();
            r.run(r.f);
        }, r.s);
    };
};
