define([
    'knockout',
    'deco/qvc',
    'Shared/globalEvents'
], function (
    ko,
    qvc,
    event
) {
    return function Person(model, when) {
        var self = this;
        var proclaim = event;

        this.id = model.id;
        this.name = model.name;

        this.isSelected = ko.observable();

        this.url = '#!UserDetails/' + self.id;
        this.selectUser = function() {
            proclaim.selectedUserHasChanged(self.id);
            document.location.hash = self.url;
        };

        init: {
            self.isSelected(self.url == document.location.hash);
            when(event.selectedUserHasChanged, function (userId) {
                self.isSelected(userId == self.id);
            });
        }

    };
});