(function() {
    'use strict';

    angular
        .module('app')
        .controller('DashboardController', DashboardController);

    DashboardController.$inject = ['$scope', 'repoService'];

    function DashboardController($scope, repoService) {
        var vm = this;
        vm.title = 'dashboard';
        vm.errorred = false;
        vm.repos = [];
        vm.filterOptions = [];

        vm.options = [];
        vm.selected = [];

        $scope.$watch('vm.repos', function() {
            vm.options = [];
            vm.repos.forEach(function(repo) {
                repo.tags.forEach(function(tag) {
                    if (vm.options.filter(function (filter) { return filter === tag; }).length === 0) {
                        vm.options.push(tag);
                    }
                });
            });
            vm.selected = vm.options.slice(0);
        });

        //repoService.getAll()
        //    .$promise
        //    .then(
        //        function(repos) {
        //            $scope.repos = repos;
        //        }
        //    )
        //    .catch(
        //        function(response) {
        //            $scope.errorred = true;
        //            $scope.repos = [];
        //        }
        //    );

        activate();

        function activate() { }
    }
})();
