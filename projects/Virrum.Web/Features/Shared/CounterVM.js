define([
    'knockout'
], function (ko) {
    return function CounterVM(model) {
        var self = this;

        this.value = ko.isObservable(model.value) ? model.value : ko.observable(model.value || 0).extend({ dirty: false });

        this.minValue = ko.isObservable(model.minValue) ? model.minValue : ko.observable(model.minValue);

        this.maxValue = ko.isObservable(model.maxValue) ? model.maxValue : ko.observable(model.maxValue);

        this.increment = function () {
            self.value(parseInt(self.value()) + 1);
        }

        this.decrement = function () {
            self.value(parseInt(self.value()) - 1);
        }

        this.isMaxValue = ko.computed(function () {
            return self.value() >= self.maxValue();
        });

        this.isMinValue = ko.computed(function () {
            return self.value() <= self.minValue();
        });
    }
});