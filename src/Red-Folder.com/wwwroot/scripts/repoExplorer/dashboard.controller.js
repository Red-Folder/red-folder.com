(function() {
    'use strict';

    angular
        .module('app')
        .controller('DashboardController', DashboardController);

    DashboardController.$inject = ['repoService'];

    function DashboardController(repoService) {
        var vm = this;
        vm.title = 'dashboard';
        vm.errorred = false;
        vm.repos = [];

        vm.options = [];
        vm.selected = [];

        function getData() {
            repoService.getAll()
                .then(
                    function (repos) {
                        vm.errorred = false;
                        populateData(repos);
                    }
                )
                .catch(
                    function(response) {
                        vm.errorred = true;
                        populateData([]);
                    }
                );
        }

        function populateData(data) {
            vm.repos = data;
            vm.options = [];
            vm.repos.forEach(function(repo) {
                repo.tags.forEach(function(tag) {
                    if (vm.options.filter(function (filter) { return filter === tag; }).length === 0) {
                        vm.options.push(tag);
                    }
                });
            });
            vm.selected = vm.options.slice(0);
        }

        function activate() {
            getData();
        }

        activate();
    }
})();
