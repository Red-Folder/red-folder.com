module.exports = function () {
    var config = {
        source: { 
            root: "./wwwroot-src/"
        },
        destination: {
            root: "./wwwroot/"
        },
        tools: {
            jscsConfig: "./.jsjc.json"
        }
    };

    config.source.less = config.source.root + "css/**/*.less";
    config.source.css = config.source.root + "css/**/*.css";
    config.source.js = config.source.root + "scripts/**/*.js";
    config.source.js3rdParty = config.source.root + "scripts/3rdParty/**/*.js";
    config.source.jsToValidate = [config.source.js, "!" + config.source.js3rdParty];

    config.destination.css = config.destination.root + "/css";
    config.destination.js = config.destination.root + "/scripts";

    return config;
};