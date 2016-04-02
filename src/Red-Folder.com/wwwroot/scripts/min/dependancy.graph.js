var width=700,height=500,color=d3.scale.category20(),svg=d3.select("#microservice-dependancy").append("svg").attr("width",width).attr("height",height).style("visibility","hidden").attr("id","microservice-dependancy-graph"),url="http://localhost:52685/api/Dependancies";d3.json(url,function(t,e){if(t)throw t;for(var n=[],r=[],a=0;a<e.nodes.length;a++)n.push({node:e.nodes[a]}),n.push({node:e.nodes[a]});for(var a=0;a<e.nodes.length;a++)r.push({source:2*a,target:2*a+1,weight:1});var i=d3.layout.force().charge(-3e3).linkDistance(100).size([width,height]).nodes(e.nodes).links(e.links).gravity(1).start(),s=d3.layout.force().charge(-100).linkDistance(35).size([width,height]).nodes(n).links(r).gravity(0).linkStrength(8).start(),l=svg.selectAll(".link").data(e.links).enter().append("line").attr("class",function(t){return"link rag-"+t.rag}).style("stroke-width",function(t){return 6});l.append("title").text(function(t){return t.rag_message});var c=svg.selectAll(".node").data(e.nodes).enter().append("g").attr("class","node").call(i.drag);c.append("image").attr("xlink:href",function(t){return t.logo_url}).attr("x",-24).attr("y",-24).attr("width",48).attr("height",48),c.append("title").text(function(t){return t.description});var o=svg.selectAll(".anchorLink").data(r),d=svg.selectAll(".anchorNode").data(s.nodes()).enter().append("g").attr("class","anchorNode");d.append("circle").attr("r",0).style("fill","#FFF");var h=d.filter(function(t,e){return e%2==1}).append("g");h.append("rect").attr("height",25).attr("width",75).attr("rx",3).attr("ry",3).style("fill","#FFF").style("stroke-width",3).style("stroke","#000"),h.append("text").text(function(t,e){return t.node.name}).style("fill","#555").style("font-family","Arial").style("font-size",12);var g=function(){this.attr("x1",function(t){return t.source.x}).attr("y1",function(t){return t.source.y}).attr("x2",function(t){return t.target.x}).attr("y2",function(t){return t.target.y})},u=function(){this.attr("transform",function(t){return"translate("+t.x+","+t.y+")"})},y=function(){this.each(function(t,e){var n=d3.select(this),r=n.select("text").node().getBBox().width,a=n.select("text").node().getBBox().height;n.select("rect").attr("width",r+10),n.select("rect").attr("height",a+10),n.select("text").attr("x",5),n.select("text").attr("y",a+5)})};i.on("tick",function(){s.start(),c.call(u),d.each(function(t,e){if(e%2==0)t.x=t.node.x,t.y=t.node.y;else{var n=this.childNodes[1].getBBox(),r=t.x-t.node.x,a=t.y-t.node.y,i=Math.sqrt(r*r+a*a),s=n.width*(r-i)/(2*i);s=Math.max(-n.width,Math.min(0,s));var c=5;this.childNodes[1].setAttribute("transform","translate("+s+","+c+")")}d.call(u),l.call(g),o.call(g),h.call(y)})}),d3.select("#microservice-dependancy-graph").style("visibility","visible"),d3.select("#microservice-dependancy-loading-message").style("visibility","hidden")});