(function() {
    'use strict';

    angular
        .module('app')
        .filter('tagFilter', function() {
            return function(repos, tags) {
                return repos.filter(function(repo) {
                    for (var i in repo.tags) {
                        if (tags.indexOf(repo.tags[i]) != -1) {
                            return true;
                        }
                    }
                    return false;
                });
            };
        });
})();