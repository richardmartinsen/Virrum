var paths = require('./projects/Virrum.Web/Scripts/paths.js');

module.exports = function(config) {
    config.set({
        frameworks: ['jasmine'],

        files: [
            'node_modules/sinon/pkg/sinon.js',
            'projects/Virrum.Web/bower_components/requirejs/require.js',
            'node_modules/karma-requirejs/lib/adapter.js',
            'node_modules/jasmine-sinon/lib/jasmine-sinon.js',
            'node_modules/jazzmine/bin/jazzmine.min.js',
            { pattern: 'projects/Virrum.Web/bower_components/deco/Source/**/*.js', included: false },
            { pattern: 'projects/Virrum.Web/bower_components/when/*.js', included: false }
        ]
        .concat(paths.list().map(function(path) {
            return { pattern: path, included: false };
        }))
        .concat([
            'projects/Virrum.Web/Scripts/paths.js',
            'projects/Virrum.Web.Tests/JavaScript/specs-main.js',
            'projects/Virrum.Web.Tests/JavaScript/Matchers/*',
            { pattern: 'projects/Virrum.Web/Features/**/*.js', included: false },
            { pattern: 'projects/Virrum.Web.Tests/JavaScript/Mocks/**/*.js', included: false },
            { pattern: 'projects/Virrum.Web.Tests/JavaScript/Given/**/*.js', included: false },
            'projects/Virrum.Web.Tests/JavaScript/Features/**/*.js'
        ]),

        reporters: ['dots', 'beep']

    });
};