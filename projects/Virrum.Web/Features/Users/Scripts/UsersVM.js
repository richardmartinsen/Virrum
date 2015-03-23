define([
    'knockout',
    'deco/qvc',
    'Shared/globalEvents',
    'Users/Scripts/Person'
], function (
    ko,
    qvc,
    proclaim,
    Person
) {
    return function UsersVM(model, when) {
        var self = this;
        console.log(model);
        this.searchtxt = ko.observable("");
        //this.id = ko.observable(1);
        //this.name = ko.observable("asdfasdf").extend({ dirty: false });
        console.log("stestef");
        this.persons = ko.observableArray(model.users.map(function (person) {
            console.log(person);
            return new Person(person, when);
        }));

        this.filteredPersons = ko.computed(function () {
            return self.persons().filter(function (person) {
                return person.name.toLowerCase().indexOf(self.searchtxt().toLowerCase()) != -1;
            });
        });

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