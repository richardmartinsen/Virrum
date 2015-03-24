define([
    'knockout',
    'deco/qvc',
    'Shared/globalEvents',
    'Person/Scripts/Person'
], function (
    ko,
    qvc,
    proclaim,
    Person
) {
    return function PersonListVM(model, when) {
        var self = this;
        console.log(model);
        this.searchtxt = ko.observable("");
        console.log("stestef");
        this.persons = ko.observableArray(model.persons.map(function (person) {
            console.log(person);
            return new Person(person, when);
        }));

        this.filteredPersons = ko.computed(function () {
            return self.persons().filter(function (person) {
                return person.name.toLowerCase().indexOf(self.searchtxt().toLowerCase()) != -1;
            });
        });

        this.selectPerson = function (user) {
            proclaim.selectedUserHasChanged(user.id);
            //document.location.href = "#/Users/UserDetails?userId=" + user.id;
            document.location.href = "/PersonDetails/" + user.id;
        };

        init: {
            //
        }

    };
});