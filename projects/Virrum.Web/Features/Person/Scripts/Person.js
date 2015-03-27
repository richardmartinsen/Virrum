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

        //this.url = '#!Users/UserDetails?userId=' + self.id;
        this.url = '/PersonDetails/' + self.id;
        this.selectPerson = function() {
            proclaim.selectedUserHasChanged(self.id);
            document.location.hash = self.url;
        };

        init: {
            self.isSelected(self.url == document.location.hash);
            when(event.selectedUserHasChanged, function (userId) {
                self.isSelected(userId == self.id);
                //console.log(self.isSelected());
                //console.log(self.url);
                //console.log(document.location.hash);
                //console.log(document.location.href);
                //console.log(userId);
                //console.log(self.id);
            });
        }

    };
});