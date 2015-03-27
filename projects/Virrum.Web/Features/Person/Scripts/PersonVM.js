define([
    'knockout',
    'deco/qvc',
    'Shared/globalEvents'
], function (
    ko,
    qvc,
    proclaim
) {
    return function PersonVM(model, when) {
        var self = this;

        this.persons = ko.observable(model.person);
        this.name = ko.observable(model.name);
        this.id = ko.observable(model.id);
        this.url = "#/PersonDetails/" + self.id();


        //this.jobpositions = ko.observableArray(model.jobPositions.map(function (job) {
        //    return {
        //        position: job.position
        //    }
        //}));

        this.positions = model.jobPositions.map(function(job) {
            return job.position}).join(' / ');

        this.savePerson = qvc.createCommand("SavePerson", {
            id: self.id,
            name: self.name
        }).success(function () {
            // proclaim ??
            document.location = self.url;
        });

        this.deletePerson = qvc.createCommand("DeletePerson", {
            id: ko.computed(function () { return self.id(); })
        }).success(function () {
            // proclaim ??
            document.location = "/#";
        });

        init: {
            //
        }

    };
});