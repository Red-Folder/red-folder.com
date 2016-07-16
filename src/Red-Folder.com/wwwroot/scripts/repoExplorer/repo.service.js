(function() {
    'use strict';

    angular
        .module('app')
        .factory('repoService', repoService);

    repoService.$inject = ['$http', '$q'];

    function repoService($http, $q) {
        var service = {
            getAll: getAll
        };

        return service;

        function getAll() {
            return $http.get('/api/Repo')
                .then(success)
                .catch(fail);

            function success(response) {
                return response.data;
            }

            function fail(error) {
                var msg = 'Query for Repos failed. ' + error.data.description;
                return $q.reject(msg);
            }
        }
    }
})();
