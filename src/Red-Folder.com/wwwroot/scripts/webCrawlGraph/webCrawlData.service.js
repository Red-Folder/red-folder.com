(function () {
    'use strict';

    angular
        .module('app')
        .factory('webCrawlDataService', webCrawlDataService);

    webCrawlDataService.$inject = ['$http', '$q'];

    function webCrawlDataService($http, $q) {
        var service = {
            getAll: getAll
        };

        return service;

        function getAll() {
            return $http.get('https://rfc-webcrawl.azurewebsites.net/api/WebCrawlResults')
                .then(success)
                .catch(fail);

            function success(response) {
                return response.data;
            }

            function fail(error) {
                var msg = 'Query for Web Crawl results failed. ' + error.data.description;
                return $q.reject(msg);
            }
        }
    }
})();
