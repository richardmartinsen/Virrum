define(['knockout', 'moment'], function (ko, moment) {
    ko.bindingHandlers.datepicker = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            //initialize datepicker with some optional options
            var options = allBindingsAccessor().datepickerOptions;
            var dp = $(element).datepicker(options);
            dp.on("changeDate", function (ev) {
                var observable = valueAccessor();
                observable(ev.date);
            });
            if (options.clearBtn) {
                dp.on("show", function (ev) {
                    $(".datepicker").find('.clear').css('display', 'table-cell');
                });
            }

            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                $(element).datepicker("remove");
            });
        },
        update: function (element, valueAccessor, allBindingsAccessor) {
            var value = ko.utils.unwrapObservable(valueAccessor());
            $(element).datepicker('setValue', value);

            var options = allBindingsAccessor().datepickerOptions;

            var format = options && options.formatForMoment || "DD-MM-YYYY";

            var formatted = value != null ? moment(value).format(format) : null;
            $(element).datepicker('update', formatted);
        }
    };

    // example: <input type="text" data-bind="forceRange: {value: angle, minValue: 0, maxValue: 360}">
    ko.bindingHandlers['forceRange'] = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            var underlyingObservable = valueAccessor().value;
            var interceptor = ko.computed({
                read: underlyingObservable,
                write: function (newValue) {
                    var minValue = ko.unwrap(valueAccessor().minValue),
                        maxValue = ko.unwrap(valueAccessor().maxValue);

                    var current = underlyingObservable(),
                        valueToWrite = isNaN(newValue) ? 0 : parseFloat(+newValue);

                    if (valueToWrite < minValue) {
                        valueToWrite = minValue;
                    }

                    if (valueToWrite > maxValue) {
                        valueToWrite = maxValue;
                    }


                    //only write if it changed
                    if (valueToWrite !== current) {
                        underlyingObservable(valueToWrite);
                    } else {
                        //if the rounded value is the same as it was, but a different value was written, force a notification so the current field is updated to the rounded value
                        if (newValue !== current) {
                            underlyingObservable.valueHasMutated();
                        }
                    }
                }
            });
            ko.bindingHandlers.value.init(element, function () { return interceptor; }, allBindingsAccessor);
        },
        update: function (element, valueAccessor, allBindingsAccessor) {
            ko.bindingHandlers.value.update(element, function () { return valueAccessor().value; }, allBindingsAccessor);
        }
    };

    ko.bindingHandlers.showModal = {
        init: function (element, valueAccessor, allBindings) {
            $(element).modal({ backdrop: true, keyboard: true, show: false });

            var onHidden = allBindings.get('onModalHidden');
            if (onHidden) {
                $(element).on('hidden.bs.modal', function () {
                    onHidden();
                });
            }
        },
        update: function (element, valueAccessor) {
            var value = valueAccessor();
            if (ko.utils.unwrapObservable(value)) {
                $(element).modal('show');
                setTimeout(function () {
                    $("input:first", element).focus();
                }, 200);
            } else {
                $(element).modal('hide');
            }
        }
    };

    ko.bindingHandlers['highlightWhenActive'] = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel, context) {

            var active = ko.unwrap(valueAccessor());

            var selected = false;

            function updateHighlighting() {
                var currentLocation = document.location.hash;
                var myLocation = element.getElementsByTagName('a')[0].getAttribute("href");
                selected = currentLocation == myLocation;

                ko.bindingHandlers.css.update(element, newValueAccessor, allBindingsAccessor, viewModel, context);
            }

            function newValueAccessor() {
                return {
                    active: selected
                };
            };

            updateHighlighting();

            window.addEventListener('hashchange', updateHighlighting, false);

            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                window.removeEventListener('hashchange', updateHighlighting);
            });
        }
    };

    ko.bindingHandlers.timeValue = {
        init: function (element, valueAccessor) {
            $(element).change(function () {
                if (this.val() != valueAccessor()) {
                    this.val(valueAccessor());
                }
            });
        },
        update: function (element, valueAccessor) {
            var value = valueAccessor();
            var date = moment(value());
            var strDate = date.format('HH:mm');
            $(element).val(strDate);
        }
    };

    ko.bindingHandlers['stringFormat'] = {
        update: function (element, valueAccessor) {
            var accessor = valueAccessor();
            var text = ko.unwrap(accessor.text);
            var args = ko.unwrap(accessor.args);
            element.textContent = stringFormat(text, args);
        }
    };

    function stringFormat(str, arr) {
        return str.replace(/\{([0-9]+)\}/g,
            function (_, index) {
                return ko.unwrap(arr[index]);
            });
    }

    ko.bindingHandlers.textLength = {
        update: function (element, valueAccessor) {

            var options = ko.unwrap(valueAccessor());
            var text = ko.unwrap(options.text) || "";
            var maxLength = ko.unwrap(options.max);
            var belowMax = ko.unwrap(options.belowMax);
            var aboveMax = ko.unwrap(options.aboveMax);

            var textArgs = text.length <= maxLength ? {
                text: belowMax,
                args: [text.length, maxLength]
            } : {
                text: aboveMax,
                args: [text.length - maxLength]
            };

            ko.bindingHandlers.stringFormat.update(element, function () { return textArgs; });

            var color = text.length <= maxLength ?
                'hsl(26, ' + minmax(0, text.length / maxLength * 100, 100) + '%, ' + minmax(50, 50 + (1 - text.length / maxLength) * 30, 80) + '%)' :
                'hsl(0, 50%, 50%)';

            ko.bindingHandlers.style.update(element, function () { return { color: color }; });
        }
    };

    function minmax(min, value, max) {
        return Math.min(Math.max(min, value), max);
    }

    ko.bindingHandlers.reverseOrder = {
        init: function (element, valueAccessor, allBindings, viewModel, context) {
            var shouldReverse = valueAccessor();

            if (ko.isObservable(shouldReverse)) {
                shouldReverse.previousValue = shouldReverse();
            }

            if (ko.unwrap(shouldReverse)) {
                Array.prototype.slice.call(element.childNodes).reverse().forEach(function (el) {
                    element.appendChild(el);
                });
            }
        },
        update: function (element, valueAccessor, allBindings, viewModel, context) {
            var value = valueAccessor();

            if (ko.isObservable(value) && value() != value.previousValue) {

                value.previousValue = value();

                Array.prototype.slice.call(element.childNodes).reverse().forEach(function (el) {
                    element.appendChild(el);
                });
            }
        }
    };

    ko.bindingHandlers.readonlyform = {
        init: function (element, valueAccessor, allBindings, viewModel) {
            var value = valueAccessor();
            if (!value) {
                return;
            }
            var elementTypes = ['input', 'select', 'button', 'textarea'];
            elementTypes.forEach(function (elementType) {
                var elements = element.getElementsByTagName(elementType);
                for (var i = 0; i < elements.length; i++) {
                    elements[i].setAttribute('disabled', true);
                    if (elements[i].style) {
                        elements[i].style.cssText = 'cursor: default';
                    }
                }
            });
        }
    };

    //Makes the element clickable
    //when clicked, the observable gets the value
    //when the value matches the observable the class chosen is added to the element
    //cleareble will clear the observable if the currently chosen element is clicked again
    //
    //usage:    <div data-bind="chosen: dinnerPlan, value: 'pasta', clearable:true" class=chosen/>
    //          <div data-bind="chosen: dinnerPlan, value: 'pizza', clearable:true"/>
    //          <div data-bind="chosen: dinnerPlan, value: 'fårikål', clearable:true"/>
    //          <div data-bind="chosen: dinnerPlan, value: 'taco', clearable:true" />
    //          <div data-bind="chosen: dinnerPlan, value: 'sushi', clearable:true"/>
    ko.bindingHandlers.chosen = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel, context) {
            var bindings = allBindingsAccessor(),
                value = valueAccessor(),
                name = ko.unwrap(bindings.value),
                clearable = ko.unwrap(bindings.clearable),
                setChosen = function (e) {
                    if (value() == name && clearable) {
                        value(null);
                    } else {
                        value(name);
                    }
                };


            ko.bindingHandlers.click.init(element, function () { return setChosen; }, allBindingsAccessor, viewModel, context);
        },
        update: function (element, valueAccessor, allBindingsAccessor, viewModel, context) {
            var bindings = allBindingsAccessor(),
                value = valueAccessor(),
                name = ko.utils.unwrapObservable(bindings.value),
                isChosen = { chosen: value() === name };

            ko.bindingHandlers.css.update(element, function () { return isChosen; }, allBindingsAccessor, viewModel, context);
        }
    };
    //Makes the element clickable
    //when clicked, the observable gets the value
    //when the value matches the observable the class chosen is added to the element
    //cleareble will clear the observable if the currently chosen element is clicked again
    //
    //usage:    <div data-bind="tab: dinnerPlan, name: 'pasta', clearable:true" class="active"/>
    //          <div data-bind="tab: dinnerPlan, name: 'pizza', clearable:true"/>
    //          <div data-bind="tab: dinnerPlan, name: 'fårikål', clearable:true"/>
    //          <div data-bind="tab: dinnerPlan, name: 'taco', clearable:true" />
    //          <div data-bind="tab: dinnerPlan, name: 'sushi', clearable:true"/>
    ko.bindingHandlers.tab = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel, context) {
            var bindings = allBindingsAccessor(),
                value = valueAccessor(),
                name = ko.unwrap(bindings.name),
                setChosen = function (e) {
                    value(name);
                };


            ko.bindingHandlers.click.init(element, function () { return setChosen; }, allBindingsAccessor, viewModel, context);
        },
        update: function (element, valueAccessor, allBindingsAccessor, viewModel, context) {
            var bindings = allBindingsAccessor(),
                value = valueAccessor(),
                name = ko.utils.unwrapObservable(bindings.name),
                isActive = { active: value() == name };

            ko.bindingHandlers.css.update(element, function () { return isActive; }, allBindingsAccessor, viewModel, context);
        }
    };

    //Shows the content of the element if the observable matches the value of the element
    //
    //usage:    <div data-bind="ifChosen: dinnerPlan, value: 'pasta'"/>
    //          <div data-bind="ifChosen: dinnerPlan, value: 'pizza'"/>
    //          <div data-bind="ifChosen: dinnerPlan, value: 'fårikål'"/>
    //          <div data-bind="ifChosen: dinnerPlan, value: 'taco'" />
    //          <div data-bind="ifChosen: dinnerPlan, value: 'sushi'"/>
    //          <div data-bind="ifChosen: dinnerPlan, value: ['sushi', 'taco', 'pizza']"/>
    ko.bindingHandlers.ifChosen = {
        update: function (element, valueAccessor, allBindingsAccessor, viewModel, context) {
            var bindings = allBindingsAccessor(),
                value = ko.unwrap(valueAccessor()),
                name = ko.unwrap(bindings.value),
                isChosen;
            if (Array.isArray(name)) {
                isChosen = function () { return name.indexOf(value) >= 0; };
            } else {
                isChosen = function () { return value == name; };
            }

            ko.bindingHandlers.visible.update(element, isChosen, allBindingsAccessor, viewModel, context);
        }
    };


    /*

    toggles the boolean value of the provided observable every time the element is clicked.
    sets the attribute active to true if the observable is true



    usage: <div data-bind="toggleActive: dinnerPlan" />

    */
    ko.bindingHandlers.toggleActive = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel, context) {
            var value = valueAccessor(),
                toggle = function (e) {
                    value(!value());
                };

            ko.bindingHandlers.click.init(element, function () { return toggle; }, allBindingsAccessor, viewModel, context);
        },
        update: function (element, valueAccessor, allBindingsAccessor, viewModel, context) {
            var value = valueAccessor(),
                isActive = { active: value() };
            if (element.namespaceURI == "http://www.w3.org/2000/svg") {
                ko.bindingHandlers.attr.update(element, function () { return isActive; }, allBindingsAccessor, viewModel, context);
            } else {
                ko.bindingHandlers.css.update(element, function () { return isActive; }, allBindingsAccessor, viewModel, context);
            }
        }
    };
});