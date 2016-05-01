(function() {
    'use strict';

    angular
        .module('app')
        .directive('rfcToggleGroup', RFCToggleDirective);

    function RFCToggleDirective() {
        // Usage:
        //     <rfc-toggle-group></rfc-toggle-group>
        // Creates:
        //
        var directive = {
            restrict: 'E',
            templateUrl: '/scripts/repoExplorer/templates/RFCToggleGroup.html',
            scope: {
                options: '=',
            },
        };
        return directive;

    }

})();