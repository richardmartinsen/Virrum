define([
    'knockout',
    'deco/qvc',
    'Shared/globalEvents'
], function (
    ko,
    qvc,
    proclaim
) {
    return function CreatePersonVM(model, when) {
        var self = this;

        this.name = ko.observable();
        this.url = "/#/index";

        this.createPerson = qvc.createCommand("CreatePerson", {
            name: self.name
        }).success(function () {
            proclaim.selectedUserHasChanged(undefined);
            document.location = self.url;
        });

        init: {
            //
        }

    };
});