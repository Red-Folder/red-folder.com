(function() {
    'use strict';

    angular
        .module('app')
        .directive('rfcToggle', RFCToggleDirective);

    function RFCToggleDirective() {
        // Usage:
        //     <rfc-toggle></rfc-toggle>
        // Creates:
        //
        var directive = {
            restrict: 'E',
            templateUrl: '/scripts/repoExplorer/templates/RFCToggle.html',
            scope: {
                option: '=',
                onToggle: '&',
                show: '=',
            },
        };
        return directive;

    }

})();