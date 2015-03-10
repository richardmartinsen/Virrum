define([
    'knockout'
], function(
    ko
) {
    return function DialogContentVM() {
        var self = this;

        this.text = ko.observable("Denne teksten er observable");

        this.close = null;

        this.accept = function() {
            self.close();
        };

    };

});