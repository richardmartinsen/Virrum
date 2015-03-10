define([
    'knockout',
    'moment',
    'services/modalDialog',
    'Settings/Toolbox/Scripts/DialogContentVM'
], function (
    ko,
    moment,
    modalDialog,
    DialogContentVM
) {

    return function CustomBindingsVM(model, when) {
        var self = this;

        this.x = ko.observable(0);
        this.y = ko.observable(0);

        this.showHide = ko.observable(true);

        this.scale = ko.observable(1);

        this.text = ko.observable("text");
        this.number = ko.observable(123);

        this.dinnerPlansToday = ko.observable("taco");
        this.dinnerPlansTomorrow = ko.observable("");
        this.activeTab = ko.observable("home");

        this.active = ko.observable(false);

        this.aFewMinutesFromNow = moment().add(10, 'minutes');

        this.longText = ko.observable("");

        
        this.date = ko.observable(moment());
        
        this.dialogContent = new DialogContentVM();

        this.showModalDialog = function() {
            var dialog = modalDialog.show('testDialogTemplate', self.dialogContent);
            self.dialogContent.close = dialog.close;
        };
        

    };
    

});