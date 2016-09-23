(function () {
    'use strict';

    angular
        .module('app')
        .directive('rfcWebCrawlGraph', webCrawlGraphDirective);

    webCrawlGraphDirective.$inject = ['d3Service','$window'];

    function webCrawlGraphDirective(d3Service, $window) {
        // Usage:
        //     <rfc-web-crawl-graph></rfc-web-crawl-graph>
        // Creates:
        //
        var directive = {
            restrict: 'E',
            scope: {
                data: '='
            },
            link: function (scope, element, attrs) {
                d3Service.d3().then(function(d3) {
                    var svg = d3.select(element[0])
                        .append('svg')
                        .style('width', '100%');

                    // Browser onresize event
                    window.onresize = function() {
                        scope.$apply();
                    };

                    // Watch for resize event
                    scope.$watch(function() {
                        return angular.element($window)[0].innerWidth;
                    }, function() {
                        scope.render(scope.data);
                    });

                    // watch for data changes and re-render
                    scope.$watch('data', function (newVals, oldVals) {
                        return scope.render(newVals);
                    }, true);

                    scope.render = function (data) {
                        // remove all previous items before render
                        svg.selectAll('*').remove();

                        // If we don't pass any data, return out of the element
                        if (!data) {
                            return;
                        }

                        var width = d3.select(element[0]).node().offsetWidth;
                        var height = 1500;

                        svg.attr('height', height)
                            //.style('visibility', 'hidden')
                            .attr('id', 'graph');

                        var force = d3.layout.force()
                                        .charge(-3000)
                                        .linkDistance(50)
                                        .size([width, height])
                                        .nodes(data.nodes)
                                        .links(data.links)
                                        .gravity(1)
                                        .start();

                        var link = svg.selectAll('.link')
                                        .data(data.links)
                                        .enter()
                                            .append('svg:line')
                                            .attr('class', 'link ')
                                            .style('stroke-width', 3);

                        var node = svg.selectAll('.node')
                                        .data(data.nodes)
                                        .enter()
                                            .append('svg:g')
                                            .attr('class', 'node');
                        node.append('svg:circle')
                            .attr('r', 5)
                            .style('fill', '#555')
                            .style('stroke', '#FFF')
                            .style('stroke-width', 3);
                        node.call(force.drag);

                        node.append('title')
                            .text(function (d) { return d.title; });

                        var updateLink = function () {
                            this.attr('x1', function (d) {
                                return d.source.x;
                            }).attr('y1', function (d) {
                                return d.source.y;
                            }).attr('x2', function (d) {
                                return d.target.x;
                            }).attr('y2', function (d) {
                                return d.target.y;
                            });
                        };

                        var updateNode = function () {
                            this.attr('transform', function (d) {
                                return 'translate(' + d.x + ',' + d.y + ')';
                            });
                        };

                        var updateLabels = function () {
                            this.each(function (d, i) {
                                var r = d3.select(this);
                                var w = r.select('text').node().getBBox().width;
                                var h = r.select('text').node().getBBox().height;
                                r.select('rect').attr('width', w + 10);
                                r.select('rect').attr('height', h + 10);
                                r.select('text').attr('x', 5);
                                r.select('text').attr('y', h + 5);
                            });
                        };

                        force.on('tick', function () {
                            node.call(updateNode);
                            link.call(updateLink);
                        });

                    };
                });
            }
        };
        return directive;

    }

})();
