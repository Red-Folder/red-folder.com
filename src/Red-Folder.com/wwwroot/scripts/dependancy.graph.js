var width = 700,
       height = 500;

var color = d3.scale.category20();

var svg = d3.select("#microservice-dependancy").append("svg")
    .attr("width", width)
    .attr("height", height)
    .style("visibility", "hidden")
    .attr("id", "microservice-dependancy-graph");

//var url = "http://products-microservices-red-folder.cloudapp.net/api/Dependancies";
var url = "http://localhost:52685/api/Dependancies";

d3.json(url, function (error, graph) {
    if (error) throw error;

    // Notes
    // Uses http://bl.ocks.org/MoritzStefaner/1377729 for the labels
    // Uses http://bl.ocks.org/mbostock/950642 for the logos

    // Create label Anchors for later
    var labelAnchors = [];
    var labelAnchorLinks = [];
    for (var i = 0; i < graph.nodes.length; i++) {
        labelAnchors.push({ node: graph.nodes[i] });
        labelAnchors.push({ node: graph.nodes[i] });
    }

    for (i = 0; i < graph.nodes.length; i++) {
        labelAnchorLinks.push({
            source: i * 2,
            target: i * 2 + 1,
            weight: 1
        });
    }

    var force = d3.layout.force()
                    .charge(-3000)
                    .linkDistance(100)
                    .size([width, height])
                    .nodes(graph.nodes)
                    .links(graph.links)
                    .gravity(1)
                    .start();

    var force2 = d3.layout.force()
                    .charge(-100)
                    .linkDistance(35)
                    .size([width, height])
                    .nodes(labelAnchors)
                    .links(labelAnchorLinks)
                    .gravity(0)
                    .linkStrength(8)
                    .start();

    var link = svg.selectAll(".link")
                    .data(graph.links)
                    .enter()
                        .append("line")
                        .attr("class", function (d) { return "link " + "rag-" + d.rag; })
                        .style("stroke-width", function (d) { return 6; });
    link.append("title")
        .text(function (d) { return d.rag_message; });

    /*
    var node = svg.selectAll(".node")
                    .data(graph.nodes)
                    .enter()
                        .append("circle")
                        .attr("class", "node")
                        .attr("r", 5)
                        .style("fill", function (d) { return color(1); })
                        .call(force.drag);
    */
    var node = svg.selectAll(".node")
                    .data(graph.nodes)
                    .enter()
                        .append("g")
                        .attr("class", "node")
                        .call(force.drag);

    node.append("image")
            .attr("xlink:href", function (d) { return d.logo_url; })
            .attr("x", -24)
            .attr("y", -24)
            .attr("width", 48)
            .attr("height", 48);

    node.append("title")
        .text(function (d) { return d.description; });

    //node.append("text")
    //    .text(function (d) { return d.name; });

    var anchorLink = svg.selectAll(".anchorLink")
                        .data(labelAnchorLinks);
                        //.enter()
                        //    .append("line")
                        //    .attr("class", "anchorLink")
                        //    .style("stroke", "#999");

    var anchorNode = svg.selectAll(".anchorNode")
                        .data(force2.nodes())
                        .enter()
                            .append("g")
                            .attr("class", "anchorNode");

    anchorNode.append("circle")
            .attr("r", 0)
            .style("fill", "#FFF");

    var labelNode = anchorNode
                        .filter(function (d, i) {
                            return i % 2 == 1;
                        })
                        .append("g");
    labelNode
        //.filter(function (d, i) {
        //    return i % 2 == 1
        //})
        .append("rect")       // attach a rectangle
        .attr("height", 25)    // set the height
        .attr("width", 75)     // set the width
        .attr("rx", 3)         // set the x corner curve radius
        .attr("ry", 3)         // set the y corner curve radius
        .style("fill", "#FFF")
        .style("stroke-width", 3)
        .style("stroke", "#000");

    labelNode
        .append("text")
        //.text(function (d, i) {
        //    return i % 2 == 0 ? "" : d.node.name
        //})
        .text(function (d, i) { return d.node.name; })
        .style("fill", "#555")
        .style("font-family", "Arial")
        .style("font-size", 12);


    var updateLink = function () {
        this.attr("x1", function (d) {
            return d.source.x;
        }).attr("y1", function (d) {
            return d.source.y;
        }).attr("x2", function (d) {
            return d.target.x;
        }).attr("y2", function (d) {
            return d.target.y;
        });
    };

    var updateNode = function () {
        this.attr("transform", function (d) {
            return "translate(" + d.x + "," + d.y + ")";
        });
    };

    var updateLabels = function () {
        //console.debug(this.select("text").node().getBBox().width);
        this.each(function (d, i) {
            var r = d3.select(this);
            //console.debug(d.select("text").node().getBBox().width);
            //console.debug(r.select("rect"));
            var w = r.select("text").node().getBBox().width;
            var h = r.select("text").node().getBBox().height;
            r.select("rect").attr("width", w + 10);
            r.select("rect").attr("height", h + 10);
            r.select("text").attr("x", 5);
            r.select("text").attr("y", h + 5);
        });

        //this
        //    .select("rect").attr("width", this.select("text").node().getBBox().width);
    };

    force.on("tick", function () {
        force2.start();

        node.call(updateNode);

        anchorNode.each(function (d, i) {
            if (i % 2 === 0) {
                d.x = d.node.x;
                d.y = d.node.y;
            } else {
                var b = this.childNodes[1].getBBox();

                var diffX = d.x - d.node.x;
                var diffY = d.y - d.node.y;

                var dist = Math.sqrt(diffX * diffX + diffY * diffY);

                var shiftX = b.width * (diffX - dist) / (dist * 2);
                shiftX = Math.max(-b.width, Math.min(0, shiftX));
                var shiftY = 5;
                this.childNodes[1].setAttribute("transform", "translate(" + shiftX + "," + shiftY + ")");
            }

            anchorNode.call(updateNode);

            link.call(updateLink);
            anchorLink.call(updateLink);

            labelNode.call(updateLabels);
        });
        /*
        link.attr("x1", function (d) { return d.source.x; })
            .attr("y1", function (d) { return d.source.y; })
            .attr("x2", function (d) { return d.target.x; })
            .attr("y2", function (d) { return d.target.y; });

        node.attr("cx", function (d) { return d.x; })
            .attr("cy", function (d) { return d.y; });
        */
    });

    d3.select("#microservice-dependancy-graph").style("visibility", "visible");
    d3.select("#microservice-dependancy-loading-message").style("visibility", "hidden");

});
