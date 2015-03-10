define([
], function(
) {

    function get(key) {
        return localStorage.getItem(key);
    }

    function set(key, value) {
        localStorage.setItem(key, value);
    }

    return {
        get: get,
        set: set
    }

});