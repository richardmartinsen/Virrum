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
        console.log(model);

        this.name = ko.observable(model.name);
        this.id = ko.observable(model.id);
        this.url = "#/PersonDetails/" + self.id;

        this.saveCommand = qvc.createCommand("SavePerson", {
            id: self.id,
            name: self.name
        }).success(function () {
            // proclaim ??
            document.location = self.url;
        });

        init: {
            //
        }

    };
});