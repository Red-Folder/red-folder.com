(function() {
    'use strict';

    angular
        .module('app')
        .service('repoService', RepoService);

    RepoService.$inject = ['$http', '$q'];

    function RepoService($resource) {
        var webservice = $resource('/api/Repo');
        this.getAll = function() {
            return $http.get('/api/Repo')
                .then(success)
                .catch(fail);

            function success(response) {
                return response.data;
            }

            function fail(e) {
                //return exception.catcher('XHR Failed for getAll')(e);
            }
        };
    }
})();