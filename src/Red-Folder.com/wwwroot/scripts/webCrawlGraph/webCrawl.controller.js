(function() {
    'use strict';

    angular
        .module('app')
        .controller('WebCrawlController', WebCrawlController);

    WebCrawlController.$inject = ['webCrawlDataService'];

    function WebCrawlController(webCrawlDataService) {
        var vm = this;
        vm.errorred = false;
        vm.loading = true;
        vm.rawData = null;
        vm.graphData = null;

        function getData() {
            webCrawlDataService.getAll()
                .then(
                    function (webCrawlResults) {
                        vm.errorred = false;
                        populateData(webCrawlResults);
                        vm.loading = false;
                    }
                )
                .catch(
                    function (response) {
                        vm.errorred = true;
                        populateData(null);
                    }
                );
        }

        function populateData(webCrawlResults) {
            vm.rawData = webCrawlResults;

            if (vm.rawData === null) {
                vm.graphData = null;
            } else {
                var nodes = vm.rawData.urls.map(function (url) {
                    return {
                        title: url.path
                    };
                });
                var links = vm.rawData.links.map(function (link) {
                    return {
                        source: indexOfByPath(nodes, link.source),
                        target: indexOfByPath(nodes, link.target),
                        weight: 1
                    };
                });

                vm.graphData = {
                    nodes: nodes,
                    links: links
                };
            }
        }

        function indexOfByPath(urls, title) {
            for (var i = 0; i < urls.length; i++) {
                if (urls[i].title === title) {
                    return i;
                }
            }
            return -1;
        }

        function activate() {
            getData();
        }

        activate();
    }
})();
