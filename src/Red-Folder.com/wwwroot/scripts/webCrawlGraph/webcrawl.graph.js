function indexOfByPath(urls, path) {
    for (var i = 0; i < urls.length; i++) {
        if (urls[i].path === path) {
            return i;
        }
    }
    return -1;
}


//.header('Access-Control-Allow-Origin', '*')
function processData(error, graph) {
    if (error) {
        throw error;
    }

    var width = 1500;
    var height = 1500;

    var svg = d3.select('#webcrawl-container').append('svg')
        .attr('width', width)
        .attr('height', height)
        .style('visibility', 'hidden')
        .attr('id', 'graph');

    var processedLinks = graph.links.map(function (link) {
        return {
            source: indexOfByPath(graph.urls, link.source),
            target: indexOfByPath(graph.urls, link.target),
            weight: 1
        };
    });

    var force = d3.layout.force()
                    .charge(-3000)
                    .linkDistance(50)
                    .size([width, height])
                    .nodes(graph.urls)
                    .links(processedLinks)
                    .gravity(1)
                    .start();

    var link = svg.selectAll('.link')
                    .data(processedLinks)
                    .enter()
                        .append('svg:line')
                        .attr('class', 'link ')
                        .style('stroke-width', 3);

    var node = svg.selectAll('.node')
                    .data(graph.urls)
                    .enter()
                        .append('svg:g')
                        .attr('class', 'node');
    node.append('svg:circle').attr('r', 5).style('fill', '#555').style('stroke', '#FFF').style('stroke-width', 3);
    node.call(force.drag);

    node.append('title')
        .text(function (d) { return d.path; });

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

    d3.select('#graph').style('visibility', 'visible');
    d3.select('#loading-message').style('visibility', 'hidden');

};

$(document).ready(function () {
    var url = 'https://rfc-webcrawl.azurewebsites.net/api/WebCrawlResults';
    d3.json(url, processData);
});