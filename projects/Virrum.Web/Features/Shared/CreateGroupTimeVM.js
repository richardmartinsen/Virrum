define([
    'knockout',
    'moment',
    'services/readable-range',
    'services/userSettingsService'
], function (
    ko,
    moment,
    rr,
    userSettingsService
) {
    return function CreateGroupTimeVM(dateObservable, startTime, endTime, readOnly) {
        var self = this;

        this.date = dateObservable;

        this.readOnly = readOnly || false;

        this.minDate = moment().toDate();

        this.momentDate = ko.computed(function () {
            return moment(self.date());
        });
        
        this.startDisplayTime = {
            time: ko.observable(startTime || "08:00").extend({ dirty: false }),
            error: ko.observable("")
        }

        this.endDisplayTime = {
            time: ko.observable(endTime || "12:00").extend({ dirty: false }),
            error: ko.observable("")
        }

        this.startTime = ko.computed(function () {
            var time = self.startDisplayTime.time().split(/[:.]/);
            if (time.length != 2) return undefined;
            var hour = time[0];
            var minute = time[1];
            return moment(self.momentDate()).hours(hour).minutes(minute).format("YYYY-MM-DD HH:mm");
        });

        this.endTime = ko.computed(function () {
            var time = self.endDisplayTime.time().split(/[:.]/);
            if (time.length != 2) return undefined;
            var hour = time[0];
            var minute = time[1];
            return moment(self.momentDate()).hours(hour).minutes(minute).format("YYYY-MM-DD HH:mm");
        });

        this.humanizedDuration = ko.computed(function () {
            var endDisplayTime = moment(self.startTime());
            var startDisplayTime = moment(self.endTime());
            return rr.getPreciseDiff(startDisplayTime, endDisplayTime);
        });

        this.startDisplayTime.time.subscribe(function () {
            self.validate();
        });

        this.endDisplayTime.time.subscribe(function () {
            self.validate();
        });

        this.validate = function () {
            var end = self.endDisplayTime.time;
            var start = self.startDisplayTime.time;
            if (start() != "" && !start().match(/^(([01]?\d)|(20|21|22|23))[:.][0-5]\d$/)) {
                self.startDisplayTime.error("formatError");
                return;
            }
            if (end() != "" && !end().match(/^(([01]?\d)|(20|21|22|23))[:.][0-5]\d$/)) {
                self.endDisplayTime.error("formatError");
                return;
            } else {
                self.startDisplayTime.error("");
                self.endDisplayTime.error("");
            }
            var startMomentWithDelay = moment(self.startTime()).add(5, 'minutes');
            if (startMomentWithDelay.isAfter(moment(self.endTime()))) {
                end(startMomentWithDelay.format('HH:mm'));
            }
        };

        this.canSave = ko.computed(function () {
            return self.startDisplayTime.error() == "" && self.endDisplayTime.error() == "";
        });
    };
});