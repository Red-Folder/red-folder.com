(function() {
    'use strict';

    angular
        .module('app')
        .directive('rfcRepoGroup', RFCRepoGroupDirective);

    function RFCRepoGroupDirective() {
        // Usage:
        //     <rfc-repo-group></rfc-repo-group>
        // Creates:
        //
        var directive = {
            restrict: 'E',
            templateUrl: '/scripts/repoExplorer/templates/RFCRepoGroup.html',
            scope: {
                repos: '=',
            },
        };
        return directive;

    }

})();