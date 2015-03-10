define([
    'knockout',
    'services/modalDialog'
], function(
    ko,
    modalDialog
) {

    return function ModalDialogVM(model, when) {
        var self = this;

        this.templateName = ko.observable();
        this.data = ko.observable();

        this.isOpen = ko.observable(false);
        this.shouldBeWide = ko.observable(false);

        this.onClose = function() {
            self.isOpen(false);
            modalDialog.onClosed();
        };

        this.open = function(name, data, shouldBeWide) {
            self.templateName(name);
            self.data(data);
            self.isOpen(true);
            self.shouldBeWide(shouldBeWide);
        };

        this.close = function () {
            self.isOpen(false);
            self.templateName(null);
            self.data(null);
            self.shouldBeWide(false);
        };

        init:{
            modalDialog.registerModalDialog(this);
        }

    };

});