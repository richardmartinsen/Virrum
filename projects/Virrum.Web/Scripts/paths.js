(function (assign) {
    assign({
        files: {
            "knockout": "../Scripts/BuiltFiles/knockout",
            "koExtensions": "../Scripts/koExtensions",
            "moment": "../Scripts/BuiltFiles/moment",
            //"Sparkline": "../bower_components/sparklines/source/sparkline",
            //"Rickshaw": "../bower_components/rickshaw/rickshaw",
            //"d3-raw": "../bower_components/d3/d3",
            //"d3": "../Scripts/d3-norwegian",
            "customBindings": "../Scripts/customBindings",
            "lodash": "../Scripts/BuiltFiles/lodash",
            //"Mousetrap": "../bower_components/mousetrap/mousetrap",
            //"lodashExtensions": "../Scripts/lodashExtensions"
            "bootstrap": "../Scripts/BuiltFiles/affix"
        },
        folders: {
            //"deco/qvc/constraints": "../Scripts/constraints",
            "services": "../Scripts/services",
            "moment/locale": "../Scripts/BuiltFiles/",
        },
        add: function (customs) {
            var all = {};
            for (var file in this.files) {
                all[file] = this.files[file];
            }
            for (var folder in this.folders) {
                all[folder] = this.folders[folder];
            }
            for (var custom in customs) {
                all[custom] = customs[custom];
            }
            return all;
        },
        list: function () {
            var all = [];
            for (var file in this.files) {
                all.push(this.files[file].replace('..', 'projects/BaneBook.Web') + '.js');
            }
            for (var folder in this.folders) {
                all.push(this.folders[folder].replace('..', 'projects/BaneBook.Web') + '/**/*.js');
            }
            return all;
        }
    });
})(function (result) {
    if (typeof module === 'object' && 'exports' in module) {
        module.exports = result;
    } else {
        window.paths = result;
    }
});
