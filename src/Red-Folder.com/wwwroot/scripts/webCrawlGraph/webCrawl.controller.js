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
        vm.selectedNode = null;

        vm.onClick = function (path) {
            vm.selectedNode = path;

            if (vm.selectedNode === null) {
                populateGraph(vm.rawData);
            } else {
                var filteredData = {};
                filteredData.links = vm.rawData.links.filter(function (link) {
                    return (link.source === vm.selectedNode || link.target === vm.selectedNode);
                });

                var filteredUrls = filteredData.links.map(function(link) {
                    return link.source;
                }).concat(
                filteredData.links.map(function(link) {
                    return link.target;
                }));

                filteredData.urls = vm.rawData.urls.filter(function (url) {
                    return filteredUrls.indexOf(url.path) >= -1;
                });

                populateGraph(filteredData);
            }
        };

        function getData() {
            webCrawlDataService.getAll()
                .then(
                    function (webCrawlResults) {
                        vm.errorred = false;
                        processData(webCrawlResults);
                        vm.loading = false;
                    }
                )
                .catch(
                    function (response) {
                        vm.errorred = true;
                        processData(null);
                    }
                );
        }

        function processData(rawData) {
            vm.rawData = rawData;
            populateGraph(rawData);
        }

        function populateGraph(filteredData) {
            if (filteredData === null) {
                vm.graphData = null;
            } else {
                var nodes = filteredData.urls.map(function (url) {
                    return {
                        title: url.path
                    };
                });
                var links = filteredData.links.map(function (link) {
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
