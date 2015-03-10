define([
    'knockout'
], function(
    ko
) {

    function getUrlObject(url) {
        var urlParts = url.split('?');
        var base = urlParts.shift();
        var path = base.split('/');
        var name = path[path.length - 1];
        var query = urlParts.join('?');

        return { base: base, query: query, name: name };
    }

    function getQueryParamObject(query) {
        var queryObject = Object.create(null);
        query.split('&').forEach(function (part) {
            if (part.length == 0) return;
            var parts = part.split('=');
            queryObject[parts.shift()] = decodeURIComponent(parts.join('='));
        });
        return queryObject;
    }


    function queryParamObjectToString(queryObject) {
        var query = [];
        for (var part in queryObject) {
            query.push(part + '=' + queryObject[part]);
        }
        return query.join('&');
    }

    function encodeQueryParamObject(paramObject) {
        var queryObject = Object.create(null);
        for (var key in paramObject) {
            var val = ko.unwrap(paramObject[key]);
            if (val || val === false) {
                queryObject[encodeURIComponent(key)] = encodeURIComponent(val);
            }
        }
        return queryObject;
    }

    function getUrlParameterString(params) {
        var paramObjectToString = queryParamObjectToString(encodeQueryParamObject(params || {}));
        return paramObjectToString.length == 0 ? '' : '?' + paramObjectToString;
    }

    return {
        replaceUrl: function(url) {
            history.replaceState("", {}, url);
        },
        replaceUrlWithParameters: function(params) {
            var url = getUrlObject(document.location.hash);
            history.replaceState("", {}, url.base + getUrlParameterString(params));
        },
        refreshPageWithParameters: function (params) {
            var url = getUrlObject(document.location.hash);
            document.location = '#' + url.name + getUrlParameterString(params);
        },
        getUrlParameters: function() {
            return getQueryParamObject(getUrlObject(document.location.hash).query);
        }
    }

});