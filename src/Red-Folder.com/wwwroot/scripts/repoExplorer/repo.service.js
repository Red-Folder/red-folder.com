(function() {
    'use strict';

    angular
        .module('app')
        .service('repoService', RepoService);

    RepoService.$inject = ['$resource'];

    function RepoService($resource) {
        var webservice = $resource('/api/Repo');
        this.getAll = function() {
            return webservice.query();
        };
    }
})();