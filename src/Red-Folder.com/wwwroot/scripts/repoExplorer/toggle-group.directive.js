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
                selected: '=',
            },
            controller: ['$scope', function($scope) {
                $scope.onToggle = function (option) {
                    var index = $scope.selected.indexOf(option);
                    if (index > -1) {
                        $scope.selected.splice(index, 1);
                    } else {
                        $scope.selected.push(option);
                    }
                };

                $scope.defaultShow = true;
            }]
        };
        return directive;

    }

})();
