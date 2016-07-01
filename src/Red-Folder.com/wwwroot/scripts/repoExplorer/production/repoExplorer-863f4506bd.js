!function(){"use strict";angular.module("app",["ngResource","frapontillo.bootstrap-switch"])}();
!function(){"use strict";function o(o){function n(){o.getAll().then(function(o){e.errorred=!1,t(o)})["catch"](function(o){e.errorred=!0,t([])})}function t(o){e.repos=o,e.options=[],e.repos.forEach(function(o){o.tags.forEach(function(o){0===e.options.filter(function(n){return n===o}).length&&e.options.push(o)})}),e.selected=e.options.slice(0)}function r(){n()}var e=this;e.title="dashboard",e.errorred=!1,e.repos=[],e.options=[],e.selected=[],r()}angular.module("app").controller("DashboardController",o),o.$inject=["repoService"]}();
!function(){"use strict";function e(){var e={restrict:"E",templateUrl:"/scripts/repoExplorer/templates/RFCRepoGroup.html",scope:{repos:"=",selected:"="}};return e}angular.module("app").directive("rfcRepoGroup",e)}();
!function(){"use strict";function e(){var e={restrict:"E",templateUrl:"/scripts/repoExplorer/templates/RFCRepo.html",scope:{name:"=",description:"=",stars:"=",openIssues:"=",tags:"="}};return e}angular.module("app").directive("rfcRepo",e)}();
!function(){"use strict";function e(){var e={restrict:"E",templateUrl:"/scripts/repoExplorer/templates/RFCToggleGroup.html",scope:{options:"=",selected:"="},controller:function(e){e.onToggle=function(t){var o=e.selected.indexOf(t);o>-1?e.selected.splice(o,1):e.selected.push(t)},e.defaultShow=!0}};return e}angular.module("app").directive("rfcToggleGroup",e)}();
!function(){"use strict";function e(){var e={restrict:"E",templateUrl:"/scripts/repoExplorer/templates/RFCToggle.html",scope:{option:"=",onToggle:"&",show:"="}};return e}angular.module("app").directive("rfcToggle",e)}();
!function(){"use strict";function t(t,e){function r(){function r(t){return t.data}function n(t){var r="Query for Repos for people failed. "+t.data.description;return e.reject(r)}return t.get("/api/Repo").then(r)["catch"](n)}var n={getAll:r};return n}angular.module("app").factory("repoService",t),t.$inject=["$http","$q"]}();