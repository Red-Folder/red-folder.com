(function() {
    'use strict';

    angular
        .module('app')
        .controller('dashboard', dashboard);

    dashboard.$inject = ['$scope', 'repoService'];

    function dashboard($scope, repoService) {
        $scope.title = 'dashboard';

        $scope.filterOptions = [
            {
                name: 'Cordova',
                show: true,
            },
            {
                name: 'Java',
                show: true,
            },
            {
                name: 'C#',
                show: true,
            },
            {
                name: 'JavaScript',
                show: true,
            },
            {
                name: 'CSS',
                show: false,
            },
        ];

        $scope.selectedFilters = function() {
            return $scope.filterOptions.filter(function(element) {
                return element.show;
            }).map(function(element) {
                return element.name;
            });
        };

        $scope.repos = repoService.getAll;

        activate();

        function activate() { }
    }
})();
