require.config({
    baseUrl: '/Features',
    paths: paths.add({
        'deco': '../Scripts/BuiltFiles/deco', 'affix': '../Scripts/BuiltFiles/affix'
    }),
    packages: [
        { name: 'when', location: '../Scripts/BuiltFiles/when', main: 'when' }
    ],
    shim: {
    },
    urlArgs: 'cacheKey=' + document.querySelector('meta[name=cacheKey]').getAttribute('content')
});

require([
    'deco',
    'moment',
    'moment/locale/nb',
    'koExtensions',
    'customBindings',
    'affix'
], function (
    deco,
    moment,
    momentNorsk,
    koExtensions,
    customBindings,
    affix
) {
    moment.locale(momentNorsk._abbr);
    ES6Promise.polyfill();

    deco.config({
        qvc: {
            baseUrl: '/qvc'
        },
        spa: {
            //Name of the inital action in a controller
            index: 'index',

            //Never cache any pages, always fetch them from the server
            cachePages: false,

            //Convert the path to a url
            pathToUrl: function(path) {
                path = path.toLowerCase() == 'index' ? '' : path;
                return '/' + path;
            }
        }
    }).start();
});