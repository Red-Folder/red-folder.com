(function() {
    'use strict';

    angular
        .module('app')
        .directive('rfcRepo', RFCRepoDirective);

    function RFCRepoDirective() {
        // Usage:
        //     <rfc-repo></rfc-repo>
        // Creates:
        //
        var directive = {
            restrict: 'E',
            templateUrl: '/scripts/repoExplorer/templates/RFCRepo.html',
            scope: {
                name: '=',
                description: '=',
                stars: '=',
                openIssues: '=',
                tags: '=',
            },
        };
        return directive;

    }

})();
