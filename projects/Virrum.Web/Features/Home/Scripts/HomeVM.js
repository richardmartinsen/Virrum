define([
    'knockout',
    'deco/qvc'
], function (
    ko,
    qvc
) {
    return function HomeVM(model) {
        var self = this;
        console.log(model);
        this.searchtxt = ko.observable("");
        //this.id = ko.observable(1);
        //this.name = ko.observable("asdfasdf").extend({ dirty: false });
        console.log("stestef");
        //this.getuser = qvc.createQuery("GetUser", {
        //    id: ko.observable(this.id)
        //}).result(function (result) {
        //    self.name(result.name);
        //});

        init: {
            //self.getuser();
        }

    };
});