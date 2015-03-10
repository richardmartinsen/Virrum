define([
    'knockout'
], function (
    ko
) {
    ko.extenders.dirty = function (target, startDirty) {
        var cleanValue = ko.observable(target());
        var dirtyOverride = ko.observable(ko.utils.unwrapObservable(startDirty));

        target.isDirty = ko.computed(function () {
            return dirtyOverride() || target() !== cleanValue();
        });

        target.reset = function () {
            target(cleanValue());
        };

        target.markClean = function () {
            cleanValue(target());
            dirtyOverride(false);
        };

        target.markDirty = function () {
            dirtyOverride(true);
        };

        return target;
    };

    /**
	usage:

	this.observable.throttledSubscribe(500, function(value){
		doAjax(value);
	});


	this.onload = function(){
		//does not cause throttledSubscribe to get called
		this.observable.updateWithoutNotifyingSubscriber(newValue);
	};

    this.setManually = function(newValue){
		//does not cause throttledSubscribe to get called the next time the observable is set
	    this.observable.disregardNextUpdate();
        this.observable(newValue);
        //from now on throttledSubscribe will be called
    }
	*/

    ko.subscribable.fn.throttledSubscribe = function (throttleTime, subscriber) {
        if (!subscriber) {
            subscriber = throttleTime;
            throttleTime = 1;
        }

        var target = this;

        var updating = false;
        target.disregardNextUpdate = function () {
            updating = true;
        };
        target.updateWithoutNotifyingSubscriber = function (value) {
            if (target() === value) {
                return;
            }
            updating = true;
            target(value);
        };

        var throttledComputed = ko.computed(function () {
            return target();
        }).extend({ throttle: throttleTime });

        throttledComputed.subscribe(function (value) {
            if (updating == false) {
                subscriber(value);
            } else {
                updating = false;
            }
        });
    };

    ko.subscribable.fn.orderBy = function (comparator) {
        return this().slice(0).sort(function (a, b) {
            a = comparator(a);
            b = comparator(b);
            return a < b ? -1 : a > b ? 1 : 0;
        });
    }

    ko.subscribable.fn.first = function (comparator) {
        return this().filter(comparator)[0];
    };

    ko.subscribable.fn.some = function (comparator) {
        return this().some(comparator);
    };

    ko.observableArray.fn.trickle = function (list, chunk) {
        if (this.trickleInterval) {
            clearInterval(this.trickleInterval);
            this.trickleInterval = null;
        }
        this([]);
        var self = this;
        chunk = Math.min(chunk || 20, list.length);
        var offset = chunk;
        this.push.apply(this, list.slice(offset - chunk, offset));
        var intval = setInterval(function () {
            chunk = offset + chunk > list.length ? list.length - offset : chunk;
            offset += chunk;
            self.push.apply(self, list.slice(offset - chunk, offset));

            if (offset == list.length) {
                clearInterval(intval);
                self.trickleInterval = null;
            }
        }, 1);

        this.trickleInterval = intval;
    };

});