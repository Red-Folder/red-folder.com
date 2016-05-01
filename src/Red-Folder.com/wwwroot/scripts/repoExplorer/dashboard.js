(function() {
    'use strict';

    angular
        .module('app')
        .controller('dashboard', dashboard);

    dashboard.$inject = ['$scope', 'repoService'];

    function dashboard($scope, repoService) {
        $scope.title = 'dashboard';
        $scope.errorred = false;
        $scope.repos = [];
        $scope.filterOptions = [];

        $scope.$watch('repos', function() {
            var uniqueTags = [];
            $scope.repos.forEach(function(repo) {
                repo.tags.forEach(function(tag) {
                    if ($scope.filterOptions.filter(function(filter) { return filter.name === tag; }).length === 0) {
                        $scope.filterOptions.push({
                            name: tag,
                            show: true,
                        });
                    }
                });
            });
        });

        repoService.getAll()
            .$promise
            .then(
                function(repos) {
                    $scope.repos = repos;
                }
            )
            .catch(
                function(response) {
                    $scope.errorred = true;
                    $scope.repos = [];
                }
            );

        $scope.selectedFilters = function() {
            return $scope.filterOptions.filter(function(element) {
                return element.show;
            }).map(function(element) {
                return element.name;
            });
        };

        activate();

        function activate() { }
    }
})();
