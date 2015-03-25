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

        this.name = ko.observable(model.name);
        this.id = ko.observable(model.id);
        this.url = "#/PersonDetails/" + self.id();
        console.log("id:");
        console.log(self.id());
        console.log(self.url);

        this.savePerson = qvc.createCommand("SavePerson", {
            id: self.id,
            name: self.name
        }).success(function () {
            // proclaim ??
            console.log("hei");
            console.log(self.url);
            document.location = self.url;
        });

        this.deletePerson = qvc.createCommand("DeletePerson", {
            id: ko.computed(function () { return self.id(); })
        }).success(function () {
            // proclaim ??
            console.log("hei");
            console.log(self.url);
            document.location = "/#";
        });

        init: {
            //
        }

    };
});