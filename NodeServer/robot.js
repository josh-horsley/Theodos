exports.create = function (modules) {
    var robot = new Robot();
    return robot.addModules(modules);
};

function Robot () {

    var r = this; //hold on to an instance of robot
    this.s; //seconds for the setTimeout call in the recall function
    this.f; //function to run every (n) seconds
    this.m; //modules passed in to be used by the software

    //Sets the modules property
    this.addModules = function (modules) {
        m = modules;
        return r;
    };

    //Sets the time delay (in seconds)
    this.runEvery = function (seconds) {
        r.s = seconds * 1000;
        return r;
    };

    //Loads the software module
    this.boot = function (Software) {
        var software = new Software(m);

        this.run(function () {
            software.run();
        });
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
}
