define(['knockout', 'deco/events'], function(ko, when) {

    var _dialogVM = null;
    _closeCallback = null;

    function openDialog(template, viewmodel, shouldBeWide) {

        _dialogVM.open(template, viewmodel, shouldBeWide);

        return {
            close: closeDialog,
            onClose: function(closeCallback) {
                _closeCallback = closeCallback;
            }
        };
    }

    function openWideDialog(template, viewmodel) {
        return openDialog(template, viewmodel, true);
    }

    function openSlimDialog(template, viewmodel) {
        return openDialog(template, viewmodel, false);
    }
    
    function closeDialog() {
        _dialogVM.close();
    }
    
    function setDialogVM(viewmodel) {
        _dialogVM = viewmodel;
    }
    
    function onDialogClosed() {
        if (_closeCallback) {
            _closeCallback();
        }
        _closeCallback = null;
    }

    when.thePageHasChanged(closeDialog);

    return {
        show: openSlimDialog,
        showWide: openWideDialog,
        registerModalDialog: setDialogVM,
        onClosed: onDialogClosed,
        close: closeDialog
    };
});